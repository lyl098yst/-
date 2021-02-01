using DocumentFormat.OpenXml.Office.CustomUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using WebApplication1.cs;
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Employee(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}
namespace WebApplication1
{
    public partial class 添加书籍 : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                return;
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
        protected void upload(object sender, EventArgs e)
        {
            getBookInfo();
        }

        public void getBookInfo()
        { //判断是否上传了文件 
            if (fileUpload.HasFile)
            {
                /*//指定上传文件在服务器上的保存路径 
                string savePath = Server.MapPath("~/upload");
                Response.Write(savePath);
                //检查服务器上是否存在这个物理路径，如果不存在则创建 
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath = savePath + "\\" + fileUpload.FileName;
                fileUpload.SaveAs(savePath);//保存文件     */
                string fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension != ".json")
                {
                    MessageBox.Show("目前仅支持json格式文件");
                    return;

                }                
                int FileLen = fileUpload.PostedFile.ContentLength;//获取上传文件的大小
                if (FileLen > 102400)
                {
                    MessageBox.Show("最大上传100MB文件");
                    return;
                }
                byte[] input = new byte[FileLen];
                //MessageBox.Show(FileLen + "");
                Stream UpLoadStream = fileUpload.PostedFile.InputStream;
                UpLoadStream.Read(input, 0, FileLen);
                UpLoadStream.Position = 0;
                StreamReader r = new StreamReader(UpLoadStream);
                try
                {
                    string access_token = (string)Session["access_token"];
                    string json = r.ReadToEnd();
                    //MessageBox.Show(json);
                    List<Book> items = JsonConvert.DeserializeObject<List<Book>>(json);
                    List<Book> data = new List<Book>();

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (BookADO.addBook(items[i], access_token))
                        {
                            data.Add(items[i]);
                        }
                    }
                    addbook.DataSource = data;
                    addbook.DataBind();
                    MessageBox.Show("导入成功");
                }
                catch (Exception ee)
                {
                    MessageBox.Show("导入过程出现错误" + ee.Message);

                }
                finally
                {
                    r.Close();
                    UpLoadStream.Close();

                }
                //add(fileUpload.FileName);
            }
            else
            {
                MessageBox.Show("请先选择json文件");
            }
        }

      
        protected void addbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = addbook.SelectedIndex;
            //MessageBox.Show(index+"");
            string str = addbook.Rows[index].Cells[0].Text;
            //MessageBox.Show(str);
            Response.Redirect("BookDetail.aspx?isbn="+str);
        }
        public void confirm(object sender, EventArgs e)
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