using System;

namespace DonorsChoose.WindowsPhone.Services.Network.DataTransferObjects
{
    public class ProposalInfo
    {
        public int Id { get; set; }
        public string ProposalUrl { get; set; }
        public string FundUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FulfillmentTrailer { get; set; }
        public double PercentFunded { get; set; }
        public double CostToComplete { get; set; }
        public MatchingFundInfo MatchingFund { get; set; }
        public double TotalPrice { get; set; }
        public bool FreeShipping { get; set; }
        public string TeacherName { get; set; }
        public GradeLevelInfo GradeLevel { get; set; }
        public string PovertyLevel { get; set; }
        public string SchoolName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ZoneInfo Zone { get; set; }
        public SubjectInfo Subject { get; set; }
        public ResourceInfo Resource { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string FundingStatus { get; set; }
    }
}
