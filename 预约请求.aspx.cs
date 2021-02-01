using System;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Configuration;
using WebApplication1.cs;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;
using System.Web.UI.WebControls;
using WebApplication1.cs.控制类;

namespace WebApplication1
{
    public partial class 预约请求 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                return;
            }
            if ((string)Session["roleid"] != "1" && (string)Session["roleid"] != "2")
            {
                MessageBox.Show("非法操作,请重新登陆");
                Session.Abandon();
                Response.Redirect("Login.aspx");
                return;
            }
            CheckOtherLogin();
            if (!Page.IsPostBack)
            {
                GetReserve(isbn.Text);
            }
        }

        protected void reserve_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            reserve.PageIndex = e.NewPageIndex;
            GetReserve(isbn.Text);
        }

        public void Search(object sender, EventArgs e)
        {
            GetReserve(isbn.Text);
        }
        public void GetReserve(string isbn)
        {
            string roleid = (string)Session["roleid"];
            List<Reserve> lr = new List<Reserve>();
            string access_token = (string)Session["access_token"];
            string num = (string)Session["num"];
            //先得到有多少数量
            int total = ReserveADO.getAllNum(roleid,isbn,access_token,num);
            //再根据数量来一个一个读取信息
            JObject jo= ReserveADO.getReserve(roleid, isbn, access_token,num);
            for (int i = 0; i < total; i++)
            {
               
                Reserve item = ReserveADO.getItem(jo,i,access_token);
                if (item.status == 0)
                {
                    string src = ReserveADO.getUrl(jo, i, access_token);
                    item.url = src;
                }
                else
                {
                    item.url = "../images/expire.jpg";
                }
                lr.Add(item);
            }
            reserve.DataSource = lr;
            reserve.DataBind();
        }
        protected void role_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = reserve.SelectedIndex;
            //MessageBox.Show(index+"");
            string str = reserve.Rows[index].Cells[1].Text;
            //MessageBox.Show(str);//书号

            
            string str1 = reserve.Rows[index].Cells[7].Text;
            //MessageBox.Show(str1);//which哪一本
            string str2 = reserve.Rows[index].Cells[6].Text;
            //MessageBox.Show(str2);//状态
            if (str2 == "-1" || str2 == "1")
            {
                MessageBox.Show("该预约不可取消");
                return;
            }

            string access_token = (string)Session["access_token"];

            //获取图书信息并修改
            if (OperateCloud.cancelReserve(str, str1, access_token) == true)
            {
                if (ReserveADO.cancelRes(str,str1,access_token) == "0")
                {
                    
                    MessageBox.Show("取消预约成功");
                }
                else
                {
                    MessageBox.Show("取消预约失败");
                }
                //将图书数据库的数据恢复调用云函数即可
                GetReserve(isbn.Text);
            }
            else
            {
                MessageBox.Show("取消预约失败");
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