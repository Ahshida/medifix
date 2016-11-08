using DBO.Data.Interfaces;

namespace DBO.Data.Objects
{
    public class MultiLingual : IMultiLingual
    {
        public byte ID
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Name_ar
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }
    }
}
