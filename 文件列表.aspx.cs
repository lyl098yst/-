using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using WebApplication1.cs;

namespace WebApplication1
{
    public partial class 上传文件 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                MessageBox.Show("登录已过期");
                Response.Redirect("Login.aspx");
                return;
            }
            if ((string)Session["roleid"] == "1")
            {
                //MessageBox.Show("管理员");
                Files.Columns[6].Visible = true;
                UpLoadDiv.Style.Add("display", "block");//显示
            }
            else
            {
                Files.Columns[6].Visible = false;
                UpLoadDiv.Style.Add("display", "none");//隐藏
            }
            CheckOtherLogin();
            if (!Page.IsPostBack)
            {
                getAllFile();
            }
            
        }
        public void getAllFile()
        {
            string access_token = (string)Session["access_token"];
            List<FileUpLoad> files = new List<FileUpLoad>();
            //先得到有多少数量
            int total = FileUpLoad.getNum(access_token);
            //再根据数量来一个一个读取信息
            JObject jo = FileUpLoad.getAllFile(access_token);
            for (int i = 0; i < total; i++)
            {

                FileUpLoad item = FileUpLoad.getItem(jo, i);
              
                files.Add(item);
            }
            Files.DataSource = files;
            Files.DataBind();
        }
        protected void Files_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = Files.SelectedIndex;
            //MessageBox.Show(index+"");
            string filename = Files.Rows[index].Cells[1].Text;
            string num = Files.Rows[index].Cells[2].Text;
            //MessageBox.Show(filename);
            //MessageBox.Show(num);
            string a = "files / " + num + " / " + filename;
            //MessageBox.Show(a);
            string filePath = Server.MapPath("files/"+num+"/"+filename);//路径

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                //MessageBox.Show("存在");
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
            }
        }
        public void UpLoad1(object sender, EventArgs e)
        {
            //指定上传文件在服务器上的保存路径 
            //取出所选文件的本地路径  
            string fullFileName = this.UpLoad.PostedFile.FileName;
            //从路径中截取出文件名  
            string fileName = fullFileName.Substring(fullFileName.LastIndexOf("\\") + 1);
            //限定上传文件的格式  
            string type = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1);
            int FileLen = UpLoad.PostedFile.ContentLength/1024;//获取上传文件的大小
            MessageBox.Show(FileLen + "");
            if (FileLen > 1024 * 1024 * 1)
            {
                MessageBox.Show("最大上传1G文件");
                return;
            }
                string savePath = Server.MapPath("~/files/"+Session["num"].ToString());
                //MessageBox.Show(savePath);
                //将文件保存在服务器中根目录下的files文件夹中  
                if (!System.IO.Directory.Exists(savePath))
                {
                    //MessageBox.Show("不存在");
                    System.IO.Directory.CreateDirectory(savePath);
                }
                //将filename换成时间。
                DateTime time=DateTime.Now;
                string year = time.Year.ToString();
                string month = time.Month.ToString();
                string day = time.Day.ToString();
                string hour = time.Hour.ToString();
                string minute = time.Minute.ToString();
                string second = time.Second.ToString();
                string filename_1 = year + month + day + hour + minute + second + fileName;
                //MessageBox.Show(filename_1);
                string saveFileName = savePath + "\\" + filename_1;
                UpLoad.SaveAs(saveFileName);
                //向数据库中存储相应通知的附件的目录  
                FileUpLoad file = new FileUpLoad();
                   //创建附件的实体  
                file.name = fileName;               //附件名  
                file.sendName = Session["name"].ToString();
                file.num = Session["num"].ToString();
                file.date = time;//上传文件的时间
                file.filename = filename_1;//用时间+文件名来表示
                //file.id = id;      
                string access_token = (string)Session["access_token"];
                if (file.upload(access_token))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('文件上传成功！');</script>");
                    getAllFile();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('文件上传失败！');</script>");
                }   
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