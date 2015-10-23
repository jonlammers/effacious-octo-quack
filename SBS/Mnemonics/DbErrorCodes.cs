using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemonics
{
    public static class DbErrorCodes
    {
        public static String DBERR_TXNM_NOFIND = "999";
        public static String DBERR_ERRM_NOFIND = "998";
        public static String DBERR_ACTM_NOFIND = "997";

        public static String TXERR_INIT_PVG = "500";
        public static String TXERR_NO_DEBIT = "499";
        public static String TXERR_NO_CREDIT = "498";
        public static String TXERR_NEGATIVE_TRANSFER = "497";
        public static String TXERR_FMT_CUSFNAME = "496";
        public static String TXERR_FMT_CUSMNAME = "495";
        public static String TXERR_FMT_CUSLNAME = "494";
        public static String TXERR_FMT_CUSADDR1 = "493";
        public static String TXERR_FMT_CUSADDR2 = "492";
        public static String TXERR_FMT_CUSCITY = "491";
        public static String TXERR_FMT_CUSSTATE = "490";
        public static String TXERR_FMT_CUSZIP = "489";
        public static String TXERR_FMT_CUSBRNCH = "488";
        public static String TXERR_FMT_CUSPHN = "487";
        public static String TXERR_FMT_CUSEMAIL = "486";
        public static String TXERR_FMT_CUSUID = "485";
        public static String TXERR_FMT_CUSSECQ1 = "484";
        public static String TXERR_FMT_CUSANS1 = "483";
        public static String TXERR_FMT_CUSSECQ2 = "482";
        public static String TXERR_FMT_CUSANS2 = "481";
        public static String TXERR_FMT_CUSSECQ3 = "480";
        public static String TXERR_FMT_CUSANS3 = "479";
        public static String TXERR_FMT_CUSNO = "478";
        public static String TXERR_FMT_CUSTYPE = "477";
        public static String TXERR_NO_USER = "000";
    }
}