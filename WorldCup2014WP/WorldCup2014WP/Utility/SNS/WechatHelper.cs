using MicroMsg.sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Resources;

namespace WorldCup2014WP.Utility
{
    public class WechatHelper
    {
        private const string WECHAT_APP_ID = "wx5ca05c4a5ddeb5d2";

        #region Singleton

        private static WechatHelper instance = new WechatHelper();

        public static WechatHelper Current
        {
            get { return instance; }
        }

        #endregion

        public void Send(string title, string desc, byte[] image, string url)
        {
            string AppID = WECHAT_APP_ID;
            //int scene = SendMessageToWX.Req.WXSceneSession; //发给微信朋友
            //int scene = SendMessageToWX.Req.WXSceneTimeline;//发送到朋友圈
            int scene = SendMessageToWX.Req.WXSceneChooseByUser; //用户选择发给好友还是朋友圈

            WXWebpageMessage msg = new WXWebpageMessage();
            msg.Title = title;
            msg.Description = desc;
            msg.ThumbData = image;
            msg.WebpageUrl = url;

            //把数据发送给微信，数据错误或发送错误，会抛出WXException，调试时，可从WXException中得知是什么错误。
            //发布应用时，可直接向用户提示错误，也可以根据自己实际需要定制错误的处理
            try
            {
                SendMessageToWX.Req req = new SendMessageToWX.Req(msg, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI(AppID);
                api.SendReq(req);
            }
            catch (WXException ex)
            {
            }
        }

        public void Send(string title, string desc, string imagePath, string url)
        {
            var thumbData = IOHelper.ReadRes(imagePath);
            Send(title, desc, thumbData, url);
        }

        //public void Send(string title, string desc, string imagePath, string url)
        //{
        //    string AppID = WECHAT_APP_ID;
        //    //int scene = SendMessageToWX.Req.WXSceneSession; //发给微信朋友
        //    //int scene = SendMessageToWX.Req.WXSceneTimeline;//发送到朋友圈
        //    int scene = SendMessageToWX.Req.WXSceneChooseByUser; //用户选择发给好友还是朋友圈

        //    WXWebpageMessage msg = new WXWebpageMessage();
        //    msg.Title = title;
        //    msg.Description = desc;
        //    msg.ThumbData = IOHelper.ReadRes(imagePath);
        //    msg.WebpageUrl = url;

        //    //把数据发送给微信，数据错误或发送错误，会抛出WXException，调试时，可从WXException中得知是什么错误。
        //    //发布应用时，可直接向用户提示错误，也可以根据自己实际需要定制错误的处理
        //    try
        //    {
        //        SendMessageToWX.Req req = new SendMessageToWX.Req(msg, scene);
        //        IWXAPI api = WXAPIFactory.CreateWXAPI(AppID);
        //        api.SendReq(req);
        //    }
        //    catch (WXException ex)
        //    {
        //    }
        //}
    }
}
