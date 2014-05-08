using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace WorldCup2014WP.Utility
{
    public class NetworkHelper
    {
        #region Singleton

        private static NetworkHelper instance = new NetworkHelper();

        public static NetworkHelper Current
        {
            get { return instance; }
        }

        #endregion

        private const string _HOST = "115.28.21.97";

        private bool _IsWifiConnectionAvailable;
        public bool IsWifiConnectionAvailable
        {
            get
            {
                if (DeviceNetworkInformation.IsNetworkAvailable && DeviceNetworkInformation.IsWiFiEnabled && _IsWifiConnectionAvailable)
                {
                    return true;
                }
                return false;
            }
        }

        private void UpdateStatus()
        {
            _IsWifiConnectionAvailable = false;

            DeviceNetworkInformation.ResolveHostNameAsync(
                new DnsEndPoint(_HOST, 80),
                new NameResolutionCallback(handle =>
                {
                    NetworkInterfaceInfo info = handle.NetworkInterface;
                    if (info != null)
                    {
                        switch (info.InterfaceType)
                        {
                            case NetworkInterfaceType.Ethernet:
                                //NetName = "Ethernet";
                                break;
                            case NetworkInterfaceType.MobileBroadbandCdma:
                            case NetworkInterfaceType.MobileBroadbandGsm:
                                switch (info.InterfaceSubtype)
                                {
                                    case NetworkInterfaceSubType.Cellular_3G:
                                        //NetName = "Cellular_3G + 3G";
                                        break;
                                    case NetworkInterfaceSubType.Cellular_EVDO:
                                        //NetName = "Cellular_EVDO + 3G";
                                        break;
                                    case NetworkInterfaceSubType.Cellular_EVDV:
                                        //NetName = "Cellular_EVDV + 3G";
                                        break;
                                    case NetworkInterfaceSubType.Cellular_HSPA:
                                        //NetName = "Cellular_HSPA + 3G";
                                        break;
                                    case NetworkInterfaceSubType.Cellular_GPRS:
                                        //NetName = "Cellular_GPRS + 2G";
                                        break;
                                    case NetworkInterfaceSubType.Cellular_EDGE:
                                        //NetName = "Cellular_EDGE + 2G";
                                        break;
                                    case NetworkInterfaceSubType.Cellular_1XRTT:
                                        //NetName = "Cellular_1XRTT + 2G";
                                        break;
                                    default:
                                        //NetName = "None";
                                        break;
                                }
                                break;
                            case NetworkInterfaceType.Wireless80211:
                                _IsWifiConnectionAvailable = true;
                                //NetName = "WiFi";
                                break;
                            default:
                                //NetName = "None";
                                break;
                        }
                    }
                    else
                    {
                        //NetName = "None";
                    }
                }), null);
        }

        public void StartListening()
        {
            UpdateStatus();
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        public void StopListening()
        {
            NetworkInformation.NetworkStatusChanged -= NetworkInformation_NetworkStatusChanged;
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            UpdateStatus();
        }
    }
}
