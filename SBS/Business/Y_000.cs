﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Data;
/*
 * ==========================
 *   USER LOGIN Transaction
 * ==========================
 */
namespace Business
{
    class Y_000
    {
        Entity.Cstm cstm;
        String TXID;
        String userid;
        String pwd;
        Data.Dber dberr;
        DataSet resultSet;
        public DataSet resultSetGet
        {
            get
            {
                return this.resultSet;
            }
        }
        String result;
        Boolean error;
        public Boolean errorGet
        {
            get
            {
                return this.error;
            }
        }
        public Y_000(String txid, String connectionString, String usr, String pwd)
        {
            this.error = false;
            this.TXID = txid;
            this.userid = usr;
            this.pwd = pwd;
            this.dberr = new Data.Dber();
            this.result = " ";
            if(processTransaction(connectionString, usr, pwd) != 0)
            {
                this.error = true;
            }
        }
        private int processTransaction(String connectionString, String usr, String pwd)
        {
            /*
             * initiate select query on CSTM, to fetch cus_no for the usr and pwd combination
             * "select cus_no from CSTM where user = usr and pwd = pwd"
             * Using the retrieved cus_no, fetch all account numbers from cstm, and store the retrieved acc nos. 
             * as "|" delimited string in result. For errors, update error with "true"
            */
            Entity.Cstm cs = Data.CstmD.Read(connectionString, usr, pwd, dberr);
            if (dberr.ifError())
            {
                Cp_Empm empm = new Cp_Empm(connectionString, usr, pwd, dberr);
                if (dberr.ifError())
                {
                    result = dberr.getErrorDesc(connectionString);
                    return -1;
                }
            }
            else
            {
                Cp_Actm ac = new Cp_Actm();
                this.resultSet = ac.fetchAccountsFromCusNo(connectionString, cs.cs_no, dberr);
                if (dberr.ifError())
                {
                    result = dberr.getErrorDesc(connectionString);
                    return -1;
                }
                if(this.resultSet == null)
                {
                    dberr.setError(Mnemonics.DbErrorCodes.TXERR_NO_USER);
                    result = dberr.getErrorDesc(connectionString);
                    return -1;
                }
            }
            return 0;
        }
        public String getOutput()
        {
            return this.result;
        }
    }
}
