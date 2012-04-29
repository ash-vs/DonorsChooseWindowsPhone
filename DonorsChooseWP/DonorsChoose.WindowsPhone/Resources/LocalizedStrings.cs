namespace DonorsChoose.WindowsPhone.Resources
{
    public class LocalizedStrings
    {
        private static AppResources _localizedResources;

        public AppResources LocalizedResources 
        { 
            get { return _localizedResources; } 
        }


        public LocalizedStrings()
        {
            _localizedResources = new AppResources();
        }
    }
}
