using System;
using System.Collections.Generic;

namespace EM.Data.Models
{
    public class Qualification
    {
        public int Id { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<Education> Educations { get; set; }
        public List<Skills> Skills { get; set; }
        public List<Language> Languages { get; set; }
        public List<License> Licenses { get; set; }
    }

    #region Internal Classes

    public class License
    {
        public int Id { get; set; }
        public string LicenseType { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
    }

    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fluency { get; set; }
        public string Competency { get; set; }
        public string Comments { get; set; }
    }

    public class WorkExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
    }

    public class Education
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Year { get; set; }
        public string Score { get; set; }
    }

    #endregion

}