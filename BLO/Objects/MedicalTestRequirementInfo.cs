using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DBO.Data.Attributes;
using DBO.Data.Managers;
using MediFast.dbo;
using ReportListing.Attributes;

namespace BLO.Objects
{
    public class MedicalTestRequirementInfo
    {
        private T_MEDICALTESTREQUIREMENT _testRequirement;
        private T_CLIENTMEDICALTESTREQUIREMENT _clientTestRequirement;
        public MedicalTestRequirementInfo()
            : this(new T_MEDICALTESTREQUIREMENT(), new T_CLIENTMEDICALTESTREQUIREMENT())
        {
        }
        public MedicalTestRequirementInfo(T_MEDICALTESTREQUIREMENT testRequirement, T_CLIENTMEDICALTESTREQUIREMENT clientTestRequirement)
        {
            _testRequirement = testRequirement;
            _clientTestRequirement = clientTestRequirement;
            if (_clientTestRequirement != null)
                IsSelected = true;
            else
                _clientTestRequirement = new T_CLIENTMEDICALTESTREQUIREMENT();
        }

        [PrimaryKey]
        public int ID
        {
            get { return this._testRequirement.ID; }
            set { this._testRequirement.ID = value; }
        }

        [Description("Selected")]
        [ReferenceData("True:Yes;False:")]
        public bool IsSelected
        {
            get;
            set;
        }
        public string Category
        {
            get { return this._testRequirement.Category.GetDescription(); }
        }
        public string Code
        {
            get { return this._testRequirement.Code; }
        }
        public string Name
        {
            get { return this._testRequirement.Name; }
        }
        [Description("MediFast Received")]
        [ReferenceData("True:Yes;False:No")]
        public bool IsMediFastReceived
        {
            get { return this._clientTestRequirement.MedifastReceivedDate.HasValue; }
            set
            {
                if (value)
                {
                    if (!this._clientTestRequirement.MedifastReceivedDate.HasValue)
                        this._clientTestRequirement.MedifastReceivedDate = DateTime.Now;
                }
                else
                    this._clientTestRequirement.MedifastReceivedDate = null;
            }
        }
        [Description("Despatched")]
        [ReferenceData("True:Yes;False:No")]
        public bool IsDespatched
        {
            get { return this._clientTestRequirement.DespatchDate.HasValue; }
            set
            {
                if (value)
                {
                    if (!this._clientTestRequirement.DespatchDate.HasValue)
                        this._clientTestRequirement.DespatchDate = DateTime.Now;
                }
                else
                    this._clientTestRequirement.DespatchDate = null;
            }
        }
        [Description("Insurer Received")]
        [ReferenceData("True:Yes;False:No")]
        public bool IsInsurerReceived
        {
            get { return this._clientTestRequirement.InsurerReceivedDate.HasValue; }
            set
            {
                if (value)
                {
                    if (!this._clientTestRequirement.InsurerReceivedDate.HasValue)
                        this._clientTestRequirement.InsurerReceivedDate = DateTime.Now;
                }
                else
                    this._clientTestRequirement.InsurerReceivedDate = null;
            }
        }

        [DataType(DataType.MultilineText)]
        public string Remarks
        {
            get { return this._clientTestRequirement.Remarks; }
            set { this._clientTestRequirement.Remarks = value; }
        }
    }
}
