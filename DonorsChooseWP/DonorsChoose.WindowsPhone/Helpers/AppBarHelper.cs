using System;
using Microsoft.Phone.Shell;

namespace DonorsChoose.WindowsPhone.Helpers
{
    internal static class ApplicationBarHelper
    {
        internal static ApplicationBar CreateApplicationBar(bool isMenuEnabled, bool isVisible)
        {
            // Create and return an instance of an ApplicationBar
            var applicationBar = new ApplicationBar();
            applicationBar.IsMenuEnabled = isMenuEnabled;
            applicationBar.IsVisible = isVisible;
            return applicationBar;
        }

        internal static ApplicationBarIconButton CreateApplicationBarButton(IApplicationBar applicationBar,
            string buttonUrl, string buttonText, bool isEnabled)
        {
            // Trying to add more than 4 buttons to an application bar
            // will result in a runtime error, so we need to check for that
            if (applicationBar.Buttons.Count >= 4)
            {
                return null;
            }

            // Create the button
            var applicationBarButton = new ApplicationBarIconButton();
            applicationBarButton.IconUri = new Uri(buttonUrl, UriKind.Relative);
            applicationBarButton.Text = buttonText;
            applicationBarButton.IsEnabled = isEnabled;

            // Add the button to the specified application bar instance
            applicationBar.Buttons.Add(applicationBarButton);

            // Return a reference to the button
            return applicationBarButton;
        }

    }
}
