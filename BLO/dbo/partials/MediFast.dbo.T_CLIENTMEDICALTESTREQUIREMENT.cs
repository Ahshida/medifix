using System;
using System.ComponentModel;
using DBO.Data.Attributes;
using ReportListing.Attributes;

namespace MediFast.dbo
{
    [AssignProperty("MedifastReceivedDate", typeof(DescriptionAttribute), "MediFast Received Date")]
    [AssignProperty("DespatchDate", typeof(DescriptionAttribute), "Despatch Date")]
    [AssignProperty("InsurerReceivedDate", typeof(DescriptionAttribute), "Insurer Received Date")]
    public partial class T_CLIENTMEDICALTESTREQUIREMENT
    {
        [DatabaseIgnore]
        [Description("MediFast Receive")]
        [ReferenceData("True:Yes;False:No")]
        public bool IsMediFastReceived
        {
            get { return this.MedifastReceivedDate.HasValue; }
            set
            {
                if (value)
                {
                    if (!this.MedifastReceivedDate.HasValue)
                        this.MedifastReceivedDate = DateTime.Now;
                }
                else
                    this.MedifastReceivedDate = null;
            }
        }
        [DatabaseIgnore]
        [Description("Despatch")]
        [ReferenceData("True:Yes;False:No")]
        public bool IsDespatched
        {
            get { return this.DespatchDate.HasValue; }
            set
            {
                if (value)
                {
                    if (!this.DespatchDate.HasValue)
                        this.DespatchDate = DateTime.Now;
                }
                else
                    this.DespatchDate = null;
            }
        }
        [DatabaseIgnore]
        [Description("Insurer Receive")]
        [ReferenceData("True:Yes;False:No")]
        public bool IsInsurerReceived
        {
            get { return this.InsurerReceivedDate.HasValue; }
            set
            {
                if (value)
                {
                    if (!this.InsurerReceivedDate.HasValue)
                        this.InsurerReceivedDate = DateTime.Now;
                }
                else
                    this.InsurerReceivedDate = null;
            }
        }
    }
}
