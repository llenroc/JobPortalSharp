using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JobPortalSharp.Data;
using JobPortalSharp.Models;
using System.Data.Entity;
using JobPortalSharp.Data.Dto;

namespace JobPortalSharp.Controllers.api
{
    public class JobPostsController : ApiController
    {
        private JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        [AllowAnonymous]
        [Route("api/jobpost/{id}")]
        public IHttpActionResult Get(int id)
        {
            var obj = db.JobPosts.Single(x => x.Id == id);
            return Ok(obj);
        }

        [HttpGet]
        [AllowAnonymous]
        public object Search([FromUri]SearchViewModel2 model)
        {
            const int LOC_RADIUS_KM = 10;

            IEnumerable<JobPost> query1 = db.JobPosts
                .Include(x => x.Employer)
                .Include(x => x.EmploymentType)
                .Include(x => x.Industry)
                .Where(x => x.Paid);

            if (string.IsNullOrWhiteSpace(model.q) == false)
            {
                query1 = query1.Where(x => x.Name.ToLower().Contains(model.q.ToLower()) || x.Employer.Name.ToLower().Contains(model.q.ToLower()));
            }

            if (model.ets != null && model.ets.Count() > 0)
            {
                query1 = query1.Where(x => model.ets.Any(y => y == x.EmploymentTypeId));
            }

            if (model.ers != null && model.ers.Count() > 0)
            {
                query1 = query1.Where(x => model.ers.Any(y => y == x.Employer.EmployerTypeId));
            }

            if (string.IsNullOrWhiteSpace(model.l) == false)
            {
                var loc = model.l.ToLower();
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

            if (model.nb)
            {
                if (model.lng != null && model.lat != null)
                {
                    var nearbyJobs = GetJobWithLocations()
                        .Except(query1)
                        .Where(x => DistanceBetweenPlaces(x.AddressLongitude.Value, x.AddressLatitude.Value, model.lng.Value, model.lat.Value) >= LOC_RADIUS_KM);
                    query1 = query1.Concat(nearbyJobs);
                }
                else
                {
                    //todo: show error to view
                }
            }

            if (model.sort == 1)
            {
                query1 = query1.OrderByDescending(x => x.ExpirationDate);
            }
            else if (model.sort == 2)
            {
                query1 = query1.OrderByDescending(x => x.PostDate);
            }

            model.rc = query1.Count();

            return Json(new
            {
                draw = model.draw,
                data = query1.Select(x => new JobPostDto
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
                    })
                    .Skip(model.start)
                    .Take(model.length),
                recordsTotal = model.rc,
                recordsFiltered = model.rc
            });
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
