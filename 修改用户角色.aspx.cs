using System;
using System.Web.UI;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using WebApplication1.cs;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class ChangeRole : System.Web.UI.Page
    {

        public enum dd
        {
            Mike=100,
            nike=102,
            jike
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                return;
            }
            if ((string)Session["roleid"] != "0")
            {
                MessageBox.Show("非法操作,请重新登陆");
                Session.Abandon();
                Response.Redirect("Login.aspx");
                return;
            }
            CheckOtherLogin();
            if (!Page.IsPostBack)
            {
                getUser("");
            }
           
        }


        public void getUser(string id)
        {

            //id为空说明要显示所有的账号，而id表明要显示某一账号，所以要分开
            //UserInfo allUser = registerDAO.getUserByNum(num, access_token);
            List<UserInfo> lr = new List<UserInfo>();
            string access_token = (string)Session["access_token"];
            //先得到有多少数量
            int total = changeRole.getTotalUser(id,access_token);
            //再根据数量来一个一个读取信息
            JObject jo = changeRole.getAllUser(id, access_token);
            for (int i = 0; i < total; i++)
            {
                UserInfo item = changeRole.getItem(jo, i, access_token);
                lr.Add(item);
            }
            role.DataSource = lr;
            role.DataBind();

        }
        public void Search(object sender, EventArgs e)
        {
            getUser(userId.Text);
        }
        public void back(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }

        protected void role_SelectedIndexChanged(object sender, EventArgs e)
        { 
            int index = role.SelectedIndex;
            //MessageBox.Show(index+"");
            string str= role.Rows[index].Cells[0].Text;
            MessageBox.Show(str);
            string ro = role.Rows[index].Cells[4].Text;
            MessageBox.Show(ro);
            if (ro == "超级管理员")
            {
                MessageBox.Show("该用户不可被修改");
                return;
            }
            string access_token = (string)Session["access_token"];
            if (changeRole.change(ro, str, access_token) == "0")
            {
                MessageBox.Show("修改权限成功");
                getUser(userId.Text);
            }
            else
            {
                MessageBox.Show("修改权限失败");
            }



        }
        
        protected void role_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            role.PageIndex = e.NewPageIndex;
            getUser("");
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