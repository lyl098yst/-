using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using WebApplication1.cs.实体类;
using WebApplication1.cs.控制类;

namespace WebApplication1
{
    public partial class 借阅历史 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                return;
            }
            if ((string)Session["roleid"] != "2")
            {
                MessageBox.Show("非法操作,请重新登陆");
                Session.Abandon();
                Response.Redirect("Login.aspx");
                return;
            }
            CheckOtherLogin();
            getAllHis();
        }

/*        protected void History_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = History.SelectedIndex;
            string ISBN = History.Rows[index].Cells[0].Text;
            MessageBox.Show(ISBN);
            Response.Redirect("BookDetail.aspx?isbn=" + ISBN);

        }*/

        public void getAllHis()
        {
            string access_token = (string)Session["access_token"];
            List<HadBorrow> borrows = new List<HadBorrow>();
            //先得到有多少数量
            string num = (string)Session["num"];
            int total = HadBorrowADO.getNum(access_token,num);
            //再根据数量来一个一个读取信息
            JObject jo = HadBorrowADO.getAllBorrow(access_token,num);
            for (int i = 0; i < total; i++)
            {
                HadBorrow item = HadBorrowADO.getItem(jo, i);

                borrows.Add(item);
            }
            History.DataSource = borrows;
            History.DataBind();
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