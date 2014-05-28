using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCup2014WP.Controls;
using WorldCup2014WP.Models;

namespace WorldCup2014WP
{
    public partial class App
    {
        IsolatedStorageSettings _Settings = IsolatedStorageSettings.ApplicationSettings;

        #region Load & Update
        
        private void LoadSettings()
        {
            //user
            if (_Settings.Contains(KEY_USER))
            {
                _User = (UserLoginResult)_Settings[KEY_USER];
            }
            else
            {
                _Settings.Add(KEY_USER, false);
            }

            ////user id
            //if (_Settings.Contains(KEY_USER_ID))
            //{
            //    _UserId = (string)_Settings[KEY_USER_ID];
            //}
            //else
            //{
            //    _Settings.Add(KEY_USER_ID, false);
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

        #region User

        private const string KEY_USER = "user";
        private UserLoginResult _User = null;
        public UserLoginResult User
        {
            get
            {
                return _User;
            }
            set
            {
                if (_User != value)
                {
                    _User = value;
                    UpdateSetting(KEY_USER, value);
                }
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return User != null;
            }
        }

        #endregion

        #region Toast Notification

        public CuteToast Toast { get; set; }

        public void ShowToastMessage(string message)
        {
            if (Toast!=null)
            {
                Toast.ShowMessage(message);
            }
        }

        #endregion
    }
}
