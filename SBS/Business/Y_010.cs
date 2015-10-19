﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Mnemonics;

namespace Business
{
    // Code for BALANE INQUIRY transaction
    class Y_010
    {
        String TXID = Mnemonics.TxnCodes.TX_BALINQ;
        String error;
        Cp_Actm acct;
        Cp_Txnm tx;
        Privilege pvg;
        Sequence seq;
        Data.Dber dberr;
        String result;
        public String resultP { get; set; }
        public Y_010(string connectionString, String acc_no)
        {
            try
            {
                dberr = new Data.Dber(); // change to Data.Dber
                processTransaction(connectionString, acc_no, dberr);
                // seq will generate and store transaction reference no.
                seq = new Sequence(TXID);
            }
            catch(Exception e)
            {
                error = e.ToString();
            }
        }
        private int processTransaction(string connectionString, String acc_no, Data.Dber dberr)
        {
            tx = new Cp_Txnm(connectionString, TXID, dberr);
            // Check if TXNM fetch for transaction type "010" is successful. Return if error encountered
            if (dberr.ifError())
            {
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            acct = new Cp_Actm(connectionString, acc_no, dberr);
            // Check if ACTM fetch for account number acc_no is successful. Return if error encountered
            if (dberr.ifError())
            {
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            // Verify if account has the privilege to execute the transaction
            pvg = new Privilege(tx.txnmP.tran_pvga, tx.txnmP.tran_pvgb, acct.actmP.ac_pvg);
            if(!pvg.verifyPrivilege(connectionString, dberr))
            {
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            // Store transaction in hisory table. Determine which history table to store in based on tx.txnmP.tran_fin_type
            /*if (tx.txnmP.tran_fin_type.Equals('Y'))
            {
                // Write to FINHIST table
                Entity.Finhist fhist = new Entity.Finhist();
                Data.FinhistD.Create(connectionString, fhist, dberr);
            }
            else
            {
                // Write to NFINHIST table
                Entity.Nfinhist nFHist = new Entity.Nfinhist();
                Data.NfinhistD.Create(connectionString, nFHist, dberr);
            }*/
            result = Convert.ToString(acct.actmP.ac_bal);
            return 0;
        }
        private Boolean updateTransactedData()
        {
            return true;
        }
        public String getOutput()
        {
            return result;
            /*if(dberr.ifError())
            {
                return dberr.getErrorDesc();
            }
            else
            {
                return Convert.ToString(acct.getBalance());
            }*/
        }
    }
}
