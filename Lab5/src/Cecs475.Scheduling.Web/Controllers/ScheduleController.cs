using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cecs475.Scheduling.Web.Controllers
{
    public class SemesterTermDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static SemesterTermDto From(Model.SemesterTerm s)
        {
            return new SemesterTermDto()
            {
                Id = s.Id,
                Name = s.Name,
            };
        }

    }

    [RoutePrefix("api/schedule")]
    public class ScheduleController : ApiController
    {
        private Model.CatalogContext mContext = new Model.CatalogContext();

        [HttpGet]
        [Route("{year}/{term}")]
        public IEnumerable<CourseSectionDto> GetCourseSections(string year, string term)
        {
            var semester = mContext.SemesterTerms.Where(s => s.Name.Equals(term + " " + year, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();
            ValidateSemester(semester, $"No semester term with term: {term} year: {year} found");
            return semester.CourseSections.Select(CourseSectionDto.From);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IEnumerable<CourseSectionDto> GetCourseSections(int id)
        {
            var semester = mContext.SemesterTerms.Where(s => s.Id == id).SingleOrDefault();
            ValidateSemester(semester, $"No semester term with id: {id} found");
            return semester.CourseSections.Select(CourseSectionDto.From);
        }

        [HttpGet]
        [Route("terms")]
        public IEnumerable<SemesterTermDto> GetSemesterTerms()
        {
            return mContext.SemesterTerms.Select(SemesterTermDto.From);
        }

        public void ValidateSemester(Model.SemesterTerm semester, String errorString)
        {
            if (semester == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(
                HttpStatusCode.NotFound, errorString));
            }
        }
    }
}