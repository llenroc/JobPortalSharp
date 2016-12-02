using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobPortalSharp.Data;
using JobPortalSharp.Models;
using X.PagedList;
using JobPortalSharp.Data.Dto;
using Microsoft.AspNet.Identity;

namespace JobPortalSharp.Controllers
{
    class JobGeolocation
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class HomeController : Controller
    {
        public JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        public ActionResult Index()
        {
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId");
            ViewBag.Industries = db.Industries.ToList();

            var model = new SearchViewModel();

            model.EmployerTypes = db.EmployerTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.EmploymentTypes = db.EmploymentTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

        public ActionResult Search(SearchViewModel model)
        {
            const int LOC_RADIUS_KM = 10;

            var pageNumber1 = model.p1 ?? 1;
            var pageNumber2 = model.p2 ?? 1;
            var pageSize = model.ps ?? 5;

            IEnumerable<JobPost> query1 = db.JobPosts.Include(x => x.Employer).Include(x => x.EmploymentType).Include(j => j.Industry);

            if (string.IsNullOrWhiteSpace(model.q) == false)
            {
                query1 = query1.Where(x => x.Name.ToLower().Contains(model.q.ToLower()) || x.Employer.Name.ToLower().Contains(model.q.ToLower()));
            }

            if (string.IsNullOrWhiteSpace(model.l1) == false)
            {
                var loc = model.l1.ToLower();
                query1 = query1
                    .Where(x =>
                        (
                            x.LocationSameAsEmployer &&
                            (
                                x.Employer.AddressStreet.ToLower().Contains(loc) ||
                                x.Employer.AddressTown.ToLower().Contains(loc) ||
                                x.Employer.AddressState.ToLower().Contains(loc) ||
                                x.Employer.AddressCountry.ToLower().Contains(loc)
                            )
                        )
                        ||
                        (
                            x.LocationSameAsEmployer == false &&
                            (
                                x.AddressStreet.ToLower().Contains(loc) ||
                                x.AddressTown.ToLower().Contains(loc) ||
                                x.AddressState.ToLower().Contains(loc) ||
                                x.AddressCountry.ToLower().Contains(loc)
                            )
                        )
                    );
            }

            if (model.nb1)
            {
                if (model.lngL1 != null && model.latL1 != null)
                {
                    var nearbyJobs = GetJobWithLocations()
                        .Except(query1)
                        .Where(x => DistanceBetweenPlaces(x.AddressLongitude.Value, x.AddressLatitude.Value, model.lngL1.Value, model.latL1.Value) >= LOC_RADIUS_KM);
                    query1 = query1.Concat(nearbyJobs);
                }
                else
                {
                    //todo: show error to view
                }
            }

            model.rc = query1.Count();

            model.Results1 = query1.OrderByDescending(x => x.PostDate).Select(x => new JobPostDto
            {
                AddressCountry = x.LocationSameAsEmployer ? x.Employer.AddressCountry : x.AddressCountry,
                AddressTown = x.LocationSameAsEmployer ? x.Employer.AddressTown : x.AddressTown,
                AddressState = x.LocationSameAsEmployer ? x.Employer.AddressState : x.AddressState,
                Details = x.Details,
                EmployerId = x.EmployerId,
                EmployerName = x.Employer.Name,
                EmploymentTypeName = x.EmploymentType.Name,
                IndustryName = x.Industry.Name,
                Id = x.Id,
                Name = x.Name,
                Salary = x.Salary,
                SalaryRangeFrom = x.SalaryRangeFrom,
                SalaryRangeTo = x.SalaryRangeTo
            }).ToPagedList(pageNumber1, pageSize);

            //model.Results2 = query2.OrderByDescending(x => x.PostDate).Select(x => new JobPostDto
            //{
            //    AddressCountry = x.LocationSameAsEmployer ? x.Employer.AddressCountry : x.AddressCountry,
            //    AddressTown = x.LocationSameAsEmployer ? x.Employer.AddressTown : x.AddressTown,
            //    AddressState = x.LocationSameAsEmployer ? x.Employer.AddressState : x.AddressState,
            //    Details = x.Details,
            //    EmployerId = x.EmployerId,
            //    EmployerName = x.Employer.Name,
            //    EmploymentTypeName = x.EmploymentType.Name,
            //    IndustryName = x.Industry.Name,
            //    Id = x.Id,
            //    Name = x.Name,
            //    Salary = x.Salary,
            //    SalaryRangeFrom = x.SalaryRangeFrom,
            //    SalaryRangeTo = x.SalaryRangeTo
            //}).ToPagedList(pageNumber2, pageSize);

            //todo: get user's location
            //todo: search jobs based on user's location sorted by distance

            model.EmployerTypes = db.EmployerTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.EmploymentTypes = db.EmploymentTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            var userId = User.Identity.GetUserId();
            ViewBag.JobSelectionCount = db.JobSelections.Where(x => x.CreatedById == userId).Count();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RecentJobs()
        {
            var jobs = db.JobPosts.Include(j => j.EmploymentType).OrderByDescending(j => j.PostDate);
            return PartialView(jobs);
        }

        const double PIx = Math.PI;
        const double RADIO = 6378.16;

        private double Radians(double x)
        {
            return x * PIx / 180;
        }

        private double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            double R = 6371; // km

            double sLat1 = Math.Sin(Radians(lat1));
            double sLat2 = Math.Sin(Radians(lat2));
            double cLat1 = Math.Cos(Radians(lat1));
            double cLat2 = Math.Cos(Radians(lat2));
            double cLon = Math.Cos(Radians(lon1) - Radians(lon2));

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;
            double d = Math.Acos(cosD);
            double dist = R * d;

            return dist;
        }

        private IEnumerable<JobPost> GetJobWithLocations()
        {
            var tmp = db.JobPosts
                .Include(j => j.Employer)
                .Include(j => j.EmploymentType)
                .Include(j => j.Industry)
                .Where(j => j.LocationSameAsEmployer)
                .ToList();
            tmp.ForEach(x =>
            {
                x.AddressLongitude = x.Employer.AddressLongitude;
                x.AddressLatitude = x.Employer.AddressLatitude;
                x.AddressTown = x.Employer.AddressTown;
                x.AddressState = x.Employer.AddressState;
                x.AddressCountry = x.Employer.AddressCountry;
            });

            return db.JobPosts
                .Include(j => j.Employer)
                .Include(j => j.EmploymentType)
                .Include(j => j.Industry)
                .Where(j => j.LocationSameAsEmployer == false).ToList()
                .Concat(tmp);
        }
    }
}