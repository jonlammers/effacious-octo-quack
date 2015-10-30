﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class DeleteEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Global.IsPageAccessible(Page.Title))
                {
                    LoadEmployees(Session["UserId"].ToString());
                }
                else
                {
                    Response.Redirect("Error.aspx?error=NoAccess");
                }
            }
        }
        private void LoadEmployees(string internalUserId)
        {
            var xSwitchObject = new Business.XSwitch();

            try
            {
                var output = xSwitchObject.GetInternalUserList(Global.ConnectionString);
                if ((output == null) || (output.Tables[0].Rows.Count != 0))
                {
                    EmployeeDropDown.DataSource = output.Tables[0];
                    EmployeeDropDown.DataTextField = "emp_uname";
                    EmployeeDropDown.DataValueField = "emp_id";
                    EmployeeDropDown.DataBind();
                }
                else
                {
                    EmployeeDropDown.Items.Add(new ListItem { Text = "No Employees found.", Value = "0" });
                }
            }
            catch { }
        }

        protected void DeleteEmployee_Click(object sender, EventArgs e)
        {
            try { 
                string[] arglist = new String[24];
                int argIndex = 0;

                arglist[argIndex++] = Mnemonics.TxnCodes.TX_DELETE_USER_EMPLOYEE;
                arglist[argIndex++] = EmployeeDropDown.SelectedValue;


                var output = new Business.XSwitch(Global.ConnectionString, Session["UserId"].ToString(),
                    string.Format("{0}|{1}", arglist));

                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Profile Updated');", true);
            }
            catch { }
            Response.Redirect("AdminHome.aspx");
        }
    }
}