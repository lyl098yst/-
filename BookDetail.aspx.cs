using Newtonsoft.Json;
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
    public partial class BookDetail : System.Web.UI.Page
    {
        detail detailtemp= new detail();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                return;
            }
            CheckOtherLogin();
            if (!Page.IsPostBack)
            {
                string isbn = Request.QueryString["isbn"];
                //MessageBox.Show(isbn);
                showBookDetail(isbn);
            }
        }
        public void showBookDetail(string isbn)
        {

            string access_token = OperateCloud.get();
            detail item = BookADO.getBookByISBN(isbn,access_token);
            if (item == null)
            {
                MessageBox.Show("出错了");
            }
            else
            {
                //display(temp);
                string imageUrl = BookADO.getUrl(item.fileIDs[0],access_token);
               /* queryString = "{\"env\":\"" + envid + "\", \"file_list\": [{\"fileid\":\"" + item.fileIDs[0] + "\",\"max_age\":7200}]}";
                url = "https://api.weixin.qq.com/tcb/batchdownloadfile?access_token=" + access_token; //POST到网站
                Message = OperateCloud.Query(queryString, url);
                JObject jo2 = OperateCloud.GetJson(Message);
                pic.ImageUrl = jo2["file_list"][0]["download_url"].ToString();*/
                pic.ImageUrl = imageUrl;
                ISBN.Text = item._id;
                book_name.Text = item.name;
                author.Text = item.author;
                description.Text = item.description;
                remain.Text = item.remain.ToString();
                type.Text = item.type;
                price.Text = item.price;
                language.Text = item.language;
                publisher.Text = item.publisher;
                date.Text = item.date;
                library.Text = item.library;
                place.Text = item.place;
                label.Text = item.label;
            }
        }   
        public void update(object sender, EventArgs e)
        {
            detail book = new detail();
            string isbn= Request.QueryString["isbn"];
            book._id = Request.QueryString["isbn"];
            book.name = book_name.Text;
            book.author = author.Text;
            book.description = description.Text;
            book.type = type.Text;
            book.price = price.Text;
            book.language = language.Text;
            book.publisher = publisher.Text;
            book.date = date.Text;
            book.library = library.Text;
            book.place = place.Text;
            book.label = label.Text;
            string access_token = (string)Session["access_token"];
            int errcode = BookADO.updateBook(book,access_token);

            if (errcode == 0)
            {
                MessageBox.Show("保存成功");
                showBookDetail(isbn);
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
        public void delete(object sender, EventArgs e)
        {

            string isbn = Request.QueryString["isbn"];
            string access_token = (string)Session["access_token"];
            int errcode = BookADO.deleteBook(isbn,access_token);
            if (errcode == 0)
            {
                MessageBox.Show("删除成功");
                Response.Redirect("修改书籍.aspx");
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }

        public void Exit(object sender, EventArgs e)
        {
            Response.Redirect("修改书籍.aspx");
            //该方法直接跳转
            //Server.Transfer("WebForm1.aspx");
            //该方法显示之前已登录，再显示登录已过期
            //Server.Execute("register.aspx");
            //控件显示在一个页面

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