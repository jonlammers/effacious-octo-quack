﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace UI
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
                
        protected void Forgotpassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (UserNameTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty)
            {
                MessageBox.Show("Looks like you have not entered the username and/or the password. They are required for sign in.");
            }
            else
            {
                var userName = UserNameTextBox.Text;
                var password = PasswordTextBox.Text;
                if (UI.Validate.isUserNameValid(userName))
                {
                    Session["Username"] = userName;
                    var xSwitchObject = new Business.XSwitch(Global.ConnectionString, userName, string.Format("001|{0}|{1}", userName, password));
                    var output = xSwitchObject.resultP;
                    if (output.Contains("|"))
                    {
                        var dataRecieved = output.Split('|');
                        Session["UserId"] = dataRecieved[0];
                        Session["UserName"] = dataRecieved[1].Trim() + " " + dataRecieved[2].Trim();
                        switch (dataRecieved[3])
                        {
                            case "1":
                                Response.Redirect("Home.aspx");
                                break;
                            case "2":
                                Response.Redirect("MerchantHome.aspx");
                                break;
                            case "3":
                            case "4":
                                Response.Redirect("EmployeeHome.aspx");
                                break;
                            case "5":
                                Response.Redirect("AdminHome.aspx");
                                break;
                            default:
                                MessageBox.Show("Looks like we could not log you in. Please check the details you have entered.");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(output);
                    }
                }
                else
                {
                    MessageBox.Show("Looks like you have entered an invalid username.");
                }
            }
        }

        protected void ForgotPasswordLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("OTP.aspx");
        }
    }
}