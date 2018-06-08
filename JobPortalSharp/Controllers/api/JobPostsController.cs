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
        public object Search([FromUri]SearchViewModel model)
        {
            const int LOC_RADIUS_KM = 50;

            IEnumerable<JobPost> query = db.JobPosts
                .Include(x => x.Employer)
                .Include(x => x.EmploymentType)
                .Include(x => x.Industry)
                .Where(x => x.Paid);//todo: this is inefficient

            if (string.IsNullOrWhiteSpace(model.q) == false)
            {
                query = query.Where(x => x.Name.ToLower().Contains(model.q.ToLower()) || x.Employer.Name.ToLower().Contains(model.q.ToLower()) || x.Details.Contains(model.q.ToLower()));
            }

            if (model.ets != null && model.ets.Count() > 0)
            {
                query = query.Where(x => model.ets.Any(y => y == x.EmploymentTypeId));
            }

            if (model.ers != null && model.ers.Count() > 0)
            {
                query = query.Where(x => model.ers.Any(y => y == x.Employer.EmployerTypeId));
            }

            if (string.IsNullOrWhiteSpace(model.l) == false)
            {
                var loc = model.l.ToLower();
                query = query.Where(x => 
                    x.LocationSameAsEmployer ? (x.Employer.AddressTown.ToLower().Contains(loc)) : x.AddressTown.ToLower().Contains(loc));
            }

            query = query.ToList();
            
            if (model.nb)
            {
                if (model.lng != null && model.lat != null)
                {
                    var nearbyJobs = db.JobPosts
                        .Include(x => x.Employer)
                        .ToList()
                        .Except(query)
                        .Where(x => DistanceBetweenPlaces(
                            x.LocationSameAsEmployer ? x.Employer.AddressLongitude.Value : x.AddressLongitude.Value, 
                            x.LocationSameAsEmployer ? x.Employer.AddressLatitude.Value : x.AddressLatitude.Value, 
                            model.lng.Value, 
                            model.lat.Value) <= LOC_RADIUS_KM);

                    query = query.Concat(nearbyJobs);
                }
                else
                {
                    //todo: show error to view
                }
            }

            if (model.sort == 1)
            {
                query = query.OrderByDescending(x => x.ExpirationDate);
            }
            else if (model.sort == 2)
            {
                query = query.OrderByDescending(x => x.PostDate);
            }

            model.rc = query.Count();

            return Json(new
            {
                draw = model.draw,
                data = query.Select(x => new JobPostDto
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

        private IEnumerable<LocationEntityDto> GetJobWithLocations2()
        {
            return db.JobPosts.Include(x => x.Employer).Select(x => new LocationEntityDto
            {
                Id = x.Id,
                AddressCountry = x.LocationSameAsEmployer ? x.Employer.AddressCountry : x.AddressCountry,
                AddressLatitude = x.LocationSameAsEmployer ? x.Employer.AddressLatitude : x.AddressLatitude,
                AddressLongitude = x.LocationSameAsEmployer ? x.Employer.AddressLatitude : x.AddressLatitude,
                AddressState = x.LocationSameAsEmployer ? x.Employer.AddressState : x.AddressState,
                AddressStreet = x.LocationSameAsEmployer ? x.Employer.AddressStreet : x.AddressStreet,
                AddressTown = x.LocationSameAsEmployer ? x.Employer.AddressTown : x.AddressTown
            });
        }
    }
}
