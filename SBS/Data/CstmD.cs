using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class CstmD
    {
        public static Cstm Read(string connectionString, string id, Dber dberr)
        {
            try
            {
                var customerMasterObject = new Cstm();
                var query = string.Format("select * from cstm where cs_no = {0}", id);
                var data = DbAccess.ExecuteQuery(connectionString, CommandType.Text, query);

                //assign the data object to customer master object
                if (data.Tables[0].Rows.Count > 0)
                {
                    customerMasterObject.cs_no = data.Tables[0].Rows[0]["cs_no"].ToString();
                    customerMasterObject.cs_type = data.Tables[0].Rows[0]["cs_type"].ToString();
                    customerMasterObject.cs_fname = data.Tables[0].Rows[0]["cs_fname"].ToString();
                    customerMasterObject.cs_mname = data.Tables[0].Rows[0]["cs_mname"].ToString();
                    customerMasterObject.cs_lname = data.Tables[0].Rows[0]["cs_lname"].ToString();
                    customerMasterObject.cs_access = data.Tables[0].Rows[0]["cs_access"].ToString();
                    customerMasterObject.cs_addr1 = data.Tables[0].Rows[0]["cs_addr1"].ToString();
                    customerMasterObject.cs_addr2 = data.Tables[0].Rows[0]["cs_addr2"].ToString();
                    customerMasterObject.cs_brnch = data.Tables[0].Rows[0]["cs_brnch"].ToString();
                    customerMasterObject.cs_city = data.Tables[0].Rows[0]["cs_city"].ToString();
                    customerMasterObject.cs_email = data.Tables[0].Rows[0]["cs_email"].ToString();
                    customerMasterObject.cs_phn = data.Tables[0].Rows[0]["cs_phn"].ToString();
                    customerMasterObject.cs_zip = data.Tables[0].Rows[0]["cs_zip"].ToString();
                    customerMasterObject.cs_usr = data.Tables[0].Rows[0]["cs_usr"].ToString();
                    customerMasterObject.cs_pw = data.Tables[0].Rows[0]["cs_pw"].ToString();
                    customerMasterObject.cs_secq1 = data.Tables[0].Rows[0]["cs_seq1"].ToString();
                    customerMasterObject.cs_secq2 = data.Tables[0].Rows[0]["cs_seq2"].ToString();
                    customerMasterObject.cs_secq3 = data.Tables[0].Rows[0]["cs_seq3"].ToString();
                    customerMasterObject.cs_state = data.Tables[0].Rows[0]["cs_state"].ToString();
                    customerMasterObject.cs_uid = data.Tables[0].Rows[0]["cs_uid"].ToString();
                    customerMasterObject.cs_ans1 = data.Tables[0].Rows[0]["cs_ans1"].ToString();
                    customerMasterObject.cs_ans2 = data.Tables[0].Rows[0]["cs_ans2"].ToString();
                    customerMasterObject.cs_ans3 = data.Tables[0].Rows[0]["cs_ans3"].ToString();



                    return customerMasterObject;
                }

                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet ReadAll(string connectionString, Dber dberr)
        {
            throw new NotImplementedException();


            //var customerMasterObject = new Cstm();
            var query = string.Format("select * from cstm");
            return  DbAccess.ExecuteQuery(connectionString, CommandType.Text, query);

        }

        public static int Create(string connectionString, Cstm dataObject, Dber dberr)
        {
            throw new NotImplementedException();
        }

        public static bool Update(string connectionString, Cstm dataObject, Dber dberr)
        {
            throw new NotImplementedException();
        }

        public static bool Delete(string connectionString, string id, Dber dberr)
        {
            throw new NotImplementedException();
        }
    }
}
