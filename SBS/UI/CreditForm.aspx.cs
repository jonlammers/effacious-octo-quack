﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace UI
{
    public partial class CreditForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("UserLogin.aspx");

            if (!IsPostBack)
            {
                if (Global.IsPageAccessible(Page.Title))
                {
                    LoadAccounts();
                }
                else
                {
                    Response.Redirect("Error.aspx?error=NoAccess");
                }
            }
        }

        private void LoadAccounts()
        {
            var output = new Business.XSwitch(Global.ConnectionString, Session["Username"].ToString(), string.Format("009|{0}", Session["UserId"].ToString()));
            if (output == null)
                Response.Redirect("Error.aspx");


            if (output.resultSet.Tables[0].Rows.Count != 0)
            {
                ToDropdown.DataSource = output.resultSet.Tables[0];
                ToDropdown.DataTextField = "ac_no";
                ToDropdown.DataValueField = "ac_no";
                ToDropdown.DataBind();
                //AccountList.InnerHtml = GetAccountListHtml(output.resultSet);
            }
            else
            {
                ToDropdown.Items.Add("No Accounts Found");
            }
        }

        protected void CreditButton_Click(object sender, EventArgs e)
        {
            if (ToDropdown.SelectedValue == null)
            {
                MessageBox.Show("Select the Account from which an amount has to be Debited");
            }
            else if (Amount.Text == "" || !UI.Validate.isAmountValid(Amount.Text))
            {
                MessageBox.Show("Amount cannot be empty, and amount accepts only decimal values.");
            }
            else
            {
                var amount = Convert.ToDouble(Amount.Text);
                var output = new Business.XSwitch(Global.ConnectionString, Session["UserId"].ToString(), string.Format("012|{0}| |{1}", ToDropdown.SelectedValue, amount));
                MessageBox.Show(output.resultP);
            }
        }
    }
}