using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.RegistrationApp
{

    public class SemesterTermDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


    public class CourseSectionDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string CourseNumber { get; set; }
        public int SectionNumber { get; set; }

        public override string ToString()
        {
            return $"{DepartmentName} {CourseNumber}-{SectionNumber}";
        }
    }

    public class RegistrationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// A URL path to the registration web service.
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// The full name of the student who is registering.
        /// </summary>
        public string FullName { get; set; }
        private IEnumerable<SemesterTermDto> semesterTerms;
        public IEnumerable<SemesterTermDto> SemesterTerms
        {
            get { return semesterTerms; }
            set
            {
                semesterTerms = value;
                OnPropertyChanged(nameof(SemesterTerms));
            }
        }
        private IEnumerable<CourseSectionDto> courseSections;
        public IEnumerable<CourseSectionDto> CourseSections
        {
            get { return courseSections; }
            set
            {
                courseSections = value;
                OnPropertyChanged(nameof(CourseSections));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
