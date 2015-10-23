using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace UI
{
    public partial class CheckBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Business.XSwitch tran = new Business.XSwitch(Global.ConnectionString, "010|1", "1");
                TextBox1.Text = tran.resultP;
            }
            catch (Exception ex)
            {
                TextBox1.Text = ex.Message;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}