using System;

namespace EM.Data.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobSpecification { get; set; }
        public string EmploymentStatus { get; set; }
        public string JobCategory { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string SubUnit { get; set; }
        public string Location { get; set; }
        public string RequiredDocumentsOnFile { get; set; }
        public byte[] Resume { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string ContractDetail { get; set; }
    }
}