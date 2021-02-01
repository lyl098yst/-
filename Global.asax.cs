using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication1
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
           RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //锁上防止多个用户同时修改Application的值
            Application.Lock();
            Application["UserOnLineCnt"] = 0;
            Application["PageClickCnt"] = 0;
            Application.UnLock();

        }
        void Session_Start(object sender,EventArgs e)
        {
            //应该登陆进去之后再加1
 /*           //一个新会话启动时执行的代码
            Application.Lock();
            Application["UserOnLineCnt"] =(int)Application["UserOnLineCnt"]+1;
            Application.UnLock();*/
        }
        void Session_End(object sender,EventArgs e)
        {
            //此时需要从类中去掉当前用户名

            if (((int)Application["UserOnLineCnt"])>0)
            {
                Application.Lock();
                Application["UserOnLineCnt"] = (int)Application["UserOnLineCnt"] - 1;
                Application.UnLock();
            }
        }
    }
}