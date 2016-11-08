using System;
using System.Data;

namespace DBO.Data.Objects
{
    public class DBODataParameter : IDbDataParameter
    {
        public byte Precision
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public byte Scale
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Size
        {
            get;
            set;
        }

        public DbType DbType
        {
            get;
            set;
        }

        public ParameterDirection Direction
        {
            get;
            set;
        }

        public bool IsNullable
        {
            get { throw new NotImplementedException(); }
        }

        public string ParameterName
        {
            get;
            set;
        }

        public string SourceColumn
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DataRowVersion SourceVersion
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object Value
        {
            get;
            set;
        }
    }

}
