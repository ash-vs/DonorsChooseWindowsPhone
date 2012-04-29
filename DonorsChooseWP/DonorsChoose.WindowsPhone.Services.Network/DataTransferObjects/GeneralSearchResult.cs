namespace DonorsChoose.WindowsPhone.Services.Network.DataTransferObjects
{
    public class GeneralSearchResult
    {
        public string SearchTerms { get; set; }
        public string SearchUrl { get; set; }
        public int TotalProposals { get; set; }
        public int Index { get; set; }
        public int Max { get; set; }
        public ProposalInfo[] Proposals { get; set; }
    }
}
