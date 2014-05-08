using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using WorldCup2014WP.Models;

namespace WorldCup2014WP.DataContext
{
    public class SubscriptionDataContext
    {
        #region Singleton

        private static SubscriptionDataContext instance = new SubscriptionDataContext();

        static SubscriptionDataContext()
        {
            //for thread-safe
        }

        private SubscriptionDataContext()
        {
        }

        public static SubscriptionDataContext Current
        {
            get { return instance; }
        }

        #endregion

        IsolatedStorageSettings _Settings = IsolatedStorageSettings.ApplicationSettings;

        public List<GameSchedule> LoadSubscriptions()
        {
            List<GameSchedule> list = null;
            if (_Settings.Contains(Constants.KEY_SUBSCRIPTION_LIST))
            {
                list = _Settings[Constants.KEY_SUBSCRIPTION_LIST] as List<GameSchedule>;
            }
            if (list == null)
            {
                list = new List<GameSchedule>();
            }
            return list;
        }

        public void AddSubscription(GameSchedule item)
        {
            List<GameSchedule> list = LoadSubscriptions();
            var duplication = list.FirstOrDefault(x => x.ID == item.ID);
            if (duplication == null)
            {
                list.Add(item);
                SaveSubscriptions(list);
            }
        }

        public void RemoveSubscription(string id)
        {
            List<GameSchedule> list = LoadSubscriptions();
            var item = list.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                list.Remove(item);
                SaveSubscriptions(list);
            }
        }

        public void SaveSubscriptions(List<GameSchedule> list)
        {
            // txtInput is a TextBox defined in XAML.
            if (_Settings.Contains(Constants.KEY_SUBSCRIPTION_LIST))
            {
                _Settings[Constants.KEY_SUBSCRIPTION_LIST] = list;
            }
            else
            {
                _Settings.Add(Constants.KEY_SUBSCRIPTION_LIST, list);
            }
            _Settings.Save();
        }

        public void ClearSubscriptions()
        {
            if (_Settings.Contains(Constants.KEY_SUBSCRIPTION_LIST))
            {
                _Settings[Constants.KEY_SUBSCRIPTION_LIST] = new List<GameSchedule>();
                _Settings.Save();
            }
        }


    }
}
