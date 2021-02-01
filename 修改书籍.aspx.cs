using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using WebApplication1.cs;

namespace WebApplication1
{
    public partial class 修改书籍 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
            }
            if ((string)Session["roleid"] != "1")
            {
                MessageBox.Show("非法操作,请重新登陆");
                Session.Abandon();
                Response.Redirect("Login.aspx");
                return;
            }
            CheckOtherLogin();
        }
        public void detail(object sender, EventArgs e)
        {
            string str = isbn.Text;
            if (str == "")
            {
                MessageBox.Show("请输入ISBN号");
                return;
            }
            //首先判断是否存在

            //MessageBox.Show(str);
            if (exist(str))
            {
                Response.Redirect("BookDetail.aspx?isbn=" + str);
            }
            else
            {
                MessageBox.Show("书库中不存在该书籍");
            }
            
        }

        public bool exist(string isbn)
        {
            string access_token = (string)Session["access_token"];  
            if (BookADO.existBook(isbn,access_token))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void back(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }


        protected void CheckOtherLogin()
        {

            Hashtable hOnline = (Hashtable)Application["Online"];//获取已经存储的application值
            if (hOnline != null)
            {
                IDictionaryEnumerator idE = hOnline.GetEnumerator();
                while (idE.MoveNext())
                {
                    if (idE.Key != null && idE.Key.ToString().Equals(Session.SessionID))
                    {
                        //already login
                        if (idE.Value != null && "XX".Equals(idE.Value.ToString()))//说明在别处登录
                        {
                            hOnline.Remove(Session.SessionID);
                            Application.Lock();
                            Application["Online"] = hOnline;
                            Application.UnLock();
                            Session.Abandon();
                            Response.Write("<script>alert('你的帐号已在别处登陆，你被强迫下线！');window.location.href='Login.aspx';</script>");//退出当前到登录页面

                            //Response.Redirect("Login.aspx");
                            Response.End();
                        }
                    }
                }
            }
        }
    }
}