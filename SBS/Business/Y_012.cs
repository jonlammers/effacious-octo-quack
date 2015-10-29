using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mnemonics;

/*
 * Code for CREDIT transaction
*/

namespace Business
{
    class Y_012
    {
        String TXID;
        String result;
        Boolean error = false;
        Boolean pvgBypassed;
        public Boolean pvgBypassedP
        {
            get
            {
                return this.pvgBypassed;
            }
            set
            {
                this.pvgBypassed = value;
            }
        }
        Data.Dber dberr;
        // only needed for wrapper transactions
        public Boolean txnErrorP
        {
            get
            {
                return dberr.ifError();
            }
        }

        public String resultP
        {
            get { return result; }
            set { result = value; }
        }

        Cp_Actm acct, acct_init;
        Cp_Txnm tx;
        Cp_Empm em;
        Privilege pvg;
        Sequence seq;
        Int32 initPvg;
        Decimal changeAmount;
        String loginAcc;
        Boolean newInitiator = false; // if the person transacting is different from the initiator, like in case of pending txns
        public Y_012(String txid, String connectionString, String acc_no, Decimal amount, String initPvg, String refno, String loginAc)
        {
            this.dberr = new Data.Dber();
            try
            {
                if (amount <= 0)
                {
                    dberr.setError(Mnemonics.DbErrorCodes.TXERR_NEGATIVE_TRANSFER);
                    this.result = dberr.getErrorDesc(connectionString);
                    this.error = true;
                }
                else
                {
                    this.initPvg = Convert.ToInt32(initPvg);
                    this.TXID = txid;
                    this.changeAmount = amount;
                    seq = new Sequence(TXID);
                    this.loginAcc = loginAc;
                    if (!acc_no.Equals(loginAc))
                    {
                        newInitiator = true;
                    }
                    if (processTransaction(connectionString, acc_no, this.initPvg, loginAc) != 0)
                    {
                        this.error = true;
                    }
                    
                    //pvg = new Privilege();
                    // seq will generate and store transaction reference no.
                }
            }
            catch(Exception e)
            {
                this.error = true;
                result = e.ToString();
            }
        }
        public Boolean basicValidationError()
        {
            return error;
        }
        private int processTransaction(string connectionString, String acc_no, Int32 initPvg, String loginAc)
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
            String initEmpNumber = "0";
            String initCustomer = "0";
            if (this.newInitiator)
            {
                acct_init = new Cp_Actm(connectionString, loginAc, dberr);
                // Check if ACTM fetch for account number acc_no is successful. Return if error encountered
                if (dberr.ifError())
                {
                    dberr = new Data.Dber();
                    em = new Cp_Empm(connectionString, loginAc, dberr);
                    if (dberr.ifError())
                    {
                        result = dberr.getErrorDesc(connectionString);
                        return -1;
                    }
                    initEmpNumber = em.empmP.emp_no;
                }
                else
                {
                    initCustomer = this.acct_init.actmP.cs_no1;
                }
            }
            else
            {
                this.acct_init = this.acct;
                initCustomer = this.acct_init.actmP.cs_no1;
            }
            if (newInitiator)
            {
                pvg = new Privilege(this.tx.txnmP.tran_pvga, this.tx.txnmP.tran_pvgb, Convert.ToInt32(em.empmP.emp_pvg));
            }
            else
            {
                pvg = new Privilege(this.tx.txnmP.tran_pvga, this.tx.txnmP.tran_pvgb, Convert.ToInt32(acct.actmP.ac_pvg));
            }
            // Verify if account has the privilege to execute the transaction
            //if (acct_init.actmP.ac_pvg == initPvg)
            //{
                //pvg = new Privilege(tx.txnmP.tran_pvga, tx.txnmP.tran_pvgb, acct_init.actmP.ac_pvg);
                if (!pvg.verifyInitPrivilege(dberr))
                {
                    result = dberr.getErrorDesc(connectionString);
                    return -1;
                }
                if (!pvg.verifyApprovePrivilege())
                {
                    String inData = this.TXID + "|" + acct.actmP.ac_no + "| |" + this.changeAmount.ToString();
                    if (pvg.writeToPendingTxns(
                        connectionString,               /* connection string */
                        acct.actmP.ac_no,               /* account 1 */
                        "0",                            /* account 2 */
                        initCustomer,                   /* customer number */
                        tx.txnmP.tran_pvgb.ToString(),  /* transaction approve privilege */
                        tx.txnmP.tran_desc,             /* transaction description */
                        initEmpNumber,                  /* initiating employee number */
                        0,                              /* debit amount */
                        this.changeAmount,              /* credit amount */
                        tx.txnmP.tran_id,               /* transaction id (not tran code) */
                        inData,                         /* incoming transaction string in XSwitch */
                        dberr                           /* error tracking object */
                        ) != 0)
                    {
                        resultP = dberr.getErrorDesc(connectionString);
                        return -1;
                    }
                    resultP = Mnemonics.DbErrorCodes.MSG_SENT_FOR_AUTH;
                    return 0;
                }
            //}
                else
                {
                    this.pvgBypassedP = true;
                }
            // Update new balance in ACTM
            acct.addBalance(connectionString, this.changeAmount, dberr);
            if (dberr.ifError())
            {
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            this.resultP = "Successful! Your new balance: " + acct.resultP;
            // Store transaction in hisory table. Determine which history table to store in based on tx.txnmP.tran_fin_type
            if (tx.txnmP.tran_fin_type.Equals("Y"))
            {
                // Write to FINHIST table
                Entity.Finhist fhist = new Entity.Finhist(this.acct.actmP.ac_no, "0", this.tx.txnmP.tran_desc,
                    0, changeAmount, Convert.ToString(this.acct.actmP.ac_bal), "0", "0","0");
                Data.FinhistD.Create(connectionString, fhist, dberr);
            }
            else
            {
                // Write to NFINHIST table
                Entity.Nfinhist nFhist = new Entity.Nfinhist(this.acct.actmP.ac_no, "0", this.tx.txnmP.tran_desc, "0", "0","0");
                Data.NfinhistD.Create(connectionString, nFhist, dberr);
            }
            if (dberr.ifError())
            {
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            
            // ----------- send email ------------------
            Entity.Cstm cstm = Data.CstmD.Read(connectionString, acct.actmP.cs_no1, dberr);
            if (dberr.ifError())
            {
                result = dberr.getErrorDesc(connectionString);
                return -1;
            }
            String mailResponse = "";
            if (!Security.OTPUtility.SendMail("SBS", "group2csefall2015@gmail.com", cstm.cs_fname + cstm.cs_mname + cstm.cs_lname,
                cstm.cs_email, "Update from SBS", tx.txnmP.tran_desc + acct.actmP.ac_bal))
            {
                mailResponse = "Mail sent.";
            }
            // -----------------------------------------
            resultP = "Transaction Successful. Your new account balance is $" + acct.actmP.ac_bal + " " + mailResponse;
            //-------------------------------------------
            return 0; // remove later
        }
        public String getOutput()
        {
            return this.result;
        }
    }
}
