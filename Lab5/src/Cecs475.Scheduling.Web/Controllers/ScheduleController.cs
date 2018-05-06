using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cecs475.Scheduling.Web.Controllers
{
    [RoutePrefix("api/schedule")]
    public class ScheduleController : ApiController
    {
        private Model.CatalogContext mContext = new Model.CatalogContext();

        [HttpGet]
        [Route("{year}/{term}")]
        public IEnumerable<CourseSectionDto> GetCourse(string year, string term)
        {
            var semester = mContext.SemesterTerms.Where(s => s.Name.Equals(String.Format("{0} {1}", term, year), StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();
            if (semester == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(
                HttpStatusCode.NotFound, $"No semester term with term + year {term} {year} found"));
            }
            return semester.CourseSections.Select(CourseSectionDto.From);
        }
    }
}