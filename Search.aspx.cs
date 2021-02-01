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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                //因为redirect之后的代码是不执行的所以加不加return无所谓
                //return;
            }
            //MessageBox.Show(Session.SessionID);
            
            //首先判断登录是否过期，如果没有过期，就判断是否被顶下线
            CheckOtherLogin();
            if (Application["UserOnLineCnt"] != null)
            {
                //Response.Write("在线人数为" + Application["UserOnLineCnt"] + "<br />");
            }
           /* Application.Lock();
            Application["PageClickCnt"] = (int)Application["PageClickCnt"] + 1;
            Application.UnLock();*/
            /*Response.Write("点击次数为" + Application["PageClickCnt"]+"<br />");*/
            if (!Page.IsPostBack)
            {
                string roleid = (string)Session["roleid"];
                string access_token = (string)Session["access_token"];
                JObject jo = getFunc.getFun(roleid,access_token);
                int length = getFunc.getLength(jo);
                //this.TreeView1.ShowLines = true;//在控件中显示网格线
                Logo.InnerHtml += "<ul>";
                for (int i = 0; i < length; i++)
                {
                    TreeNode rootNode = new TreeNode();//定义根节点
                    string funcName = getFunc.getFuncName(jo, i, access_token);
                    rootNode.Text = funcName;
                    //成功取出
                    rootNode.NavigateUrl = "~/" + funcName + ".aspx";
                    //rootNode.ChildNodes.Add(tr1);//把子节点添加到根节点                
                    //this.TreeView1.Nodes.Add(rootNode);//把根节点添加到TreeView控件               
                    Logo.InnerHtml += "<li><a href='" + funcName + ".aspx'>"+funcName+"</a></li>";
                }
                Logo.InnerHtml += "</ul>";
              /*  Logo.InnerHtml += "<ul>";
                Logo.InnerHtml += "<li><a href = '#' onclick = 'exit()' runat = 'server' >退出</a></li>";
                
                Logo.InnerHtml += "</ul>";*/
            }
        }
        public void Search(object sender, EventArgs e)
        {
            //MessageBox.Show(book.Text + "");
            Response.Redirect("Library.aspx?BookName="+book.Text);
        }
        public void Exit(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
            //该方法直接跳转
            //Server.Transfer("WebForm1.aspx");
            //该方法显示之前已登录，再显示登录已过期
            //Server.Execute("register.aspx");
            //控件显示在一个页面

        }



        public void CheckOtherLogin()
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