using DBO.Data.Managers;

namespace DBO.Data.Objects
{
    public class CommonDataBoundItem
    {
        public CommonDataBoundItem()
        {
        }
        public CommonDataBoundItem(object id, string description)
        {
            this.ID = id;
            this.Description = description;
        }
        private string _id;
        public object ID
        {
            get { return _id; }
            private set { _id = value.ConvertTo<string>(); }
        }
        public string Description
        {
            get;
            private set;
        }

        public override bool Equals(object obj)
        {
            var item = obj as CommonDataBoundItem;
            return object.Equals(this.ID, item.ID);
        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }

}
