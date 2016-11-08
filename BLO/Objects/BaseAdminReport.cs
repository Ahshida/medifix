using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLO.Interfaces;
using DBO.Data;
using DBO.Data.Managers;
using DBO.Data.Interfaces;

namespace BLO.Objects
{
    public class BaseAdminReport<T> : BLOListReport<T>, IBaseAdminReport
        where T : ITable
    {
        public int? ItemID { get; set; }
        public BaseAdminReport(Controller controller, int? itemID)
            : base(controller)
        {
            this.ItemID = ValidateItemID(itemID);
            this.AutoGenerateColumns = true;
            this.CanAdd = this.CanEdit = this.CanDelete = true;
            this.FormOnly = this.ItemID.HasValue;
        }
        protected virtual int? ValidateItemID(int? id)
        {
            return id;
        }

        protected override IEnumerable<T> GenerateItems()
        {
            if (this.ItemID.HasValue)
            {
                if (this.ItemID == 0)
                {
                    return ManipulateItems(new List<T>
                        {
                            BeforeCreateNewItem(Activator.CreateInstance<T>())
                        });
                }
                else
                    this.Conditions = new WhereClause("ID={0}", this.ItemID).Where;
            }
            else
            {
                this.Conditions = BuildCondition();
            }

            return base.GenerateItems();
        }

        public override IEnumerable<T> ManipulateItems(IEnumerable<T> items)
        {
            return base.ManipulateItems(items);
        }

        public string GetTitle()
        {
            return BuildTitle(this.GetObjects().FirstOrDefault().ConvertTo<T>());
        }
        protected virtual string BuildTitle(T item)
        {
            throw new Exception("BuildTitle is required to override");
        }
        protected virtual string BuildCondition()
        {
            return null;
        }
        protected virtual T BeforeCreateNewItem(T item)
        {
            return item;
        }
    }
}
