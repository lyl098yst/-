using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Windows;
using System.Security.Policy;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.cs;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["access_token"] == null)
            {
                string access_token = OperateCloud.get();
                //MessageBox.Show(access_token);
                Session["access_token"] = access_token;
            }
        }

        protected void getNum(object sender, EventArgs e)
        {
            //判断用户名是否已经被注册
            string num = name.Text;
            string access_token = (string)Session["access_token"];
            int length = registerADO.getUserName(num, access_token);
            //MessageBox.Show("length" + length);
            if (length == 1)
            {
                //MessageBox.Show("该用户名已被注册");
                name.Text = "";
                judgeName.Text = "该用户名已经被注册";
            }
            else if(length==0)
            {
                judgeName.Text = "该用户名可用";
            }
            
            /*string temp = jo["data"][0].ToString();//data里面第一个列表里面的json数据
            StringReader readertext1 = new StringReader(temp);
            JsonTextReader reader1 = new JsonTextReader(readertext1);
            JObject jo1 = (JObject)JToken.ReadFrom(reader1);*/
        }


        protected void getPhone(object sender, EventArgs e)
        {
            //判断用户名是否已经被注册
            string pho = phone.Text;
            if (pho.Length != 11)
            {
                MessageBox.Show("手机号不够11位");
                return;
            }
            string access_token = (string)Session["access_token"];
            int length = registerADO.getPhoneBindNum(pho,access_token);
            //MessageBox.Show("length" + length);
            if (length == 1)
            {
                //MessageBox.Show("该用户名已被注册");
                phone.Text = "";
                judge_phone.Text = "该手机号已经被绑定";
            }
            else if (length == 0)
            {
                judge_phone.Text = "该手机号可用";
            }
        }



        protected void regist(Object sender, EventArgs e)
        {

            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                //变量和ID不可重名
                UserInfo newUser = new UserInfo();
                newUser.num = name.Text;
                newUser.name = name_real.Text;
                newUser.pwd = Md5encryption.md5encryption(password.Text);
                newUser.phone = phone.Text;
                newUser.email = email.Text;
                for(int i = 0; i < newUser.name.Length; i++)
                {
                    if (newUser.name[i] == '\\' || newUser.name[i] == '*' || newUser.name[i] == '\"')
                    {
                        MessageBox.Show("姓名不可以包含敏感词汇");
                        return;
                    }
                }
                
                string access_token = (string)Session["access_token"];
                int errcode = registerADO.addNewUser(newUser,access_token);
                if(errcode == 0)
                {
                    MessageBox.Show("注册成功，前往登录");
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    MessageBox.Show("手机号绑定失败,错误码:" + errcode + ",请联系管理员************");
                }

            }
            
          
           
           
        }

    }
}