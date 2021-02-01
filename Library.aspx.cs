using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Configuration;
using TableCell = System.Web.UI.WebControls.TableCell;
using TableRow = System.Web.UI.WebControls.TableRow;
using System.Collections;

namespace WebApplication1
{
    public partial class Library : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
            }
            CheckOtherLogin();
            if (!Page.IsPostBack)
            {

                book.Text = Request.QueryString["BookName"];
                Session["CurrentPage"] = 1;
                current.Text = "1";
                GetBook(book.Text);
            }
        }
        public void GetBook(string name)
        {
            string envid = ConfigurationManager.AppSettings["envid"];
            int booknum = int.Parse(ConfigurationManager.AppSettings["booknum"]);
            //string name = Request.QueryString["BookName"];

            int skip = booknum * ((int)Session["CurrentPage"] - 1);
            string queryString = "";
            if (name == "")
            {

                queryString = "{\"env\":\"" + envid + "\", \"query\": \"db.collection(\\\"library\\\").where({}).limit(" + booknum + ").skip(" + skip + ").get()\"}";
            }
            else
            {

                queryString = "{\"env\":\"" + envid + "\", \"query\": \"db.collection(\\\"library\\\").where({name:db.RegExp({regexp:\\\"" + name + "\\\" ,option:\\\"i\\\"})}).limit(" + booknum + ").skip(" + skip + ").get()\"}";
            }
            //access_token
            //MessageBox.Show(queryString);
            string access_token = (string)Session["access_token"];
            string url = "https://api.weixin.qq.com/tcb/databasequery?access_token=" + access_token; //POST到网站
            string Message = OperateCloud.Query(queryString, url);
            JObject jo = OperateCloud.GetJson(Message);
            //MessageBox.Show("errcode:" + jo["errcode"].ToString());
            int limit = int.Parse(jo["pager"]["Limit"].ToString());
            int total = int.Parse(jo["pager"]["Total"].ToString());
            //MessageBox.Show("total"+total+"limit"+limit);
            if (total % booknum != 0)
            {
                Session["Count"] = total / booknum + 1;
                count.Text = Session["Count"] + "";
            }
            else
            {
                Session["Count"] = total / booknum;
                count.Text = Session["Count"] + "";
            }
            //MessageBox.Show("count:"+Session["Count"]);
            //List<string> filelist = new List<string>();
            int loop = 0;
            if (limit < total - skip)
            {
                loop = limit;
            }
            else
            {
                loop = total - skip;
            }
            for (int i = 0; i < loop; i++)
            {
                string temp = jo["data"][i].ToString();
                JObject jo1 = OperateCloud.GetJson(temp);
                //MessageBox.Show(jo1["name"].ToString());

                queryString = "{\"env\":\"" + envid + "\", \"file_list\": [{\"fileid\":\"" + jo1["fileIDs"][0].ToString() + "\",\"max_age\":7200}]}";

                //access_token
                url = "https://api.weixin.qq.com/tcb/batchdownloadfile?access_token=" + access_token; //POST到网站
                Message = OperateCloud.Query(queryString, url);
                JObject jo2 = OperateCloud.GetJson(Message);
                int errorcode = int.Parse(jo2["errcode"].ToString());
                if (errorcode == 0)
                {
                    /*MessageBox.Show("获取图片成功");
                    MessageBox.Show(jo2["file_list"][0]["download_url"].ToString());*/
                    TableRow row = new TableRow();

                    TableCell cell3 = new TableCell();
                    cell3.Text = "<p style='width:100px;padding-left:30px' class='overline'>" + jo1["remain"].ToString() + "</ p>";
                    row.Cells.Add(cell3);

                    TableCell cell1 = new TableCell();
                    cell1.Text = "<p style='width:100px;padding:10px' class='overline'>" + jo1["name"].ToString() + "</ p>";
                    row.Cells.Add(cell1);

                    TableCell cell0 = new TableCell();
                    cell0.Text = "<img style='width:120px;height:160px' src='" + jo2["file_list"][0]["download_url"].ToString() + "'/>";
                    row.Cells.Add(cell0);

                    TableCell cell6 = new TableCell();
                    cell6.Text = "<p style='width:500px;padding:40px' class='overline'>" + jo1["description"].ToString() + "</ p>";
                    row.Cells.Add(cell6);

                    TableCell cell2 = new TableCell();
                    cell2.Text = "<p style='width:200px;padding:10px' class='overline'>" + jo1["author"].ToString() + "</ p>";
                    row.Cells.Add(cell2);

                    TableCell cell4 = new TableCell();
                    cell4.Text = "<p style='width:100px;padding:10px' class='overline'>" + jo1["library"].ToString() + "</ p>";
                    row.Cells.Add(cell4);

                    TableCell cell5 = new TableCell();
                    cell5.Text = "<p style='width:100px;padding:10px' class='overline'>" + jo1["place"].ToString() + ":" + jo1["label"].ToString() + "</ p>";
                    row.Cells.Add(cell5);
                    MyTable.Rows.Add(row);
                }
            }

            /*foreach(string i in filelist)
            {
                MessageBox.Show(i);
            }*/

        }
        public void Search(object sender, EventArgs e)
        {
            GetBook(book.Text);
        }
        public void LastPage(object sender, EventArgs e)
        {

            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                //因为redirect之后的代码是不执行的所以加不加return无所谓
                //return;
            }
            //首先判断登录是否过期，如果没有过期，就判断是否被顶下线
            CheckOtherLogin();
            //MessageBox.Show("count:" + count + "currentPage:" + currentPage);
            if ((int)Session["CurrentPage"] == 1)
            {
                MessageBox.Show("当前已经是第一页");
                GetBook(book.Text);
            }
            else
            {
                Session["CurrentPage"] = (int)Session["CurrentPage"] - 1;
                current.Text = Session["CurrentPage"] + "";
                GetBook(book.Text);
            }


        }
        public void NextPage(object sender, EventArgs e)
        {
            if ((int)Session["CurrentPage"] == (int)Session["Count"])
            {
                MessageBox.Show("当前已经是最后一页");
                GetBook(book.Text);

            }
            else
            {
                Session["CurrentPage"] = (int)Session["CurrentPage"] + 1;
                current.Text = Session["CurrentPage"] + "";
                GetBook(book.Text);
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