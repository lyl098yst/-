using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows;
using ThirdParty.Json.LitJson;
using WebApplication1.cs;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected string envid = "";
        static string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstr);
        protected void Page_Load(object sender, EventArgs e)
        {
            //必须判断IsPostBack
            //否则会出现无法修改用户名的错误（修改之后相当于没有修改）
            if (Session["login"] != null)
            {
                MessageBox.Show("已登录,请先退出");
                Response.Redirect("Search.aspx");
            }
            else
            {
                envid = ConfigurationManager.AppSettings["envid"];
                //MessageBox.Show("未登录状态");
            }
            if (!IsPostBack)
            {
                shouji.Style.Add("display", "none");//隐藏
                zhanghao.Style.Add("display", "bolck");//显示   
                //获取cookies看一下有没有登陆过
                if (Request.Cookies["UserName"] == null || Request.Cookies["PassWord"] == null)
                {
                    login_name.Text = "";
                }
                else
                {
                    //有cookies就直接显示用户名，不显示密码防止他人使用电脑登入
                    login_name.Text = Request.Cookies["UserName"].Value;
                    //pwd.Value = Request.Cookies["PassWord"].Value;
                }
                if (Request.Cookies["Phone"] == null)
                {
                    phone.Text = "";
                }
                else
                {
                    phone.Text = Request.Cookies["Phone"].Value;
                }
            }
            //获取access_token来操作数据库
            if (Session["access_token"] == null)
            {
                string access_token = OperateCloud.get();
                //MessageBox.Show(access_token);
                Session["access_token"] = access_token;
            }
            
        }

       public string getenv()
        {
            return envid;
        }
        protected void login(object sender, EventArgs e)
        {
            //防止服务器请求过多
            if ((int)Application["UserOnLineCnt"] == 100)
            {
                //MessageBox.Show("当前登录人数爆满,请稍后再试"+(int)Application["UserOnLineCnt"]);
                MessageBox.Show("当前登录人数爆满,请稍后再试");
                return;
            }


            if (login_name.Text == "" || login_password.Text == "")
            {
                MessageBox.Show("请输入用户名和密码再登陆");
                return;
            }

            //在这里判断是否是当前登录
            //首先获取当前用户的IP地址
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            //MessageBox.Show(ipAddress);
            //获取表中的IP地址
            try
            {
                Hashtable hOnline = (Hashtable)Application["num_id"];//读取全局变量
                if (hOnline != null)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    while (idE.MoveNext())
                    {
                        if (idE.Key != null && idE.Key.ToString().Equals(login_name.Text))//如果当前用户已经登录，
                        {
                            
                            //说明找到了，获取num上次登录的IP地址
                            string ipaddress = idE.Value.ToString();
                            if(ipaddress == (string)Request.ServerVariables["REMOTE_ADDR"])
                            {
                                //无需操作
                            }
                            else
                            {
                                MessageBox.Show("异地登陆，请使用手机号登录");
                                return;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    hOnline = new Hashtable();//MessageBox.Show("不存在");
                }
                //如果可以到这里就更新一下。不存在或者登陆成功更新
                hOnline[login_name.Text] = Request.ServerVariables["REMOTE_ADDR"];
                Application.Lock();
                Application["num_id"] = hOnline;
            }
            catch (Exception ee)
            {
                Application.UnLock();
            }
            finally
            {
                Application.UnLock();
            }


            //query字段
            string num = login_name.Text;
            string access_token = (string)Session["access_token"];
            UserInfo loginUser = registerADO.getUserByNum(num, access_token);
            if (loginUser != null)
            {
                if(loginUser.pwd == Md5encryption.md5encryption(login_password.Text.Trim()))
                {
                    MessageBox.Show("登陆成功");
                    Session["num"] = loginUser.num;
                    Session["pwd"] = loginUser.pwd;
                    Session["name"] = loginUser.name;
                    Session["email"] = loginUser.email;
                    Session["phone"] = loginUser.phone;
                    Session["roleid"] = loginUser.roleid;
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                    return;
                }
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
                return;
            }
               
            
            if (Request.Cookies["UserName"] == null || Request.Cookies["PassWord"] == null)
                            {
                                //没有cookies说明没有登陆过，新建一个Cookies
                                //值设置为当前登录名，过期时间设置为3天,添加到Response中去
                                HttpCookie userCookie = new HttpCookie("UserName", login_name.Text.Trim());
                                //userCookie.Value = "defaultName";
                                //过期日期为日期时间
                                //在当前天数上添加3天
                                userCookie.Expires = DateTime.Now.AddDays(3);
                                Response.Cookies.Add(userCookie);
                                HttpCookie pwdCookie = new HttpCookie("PassWord", Md5encryption.md5encryption(login_password.Text.Trim()));
                               
                                userCookie.Expires = DateTime.Now.AddDays(3);
                                Response.Cookies.Add(pwdCookie);
                               }
                            else
                            {
                                //将数据存入cookies中
                                //这个cookie只有一个值所以直接Value
                                //该cookie更新值和过期时间即可
                                Response.Cookies["UserName"].Value = login_name.Text.Trim();
                                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(3);
                                Response.Cookies["PassWord"].Value = Md5encryption.md5encryption(login_password.Text.Trim());
                                Response.Cookies["PassWord"].Expires = DateTime.Now.AddDays(3);
            }


            Session["login"] = true;
            //MessageBox.Show(Session.SessionID);
            Session.Timeout = 20;
            //Session["cunchu1"] = "可以存储多个数据";
            //Session.Add("cunchu2", "这样也能存");
            //Session.Abandon();

            //会有出现死锁的问题
            try
            {
                /* List<string> list = loginList.getUserList();
                 if (list == null)
                 {
                     loginList.addUser(login_name.Text);
                 }
                 else
                 {
                     for (int i = 0; i < list.Count; i++)
                     {
                         MessageBox.Show(list[i]);
                     }
                 }*/


                //利用Session来实现单点登录
                Hashtable hOnline = (Hashtable)Application["Online"];//读取全局变量
                if (hOnline != null)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    string strKey = "";
                    while (idE.MoveNext())
                    {
                        if (idE.Value != null && idE.Value.ToString().Equals(login_name.Text))//如果当前用户已经登录，
                        {
                            //already login            
                            strKey = idE.Key.ToString();
                            hOnline[strKey] = "XX";//将当前用户已经在全局变量中的值设置为XX,代表被顶下线
                            //这个SessionID为XX说明这个SessionID被顶下线，重新登陆重新写入SessionID
                            //每次登录都判断，因为用户名唯一，SessionID唯一，所以这个查找唯一//每次只存最近登录的
                            break;
                        }
                    }
                }
                else
                {
                    hOnline = new Hashtable();
                }

                hOnline[Session.SessionID] = login_name.Text;//初始化当前用户的
                Application.Lock();
                Application["Online"] = hOnline;
                Application["UserOnLineCnt"] = (int)Application["UserOnLineCnt"] + 1;
            }
            catch (Exception ee)
            {
                Application.UnLock();
            }
            finally
            {
                Response.Redirect("Search.aspx");
            }


                /*}
            }
        }
       if (flag)
        {
            MessageBox.Show("无效用户名或密码");
            login_name.Text = "";
            login_password.Text = "";
        }
        sdr.Close();
        conn.Close();*/
           

        }
        protected void login2(object sender, EventArgs e)
        {
            //MessageBox.Show("手机号登录");
            if ((int)Application["UserOnLineCnt"] == 100)
            {
                //MessageBox.Show("当前登录人数爆满,请稍后再试"+(int)Application["UserOnLineCnt"]);
                MessageBox.Show("当前登录人数爆满,请稍后再试");
                return;
            }

            if (phone.Text == "" || code.Text == "")
            {
                MessageBox.Show("请输入手机号和验证码登录");
                return;
            }

            //query字段
            string pho = phone.Text;
            string access_token = (string)Session["access_token"];
            UserInfo loginUser = registerADO.getUserByPho(pho, access_token);
            if (loginUser != null)
            {
                    MessageBox.Show("登陆成功");
                    Session["num"] = loginUser.num;
                    Session["pwd"] = loginUser.pwd;
                    Session["name"] = loginUser.name;
                    Session["email"] = loginUser.email;
                    Session["phone"] = loginUser.phone;
                    Session["roleid"] = loginUser.roleid;
            }
            else
            {
                MessageBox.Show("该手机号未注册");
                return;
            }
            if (Request.Cookies["Phone"] == null)
            {
                HttpCookie userCookie = new HttpCookie("Phone", phone.Text.Trim());
                userCookie.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Add(userCookie);
            }
            else
            {
                Response.Cookies["Phone"].Value = phone.Text.Trim();
            }
            Session["login"] = true;
            Session.Timeout = 20;
            try
            {

                //利用Session来实现单点登录
                Hashtable hOnline = (Hashtable)Application["Online"];//读取全局变量
                if (hOnline != null)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    string strKey = "";
                    while (idE.MoveNext())
                    {
                        if (idE.Value != null && idE.Value.ToString().Equals(login_name.Text))//如果当前用户已经登录，
                        {
                            //already login            
                            strKey = idE.Key.ToString();
                            hOnline[strKey] = "XX";//将当前用户已经在全局变量中的值设置为XX,代表被顶下线
                            //这个SessionID为XX说明这个SessionID被顶下线，重新登陆重新写入SessionID
                            //每次登录都判断，因为用户名唯一，SessionID唯一，所以这个查找唯一
                            //每次只存最近登录的
                            break;
                        }
                    }
                }
                else
                {
                    hOnline = new Hashtable();
                }

                hOnline[Session.SessionID] = login_name.Text;//初始化当前用户的
                Application.Lock();
                Application["Online"] = hOnline;
                Application["UserOnLineCnt"] = (int)Application["UserOnLineCnt"] + 1;

            }
            catch (Exception ee)
            {
                Application.UnLock();
            }
            finally
            {
                Response.Redirect("Search.aspx");
            }


        }




        public void zhanghao_login(object sender, EventArgs e)
        {
            shouji.Style.Add("display", "none");//隐藏
            zhanghao.Style.Add("display", "bolck");//显示
        }
        public void phone_login(object sender, EventArgs e)
        {
            zhanghao.Style.Add("display", "none");//隐藏
            shouji.Style.Add("display", "bolck");//显示
        }
        protected void go_register(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
        public string getEncryption()
        {
            MessageBox.Show(login_password.Text);
            return Md5encryption.md5encryption(login_password.Text.Trim());
        }
     /*   public void test(object sender, EventArgs e)
        {
            try
            {
                string num = login_name.Text;
                string queryString = "{\"env\":\"lylwzyxad-0gihxbo72145c330\", \"query\": \"db.collection(\\\"student\\\").where({num:\\\"" + num + "\\\"}).limit(10).get()\"}";
                byte[] byteData = Encoding.UTF8.GetBytes(queryString);
                string access_token = (string)Session["access_token"];
                string url = "https://api.weixin.qq.com/tcb/databasequery?access_token=" + access_token; //POST到网站
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json;charset=UTF-8";
                webRequest.ContentLength = byteData.Length;
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(byteData, 0, byteData.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string Message = php.ReadToEnd();
                //textBox1.Text = Message;
                php.Close();
                JObject jo = OperateCloud.GetJson(Message);
                string temp = jo["data"][0].ToString();//data里面第一个列表里面的json数据               
                JObject jo1 = OperateCloud.GetJson(temp);
                MessageBox.Show(jo1["pwd"].ToString());
                if (Md5encryption.md5encryption(login_password.Text.Trim()) == jo1["pwd"].ToString())
                {
                    MessageBox.Show("登陆成功");
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }
            }catch(Exception ee)
            {
                MessageBox.Show("用户名或密码错误");
            }
        }*/
    }

}

/*
 

1.最常用的页面跳转（原窗口被替代）：Response.Redirect("XXX.aspx");

2.利用url地址打开本地网页或互联网：Respose.Write("<script language='javascript'>window.open('"+ url+"');</script>");

3.原窗口保留再新打开另一个页面（浏览器可能阻止，需要解除）：Response.Write("<script>window.open('XXX.aspx','_blank')</script>");

4.效果同1中的另一种写法：Response.Write("<script>window.location='XXX.aspx'</script>");

5.也是原窗口被替代的 （常用于传递session变量的页面跳转）：Server.Transfer("XXX.aspx");

6.原窗口保留，以对话框形式打开新窗口：Response.Write("<script>window.showModelessDialog('XXX.aspx')</script>");

7.对话框形式打开新窗口，原窗口被代替：Response.Write("<script>window.showModelDialog('XXX.aspx')</script>");

8.打开简洁窗口：Respose.Write("<script language='javascript'>window.open('"+url+"','','resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no, menu=no');</script>");

9.利用vs2008端口：System.Diagnostics.Process.Start(http://localhost:3210/系统管理员.aspx);

 */
