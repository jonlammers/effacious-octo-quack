using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Security;

namespace UI
{
    public partial class MakePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserId"] == null || Session["Access"] == null)
                    Response.Redirect("UserLogin.aspx");
                if (Session["Access"].ToString() == "5")
                    Response.Redirect("AdminHome.aspx");
                else if (Session["Access"].ToString() == "1")
                    Response.Redirect("Home.aspx");
                else if (Session["Access"].ToString() == "3" || Session["Access"].ToString() == "4")
                    Response.Redirect("EmployeeHome.aspx");

                if (!IsPostBack)
                {
                    if (Global.IsPageAccessible(Page.Title))
                    {
                        if (Session["Access"].ToString() == "2")
                        {
                            LoadCustomers(Session["UserId"].ToString());
                            LoadCurrentUserAccounts();
                        }
                    }
                    else
                    {
                        Response.Redirect("Error.aspx?error=NoAccess");
                    }
                }
            }
            catch { }
        }

        private void LoadCustomers(string internalUserId)
        {
            var xSwitchObject = new Business.XSwitch();
            try
            {
                var output = xSwitchObject.getMerchantAccessibleCustomerData(Global.ConnectionString, Session["UserId"].ToString());
                if ((output == null) || (output.Tables[0].Rows.Count != 0))
                {
                    CustomerDropDown.DataSource = output.Tables[0];
                    CustomerDropDown.DataTextField = "cs_uname";
                    CustomerDropDown.DataValueField = "cs_no";
                    CustomerDropDown.DataBind();
                    //AccountList.InnerHtml = GetAccountListHtml(output.resultSet);
                    LoadAccounts(CustomerDropDown.SelectedValue, byPass: true);
                }
                else
                {
                    CustomerDropDown.Items.Add(new ListItem { Text = "You have access to no customers", Value = "0" });
                }
            }
            catch (Exception ex)
            { }
        }

        private void LoadAccounts(string externalUserId, bool byPass = false)
        {
            try
            {
                if (Session["Access"].ToString() == "2" || byPass)
                {
                    var output = new Business.XSwitch(Global.ConnectionString, Session["UserId"].ToString(), string.Format("009|{0}", externalUserId));
                    if (output == null)
                        Response.Redirect("Error.aspx");

                    if (output.resultSet.Tables.Count > 0)
                    {
                        if (output.resultSet.Tables[0].Rows.Count != 0)
                        {
                            FromDropdown.Items.Clear();
                            FromDropdown.DataSource = output.resultSet.Tables[0];
                            FromDropdown.DataTextField = "ac_no";
                            FromDropdown.DataValueField = "ac_no";
                            FromDropdown.DataBind();
                            //AccountList.InnerHtml = GetAccountListHtml(output.resultSet);
                        }
                        else
                        {
                            FromDropdown.Items.Add(new ListItem { Text = "No Accounts Found", Value = "0" });
                        }
                    }
                    else
                    {
                        FromDropdown.Items.Clear();
                        FromDropdown.Items.Add(new ListItem { Text = "No Accounts Found", Value = "0" });
                    }

                }
            }
            catch {
                
            }
        }

        private void LoadCurrentUserAccounts()
        {
            try
            {
                if (Session["Access"].ToString() == "2")
                {
                    var output = new Business.XSwitch(Global.ConnectionString, Session["UserId"].ToString(), string.Format("009|{0}", Session["UserId"].ToString()));
                    if (output == null)
                        Response.Redirect("Error.aspx");

                    if (output.resultSet.Tables.Count > 0)
                    {
                        if (output.resultSet.Tables[0].Rows.Count != 0)
                        {
                            ToDropdown.Items.Clear();
                            ToDropdown.DataSource = output.resultSet.Tables[0];
                            ToDropdown.DataTextField = "ac_no";
                            ToDropdown.DataValueField = "ac_no";
                            ToDropdown.DataBind();
                            //AccountList.InnerHtml = GetAccountListHtml(output.resultSet);
                        }
                        else
                        {
                            FromDropdown.Items.Add(new ListItem { Text = "No Accounts Found", Value = "0" });
                        }
                    }
                    else
                    {
                        FromDropdown.Items.Clear();
                        FromDropdown.Items.Add(new ListItem { Text = "No Accounts Found", Value = "0" });
                    }

                }
            }
            catch
            {

            }
        }
        private string generatedotp;
        
        protected void MakePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Access"].ToString() == "2")
                {
                    if (FromDropdown.SelectedValue == "0")
                    {
                        //MessageBox.Show("Select the Account from which an amount has to be Debited");
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Select the Amount from which an amount has to be Debited');", true);
                    }

                    if (ToDropdown.SelectedValue == "0")
                    {
                        //MessageBox.Show("Select the Account to which the amount has to be Debited");
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Select the Account to which the amount has to be Debited');", true);
                    }
                    else if (Amount.Text == "" || !UI.Validate.isAmountValid(Amount.Text))
                    {
                        //MessageBox.Show("Amount cannot be empty, and amount accepts only decimal values.");
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Amount cannot be empty, and amount accepts only decimal values.');", true);
                    }
                    else
                    {
                        var amount = Convert.ToDecimal(Amount.Text);
                        var xsw = new Business.XSwitch();
                        var customer = new Entity.Cstm();
                        customer = xsw.getExternalUserDataFromUserName(Global.ConnectionString, CustomerDropDown.SelectedValue.ToString());
                        var customeremail = customer.cs_email;
                        var customername = customer.cs_fname + " " + customer.cs_lname;
                        _otpService = new OTPService(Session["UserId"].ToString() + Session["UserName"].ToString());
                        generatedotp = _otpService.GenerateOTP(Session["UserName"].ToString(), email: Session["UserEmail"].ToString(), notifyByEmail: false);
                        const string subject = "Your OTP from the most secure bank, SBS, ";
                        string body = string.Format("Hello {0}, <br /> <br />Your <b>OTP</b> from the most secure bank: <br /> {1} <br /><br /> Regards, <br /> SBS Team.", "", generatedotp);
                        OTPUtility.SendMail("Group 2", "group2csefall2015@gmail.com", customername, customeremail, subject, body);
                        OTPDiv.Visible = true;
                        MakePaymentButton.Visible = false;

                   }
                }

                else
                {
                   
                }
            }
            catch { }

        }
        private static OTPService _otpService;
       
        private void ProcessTransaction(decimal amount)
        {
            try
            {
                //TODO:
                //var output = new Business.XSwitch(Global.ConnectionString, Session["UserId"].ToString(), string.Format("011|{0}| |{1}|{2}| ", FromDropdown.SelectedValue, amount, Session["Access"]));
                //MessageBox.Show(string.Format("The debit was successful. Your current balance is {0}", output.resultP));
                var output = new Business.XSwitch(Global.ConnectionString, FromDropdown.SelectedValue, string.Format("021|{0}|{1}|{2}|{3}| ", FromDropdown.SelectedValue, ToDropdown.SelectedValue, amount, Session["Access"].ToString()));
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('" + output.resultP + "');", true);

            }
            catch { }
        }

        private void ResetPage()
        {
            Amount.Text = string.Empty;
            FromDropdown.SelectedIndex = 0;
            CustomerDropDown.SelectedIndex = 0;
            OTPDiv.Visible = false;
            ToDropdown.SelectedIndex = 0;
            MakePaymentButton.Visible = true;
        }

        protected void CustomerDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccounts(CustomerDropDown.SelectedValue, byPass: true);
        }
        
        protected void VerifyButton_Click(object sender, EventArgs e)
        {
            if (_otpService.VerifyOTP(OTPTextBox.Text))
            {
                ProcessTransaction(Convert.ToDecimal(Amount.Text));
                ResetPage();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Enter the correct OTP.');", true);
            }
        }

        protected void ResendOTPLink_Click(object sender, EventArgs e)
        {
            var amount = Convert.ToDecimal(Amount.Text);
            var xsw = new Business.XSwitch();
            var customer = new Entity.Cstm();
            customer = xsw.getExternalUserDataFromUserName(Global.ConnectionString, CustomerDropDown.SelectedValue.ToString());
            var customeremail = customer.cs_email;
            var customername = customer.cs_fname + " " + customer.cs_lname;
            _otpService = new OTPService(Session["UserId"].ToString() + Session["UserName"].ToString());
            generatedotp = _otpService.GenerateOTP(Session["UserName"].ToString(), email: Session["UserEmail"].ToString(), notifyByEmail: false);
            const string subject = "Your OTP from the most secure bank, SBS, ";
            string body = string.Format("Hello {0}, <br /> <br />Your <b>OTP</b> from the most secure bank: <br /> {1} <br /><br /> Regards, <br /> SBS Team.", "", generatedotp);
            OTPUtility.SendMail("Group 2", "group2csefall2015@gmail.com", customername, customeremail, subject, body);
        }
    }
}
