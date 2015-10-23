using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Pendtxn
    {
        private static String DATE_FORMAT = "en-US";
        public Decimal cr_amt {get; set;}
        public String ref_no { get; set; }
        public String tran_date { get; set; }
        public String ac_no { get; set; }
        public String tran_pvgb { get; set; }
        public String tran_desc { get; set; }
        public String init_empid { get; set; }
        public Decimal dr_amt { get; set; }
        public Pendtxn() { }
        public Pendtxn(Decimal cr, String refno, String ac1, String ac2,
            String pvb, String trdesc, String initemp, Decimal dr)
        {
            DateTime dt = DateTime.Now;
            this.cr_amt = cr;
            this.ref_no = refno;
            this.tran_date = dt.Date.ToString(Pendtxn.DATE_FORMAT); ;
            this.ac_no = ac1;
            this.tran_pvgb = pvb;
            this.tran_desc = trdesc;
            this.init_empid = initemp;
            this.dr_amt = dr;
        }
    }
}
