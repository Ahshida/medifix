using BLO.Objects;
using DBO.Data;
using MediFast.dbo;

namespace BLO.Managers
{
    public class MediFastManager : BLOObject
    {
        public void CreateDefaultMedifastTestRequirements()
        {
            var defaultItems = T_MEDICALTESTREQUIREMENT.Select(new WhereClause("InsurerID is null and Active=1"));
            var trans = new DBODataManager().BeginTransaction();
            foreach (var item in defaultItems)
            {
                item.InsurerID = this.Config.UserInfo.CompanyID;
                item.CreatedBy = this.Config.UserInfo.ID;
                item.ModifiedBy = this.Config.UserInfo.ID;
                trans.Insert(item);
            }
            trans.SubmitChanges();
        }

        public void CreateDefaultPolicyType()
        {
            var defaultItems = T_POLICYTYPE.Select(new WhereClause("InsurerID is null and Active=1"));
            var trans = new DBODataManager().BeginTransaction();
            foreach (var item in defaultItems)
            {
                item.InsurerID = this.Config.UserInfo.CompanyID;
                item.CreatedBy = this.Config.UserInfo.ID;
                item.ModifiedBy = this.Config.UserInfo.ID;
                trans.Insert(item);
            }
            trans.SubmitChanges();
        }
    }
}
