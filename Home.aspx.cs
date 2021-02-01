using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Documents;
using WebApplication1.cs;
using TableCell = System.Web.UI.WebControls.TableCell;
using TableRow = System.Web.UI.WebControls.TableRow;

namespace WebApplication1
{
    public partial class Home :  System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //刚开始判断一下session，如果过期了就退出了，重新登录
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
            }
            CheckOtherLogin();

            //如果没有过期就显示出来登录的用户名
            if (!Page.IsPostBack)
            {
                //因为无法级联查询，所以只能这种方式
                string roleid = (string)Session["roleid"];
                string envid = ConfigurationManager.AppSettings["envid"];
                string queryString = "{\"env\":\"" + envid + "\", \"query\": \"db.collection(\\\"role_func\\\").where({roleid:\\\"" + roleid + "\\\"}).limit(10).get()\"}";
                
                string access_token = (string)Session["access_token"];
                string url = "https://api.weixin.qq.com/tcb/databasequery?access_token=" + access_token; //POST到网站
                string Message = OperateCloud.Query(queryString, url);
                JObject jo = OperateCloud.GetJson(Message);
                int length = int.Parse(jo["pager"]["Total"].ToString());
                //MessageBox.Show(length+"");

                //this.TreeView1.ShowLines = true;//在控件中显示网格线

                for (int i = 0; i < length; i++)
                {
                    string temp = jo["data"][i].ToString();//data里面第一个列表里面的json数据
                    JObject jo1 = OperateCloud.GetJson(temp);
                    TreeNode rootNode = new TreeNode();//定义根节点
                    //获得了funcid之后，从function数据库中取出功能名称
                    string funcid = jo1["funcid"].ToString();
                    queryString = "{\"env\":\"" + envid + "\", \"query\": \"db.collection(\\\"func\\\").where({id:\\\"" + funcid + "\\\"}).limit(10).get()\"}";
                    url = "https://api.weixin.qq.com/tcb/databasequery?access_token=" + access_token; //POST到网站
                    Message = OperateCloud.Query(queryString, url);
                    JObject jo2 = OperateCloud.GetJson(Message);
                    string function = jo2["data"][0].ToString();//data里面第一个列表里面的json数据
                    JObject jo22 = OperateCloud.GetJson(function);
                    rootNode.Text = jo22["func"].ToString();
                    //MessageBox.Show(jo22["func"].ToString());
                    //成功取出
                    rootNode.NavigateUrl = "~/" + jo22["func"].ToString() + ".aspx";
                    //rootNode.ChildNodes.Add(tr1);//把子节点添加到根节点                
                    TreeView1.Nodes.Add(rootNode);//把根节点添加到TreeView控件中
                }
                TreeNode rootNode1 = new TreeNode();//定义根节点
                rootNode1.Text = "返回首页";

                rootNode1.NavigateUrl = "~/Search.aspx";
                TreeView1.Nodes.Add(rootNode1);//把根节点添加到TreeView控件中
                showPersonInfo();
            }

        }
        
        public void showPersonInfo()
        {
            string num = (string)Session["num"];
            string access_token = (string)Session["access_token"];
            UserInfo user = changeRole.getUserByID(num, access_token);
            TextBox0.Text = user.num;
            TextBox4.Text = user.name;
            TextBox3.Text = user.roleid;
            TextBox1.Text = user.phone;
            TextBox2.Text = user.email;            
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

        public void back(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
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
