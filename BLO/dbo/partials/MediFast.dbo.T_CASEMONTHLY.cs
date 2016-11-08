using System.ComponentModel;
using DBO.Data.Attributes;
using System;

namespace MediFast.dbo
{
    public partial class T_CASEMONTHLY
    {
        [DatabaseIgnore]
        public DateTime Date
        {
            get { return new DateTime(this.Year, this.Month, 1).AddMonths(1).AddDays(-1); }
        }
    }
}
