﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    class Y_026
    {
        Entity.Empm empm;
        Entity.Pendtxn pendTxn;
        Cp_Txnm tx;
        Data.Dber dberr;
        String result;
        public String resultP
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }
        String TXID;
        String cusNo;
        Boolean employee = false;
        Privilege pvg;
        Boolean error = false;
        public Y_026(String conectionString, String txid, String a, String b, String c, String d, String e,
            String f, String g, String h, String i, String j, String k, String l, String m, String n, String o, String p, String q,
            String loginAc)
        {
            dberr = new Data.Dber();
            this.TXID = txid;
            empm = new Entity.Empm();
            processTransaction(conectionString, a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, loginAc);
        }
        private int processTransaction(String connectionString, String a, String b, String c, String d, String e,
            String f, String g, String h, String i, string j, String k, String l, String m, String n, String o, String p, String q,
            String loginAc)
        {
            this.tx = new Cp_Txnm(connectionString, this.TXID, dberr);
            if (dberr.ifError())
            {
                resultP = dberr.getErrorDesc(connectionString);
                return -1;
            }
            empm.emp_no = a;
            empm.emp_fname = b;
            empm.emp_mname = c;
            empm.emp_lname = d;
            empm.emp_addr1 = e;
            empm.emp_addr2 = f;
            empm.emp_city = i;
            empm.emp_state = h;
            empm.emp_zip = g;
            empm.emp_brnch = j;
            empm.emp_phn = k;
            empm.emp_email = l;
            empm.emp_secq1 = m;
            empm.emp_ans1 = n;
            empm.emp_secq2 = o;
            empm.emp_ans2 = p;
            empm.emp_pass = q;
            int empId = Data.EmpmD.Create(connectionString, empm, dberr);
            if (dberr.ifError())
            {
                resultP = dberr.getErrorDesc(connectionString);
                return -1;
            }
            String mailResponse = "";
            if (!Security.OTPUtility.SendMail("SBS", "group2csefall2015@gmail.com", empm.emp_fname + empm.emp_mname + empm.emp_lname,
                empm.emp_email, "Update from SBS: you new User Id with us is: " + empId.ToString(), tx.txnmP.tran_desc))
            {
                mailResponse = "Mail sent.";
            }
            //-------------------------------
            resultP = "Successful!" + mailResponse;
            //resultP = "Successful!";
            return 0;
        }
    }
}
