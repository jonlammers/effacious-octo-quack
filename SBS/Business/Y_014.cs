using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Business
{
    class Y_014
    {
        Entity.Cstm cstm;
        Boolean newInitiator;
        Data.Dber dberr;
        String result;
        public Y_014(String txid, String connectionString, String a, String b, String c, String d, String e, String f, 
            String g, String h, String i, String j, String k, String l, String m, String n, String o, String p, String q, String r, 
            String s, String t, String u, String loginAc)
        {
            dberr = new Data.Dber();
            newInitiator = false;
            if (!a.Equals(loginAc))
            {
                newInitiator = true;
            }
            cstm.cs_no = a;
            cstm.cs_type = b;
            cstm.cs_fname = c;
            cstm.cs_mname = d;
            cstm.cs_lname = e;
            cstm.cs_addr1 = f;
            cstm.cs_addr2 = g;
            cstm.cs_city = h;
            cstm.cs_state = i;
            cstm.cs_zip = j;
            cstm.cs_brnch = k;
            cstm.cs_phn = l;
            cstm.cs_email = m;
            cstm.cs_uid = n;
            cstm.cs_secq1 = o;
            cstm.cs_ans1 = p;
            cstm.cs_secq2 = q;
            cstm.cs_ans2 = r;
            cstm.cs_secq3 = s;
            cstm.cs_ans3 = t;
            cstm.cs_access = u;
            processTransaction(connectionString, cstm);
        }
        /*
         * 1 = check for integer data
         * 2 = check for aphabetic data
         * 3 = check for special characters;
         */
        private Boolean verifyInputType(int type, String verificationData)
        {
            int countInt = 0, countAlpha = 0, countSpecial = 0;
            for (int i = 0; i < verificationData.Length; i++ )
            {
                if (Char.IsNumber(verificationData[i])) countInt++;
                else
                {
                    if (Char.IsLetter(verificationData[i])) countAlpha++;
                    else
                    {
                        countSpecial++;
                    }
                }
            }
            if (type == 1)
            {
                if (countInt > 0 && countAlpha == 0 && countSpecial == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (type == 2)
            {
                if (countInt == 0 && countAlpha > 0 && countSpecial == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (type == 3)
            {
                if (countInt > 0 || countAlpha > 0 || countSpecial > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        private int processTransaction(String connectionString, Entity.Cstm cstm)
        {
            if (!verifyInputType(1, cstm.cs_no))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSNO);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(2, cstm.cs_type))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSTYPE);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(2, cstm.cs_fname))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSFNAME);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(2, cstm.cs_mname))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSMNAME);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(2, cstm.cs_lname))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSLNAME);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_addr1))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSADDR1);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_addr2))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSADDR2);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_city))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSCITY);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(2, cstm.cs_state))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSSTATE);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(1, cstm.cs_zip))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSZIP);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(1, cstm.cs_brnch))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSBRNCH);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(1, cstm.cs_phn))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSPHN);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_email))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSEMAIL);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(1, cstm.cs_uid))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSUID);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_secq1))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSSECQ1);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_ans1))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSANS1);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_secq2))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSSECQ2);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_ans2))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSANS2);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_secq3))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSSECQ3);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            if (!verifyInputType(3, cstm.cs_ans3))
            {
                this.dberr.setError(Mnemonics.DbErrorCodes.TXERR_FMT_CUSANS3);
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            Data.CstmD.Update(connectionString, this.cstm, this.dberr);
            if (dberr.ifError())
            {
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            return 0;
        }
        public String getOutput()
        {
            return this.result;
        }
    }
}