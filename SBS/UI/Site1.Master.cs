﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            if (Global.IsPageAccessible(Page.Title))
            {
                if (Session["UserName"] == null || Session["Access"] == null)
                    Response.Redirect("UserLogin.aspx");
                else
                {
                    UserNameLabel.InnerText = Session["UserName"].ToString();
                    if (Session["Access"].ToString() != "2") MakePaymentLink.Visible = false;

                    if (Session["Access"].ToString() != "2" && Session["Access"].ToString() != "1")
                    {
                        InternalTransferLink.Visible = false;
                        ExternalTransferLink.Visible = false;
                        AccountStatementLink.Visible = false;
                        CreateAccountLink.Visible = false;
                    }
                    if (Session["Access"].ToString() == "5")
                    {
                        TransactionMenu.Visible = false;
                        AccountStatementLink.Visible = false;
                        InternalTransferLink.Visible = false;
                        ExternalTransferLink.Visible = false;
                        CreditFormLink.Visible = false;
                        DebitFormLink.Visible = false;
                        CreateAccountLink.Visible = false;

                        AdminDropDown.Visible = true;
                    }
                }
            }
        }

        protected void SignOutLink_Click(object sender, EventArgs e)
        {
            DestroySession();
            Response.Redirect("UserLogin.aspx");
        }

        protected void DestroySession()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
        }

        protected void HomeLink_Click(object sender, EventArgs e)
        {
            if (Session["Access"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                switch (Session["Access"].ToString())
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
                        Response.Redirect("Error.aspx");
                        break;
                }
            }
        }

        protected void ProfileUpdateLink_Click(object sender, EventArgs e)
        {
            if (Session["Access"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                switch (Session["Access"].ToString())
                {
                    case "1":
                    case "2":
                        Response.Redirect("CustProfileUpdate.aspx");
                        break;
                    case "3":
                    case "4":
                    case "5":
                        ProfileUpdateLink.Visible = false;
                        break;
                    default:
                        Response.Redirect("Error.aspx");
                        break;
                }
            }
        }
    }
}