using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup2014WP
{
    public partial class App
    {
        IsolatedStorageSettings _Settings = IsolatedStorageSettings.ApplicationSettings;

        #region DownloadImageOnlyOnWifi

        private const string KEY_WIFI_IMAGE = "wifi_image";

        private bool _DownloadImageOnlyOnWifi = false;
        public bool DownloadImageOnlyOnWifi
        {
            get
            {
                return _DownloadImageOnlyOnWifi;
            }
            set
            {
                if (_DownloadImageOnlyOnWifi != value)
                {
                    _DownloadImageOnlyOnWifi = value;
                    UpdateSetting(KEY_WIFI_IMAGE, value);
                }
            }
        }

        #endregion

        #region Banner

        private const string KEY_DISMISSED_BANNER = "banner";

        private string _DismissedBannerId = string.Empty;
        public string DismissedBannerId
        {
            get
            {
                return _DismissedBannerId;
            }
            set
            {
                if (_DismissedBannerId != value)
                {
                    _DismissedBannerId = value;
                    UpdateSetting(KEY_DISMISSED_BANNER, value);
                }
            }
        }

        #endregion

        private void LoadSettings()
        {
            ////wifi image
            //if (_Settings.Contains(KEY_WIFI_IMAGE))
            //{
            //    _DownloadImageOnlyOnWifi = (bool)_Settings[KEY_WIFI_IMAGE];
            //}
            //else
            //{
            //    _Settings.Add(KEY_WIFI_IMAGE, false);
            //}

            //dismissed banner
            if (_Settings.Contains(KEY_DISMISSED_BANNER))
            {
                _DismissedBannerId = (string)_Settings[KEY_DISMISSED_BANNER];
            }
            else
            {
                _Settings.Add(KEY_DISMISSED_BANNER, string.Empty);
            }

            _Settings.Save();
        }

        private void UpdateSetting(string key, object value)
        {
            _Settings[key] = value;
            _Settings.Save();
        }
    }
}
