using System;

using Windows.ApplicationModel;

namespace AppStudio.ViewModels
{
    public class AboutThisAppViewModel
    {
        public string Publisher
        {
            get
            {
                return "AppStudio";
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            }
        }

        public string AboutText
        {
            get
            {
                return "The world\'s largest professional association for the advancement of technology";
            }
        }
    }
}

