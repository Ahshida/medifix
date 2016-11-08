using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using DBO.Data;
using DBO.Data.Attributes;
using DBO.Data.Interfaces;
using DBO.Data.Managers;
using DBO.Data.Objects;

namespace MediFast.dbo
{
    public class MediFastClass<T> : BaseClass<DBODataManager, T> where T : ITable
    {
    }

    public partial class E_BANK : MediFastClass<E_BANK>, ITable, IEnumCode, IName, IActive
    {
        public E_BANK()
            : base()
        {
            OnCreated();
        }

        public static E_BANK SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_BANK.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(50)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

    }
    public partial class E_CASESTATUS : MediFastClass<E_CASESTATUS>, ITable, IEnumCode, IName, IActive
    {
        public E_CASESTATUS()
            : base()
        {
            OnCreated();
        }

        public static E_CASESTATUS SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_CASESTATUS.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(30)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

    }
    public partial class E_COMPANYTYPE : MediFastClass<E_COMPANYTYPE>, ITable, IEnumCode, IName, IActive
    {
        public E_COMPANYTYPE()
            : base()
        {
            OnCreated();
        }

        public static E_COMPANYTYPE SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_COMPANYTYPE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnSeqing(int value);
        partial void OnSeqed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(20)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(45)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int Seq

        private int _Seq;
        [Required]
        [HasDefault("((0))")]
        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (!object.Equals(_Seq, value))
                {
                    OnSeqing(value);
                    _Seq = value;
                    IsModified = true;
                    OnSeqed();
                }
            }
        }

        #endregion

    }
    public partial class E_COUNTRY : MediFastClass<E_COUNTRY>, ITable, IEnumCode, IName, IActive
    {
        public E_COUNTRY()
            : base()
        {
            OnCreated();
        }

        public static E_COUNTRY SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_COUNTRY.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnSeqing(int value);
        partial void OnSeqed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(20)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(45)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int Seq

        private int _Seq;
        [Required]
        [HasDefault("((0))")]
        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (!object.Equals(_Seq, value))
                {
                    OnSeqing(value);
                    _Seq = value;
                    IsModified = true;
                    OnSeqed();
                }
            }
        }

        #endregion

    }
    public partial class E_EVENTLOGTYPE : MediFastClass<E_EVENTLOGTYPE>, ITable, IEnumCode, IName, IActive
    {
        public E_EVENTLOGTYPE()
            : base()
        {
            OnCreated();
        }

        public static E_EVENTLOGTYPE SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_EVENTLOGTYPE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(50)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

    }
    public partial class E_INSURERCASESTATUS : MediFastClass<E_INSURERCASESTATUS>, ITable, IEnumCode, IName, IActive
    {
        public E_INSURERCASESTATUS()
            : base()
        {
            OnCreated();
        }

        public static E_INSURERCASESTATUS SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_INSURERCASESTATUS.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(30)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

    }
    public partial class E_PANELTYPE : MediFastClass<E_PANELTYPE>, ITable, IEnumCode, IName, IActive
    {
        public E_PANELTYPE()
            : base()
        {
            OnCreated();
        }

        public static E_PANELTYPE SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_PANELTYPE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnSeqing(int value);
        partial void OnSeqed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(20)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(45)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int Seq

        private int _Seq;
        [Required]
        [HasDefault("((0))")]
        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (!object.Equals(_Seq, value))
                {
                    OnSeqing(value);
                    _Seq = value;
                    IsModified = true;
                    OnSeqed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("Country", typeof(E_COUNTRY), "ID")]
    public partial class E_STATE : MediFastClass<E_STATE>, ITable, IEnumCode, IName, IActive
    {
        public E_STATE()
            : base()
        {
            OnCreated();
        }

        public static E_STATE SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_STATE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCountrying(BLO.Objects.Enums.MediFast.Enum_COUNTRY value);
        partial void OnCountryed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnSeqing(int value);
        partial void OnSeqed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_COUNTRY Country

        private BLO.Objects.Enums.MediFast.Enum_COUNTRY _Country;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_COUNTRY Country
        {
            get { return _Country; }
            set
            {
                if (!object.Equals(_Country, value))
                {
                    OnCountrying(value);
                    _Country = value;
                    IsModified = true;
                    OnCountryed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(20)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(45)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int Seq

        private int _Seq;
        [Required]
        [HasDefault("((0))")]
        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (!object.Equals(_Seq, value))
                {
                    OnSeqing(value);
                    _Seq = value;
                    IsModified = true;
                    OnSeqed();
                }
            }
        }

        #endregion

    }
    public partial class E_TESTREQUIREMENTCATEGORY : MediFastClass<E_TESTREQUIREMENTCATEGORY>, ITable, IEnumCode, IName, IActive
    {
        public E_TESTREQUIREMENTCATEGORY()
            : base()
        {
            OnCreated();
        }

        public static E_TESTREQUIREMENTCATEGORY SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_TESTREQUIREMENTCATEGORY.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(20)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(45)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

    }
    public partial class E_TITLE : MediFastClass<E_TITLE>, ITable, IEnumCode, IName, IActive
    {
        public E_TITLE()
            : base()
        {
            OnCreated();
        }

        public static E_TITLE SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_TITLE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnSeqing(int value);
        partial void OnSeqed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(20)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(45)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int Seq

        private int _Seq;
        [Required]
        [HasDefault("((0))")]
        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (!object.Equals(_Seq, value))
                {
                    OnSeqing(value);
                    _Seq = value;
                    IsModified = true;
                    OnSeqed();
                }
            }
        }

        #endregion

    }
    public partial class E_USERROLE : MediFastClass<E_USERROLE>, ITable, IEnumCode, IName, IActive
    {
        public E_USERROLE()
            : base()
        {
            OnCreated();
        }

        public static E_USERROLE SelectExact(byte _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return E_USERROLE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(byte value);
        partial void OnIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();

        #endregion

        #region byte ID

        private byte _ID;
        [PrimaryKey(true, false)]
        [Required]
        public byte ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(20)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(45)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

    }
    public partial class SYSDIAGRAMS : MediFastClass<SYSDIAGRAMS>, ITable
    {
        public SYSDIAGRAMS()
            : base()
        {
            OnCreated();
        }

        public static SYSDIAGRAMS SelectExact(int _diagram_id)
        {
            if (_diagram_id <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("diagram_id={0}", _diagram_id).Where);
            return SYSDIAGRAMS.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void Onnameing(string value);
        partial void Onnameed();
        partial void Onprincipal_iding(int value);
        partial void Onprincipal_ided();
        partial void Ondiagram_iding(int value);
        partial void Ondiagram_ided();
        partial void Onversioning(int? value);
        partial void Onversioned();
        partial void Ondefinitioning(byte[] value);
        partial void Ondefinitioned();

        #endregion

        #region string name

        private string _name;
        [Required]
        [MaxLength(256)]
        public string name
        {
            get { return _name; }
            set
            {
                if (!object.Equals(_name, value))
                {
                    Onnameing(value);
                    _name = value;
                    IsModified = true;
                    Onnameed();
                }
            }
        }

        #endregion

        #region int principal_id

        private int _principal_id;
        [Required]
        public int principal_id
        {
            get { return _principal_id; }
            set
            {
                if (!object.Equals(_principal_id, value))
                {
                    Onprincipal_iding(value);
                    _principal_id = value;
                    IsModified = true;
                    Onprincipal_ided();
                }
            }
        }

        #endregion

        #region int diagram_id

        private int _diagram_id;
        [PrimaryKey]
        [Required]
        public int diagram_id
        {
            get { return _diagram_id; }
            set
            {
                if (!object.Equals(_diagram_id, value))
                {
                    Ondiagram_iding(value);
                    _diagram_id = value;
                    IsModified = true;
                    Ondiagram_ided();
                }
            }
        }

        #endregion

        #region int? version

        private int? _version;
        public int? version
        {
            get { return _version; }
            set
            {
                if (!object.Equals(_version, value))
                {
                    Onversioning(value);
                    _version = value;
                    IsModified = true;
                    Onversioned();
                }
            }
        }

        #endregion

        #region byte[] definition

        private byte[] _definition;
        public byte[] definition
        {
            get { return _definition; }
            set
            {
                if (!object.Equals(_definition, value))
                {
                    Ondefinitioning(value);
                    _definition = value;
                    IsModified = true;
                    Ondefinitioned();
                }
            }
        }

        #endregion

    }
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    public partial class T_ATTACHMENT : MediFastClass<T_ATTACHMENT>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator
    {
        public T_ATTACHMENT()
            : base()
        {
            OnCreated();
        }

        public static T_ATTACHMENT SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_ATTACHMENT.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnPathing(string value);
        partial void OnPathed();
        partial void OnDescriptioning(string value);
        partial void OnDescriptioned();
        partial void OnSortOrdering(int value);
        partial void OnSortOrdered();
        partial void OnIsLettering(bool value);
        partial void OnIsLettered();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region string Path

        private string _Path;
        [Required]
        [MaxLength(300)]
        public string Path
        {
            get { return _Path; }
            set
            {
                if (!object.Equals(_Path, value))
                {
                    OnPathing(value);
                    _Path = value;
                    IsModified = true;
                    OnPathed();
                }
            }
        }

        #endregion

        #region string Description

        private string _Description;
        [MaxLength(300)]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (!object.Equals(_Description, value))
                {
                    OnDescriptioning(value);
                    _Description = value;
                    IsModified = true;
                    OnDescriptioned();
                }
            }
        }

        #endregion

        #region int SortOrder

        private int _SortOrder;
        [Required]
        public int SortOrder
        {
            get { return _SortOrder; }
            set
            {
                if (!object.Equals(_SortOrder, value))
                {
                    OnSortOrdering(value);
                    _SortOrder = value;
                    IsModified = true;
                    OnSortOrdered();
                }
            }
        }

        #endregion

        #region bool IsLetter

        private bool _IsLetter;
        [Required]
        public bool IsLetter
        {
            get { return _IsLetter; }
            set
            {
                if (!object.Equals(_IsLetter, value))
                {
                    OnIsLettering(value);
                    _IsLetter = value;
                    IsModified = true;
                    OnIsLettered();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("BankID", typeof(E_BANK), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("InsurerID", typeof(T_COMPANY), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    [ForeignKey("Title", typeof(E_TITLE), "ID")]
    public partial class T_BANKBRANCH : MediFastClass<T_BANKBRANCH>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IActive
    {
        public T_BANKBRANCH()
            : base()
        {
            OnCreated();
        }

        public static T_BANKBRANCH SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_BANKBRANCH.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnInsurerIDing(int value);
        partial void OnInsurerIDed();
        partial void OnBankIDing(BLO.Objects.Enums.MediFast.Enum_BANK value);
        partial void OnBankIDed();
        partial void OnBranching(string value);
        partial void OnBranched();
        partial void OnTelephoneing(string value);
        partial void OnTelephoneed();
        partial void OnFaxing(string value);
        partial void OnFaxed();
        partial void OnTitleing(BLO.Objects.Enums.MediFast.Enum_TITLE? value);
        partial void OnTitleed();
        partial void OnContactPersoning(string value);
        partial void OnContactPersoned();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int InsurerID

        private int _InsurerID;
        [Required]
        public int InsurerID
        {
            get { return _InsurerID; }
            set
            {
                if (!object.Equals(_InsurerID, value))
                {
                    OnInsurerIDing(value);
                    _InsurerID = value;
                    IsModified = true;
                    OnInsurerIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_BANK BankID

        private BLO.Objects.Enums.MediFast.Enum_BANK _BankID;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_BANK BankID
        {
            get { return _BankID; }
            set
            {
                if (!object.Equals(_BankID, value))
                {
                    OnBankIDing(value);
                    _BankID = value;
                    IsModified = true;
                    OnBankIDed();
                }
            }
        }

        #endregion

        #region string Branch

        private string _Branch;
        [MaxLength(100)]
        public string Branch
        {
            get { return _Branch; }
            set
            {
                if (!object.Equals(_Branch, value))
                {
                    OnBranching(value);
                    _Branch = value;
                    IsModified = true;
                    OnBranched();
                }
            }
        }

        #endregion

        #region string Telephone

        private string _Telephone;
        [MaxLength(20)]
        public string Telephone
        {
            get { return _Telephone; }
            set
            {
                if (!object.Equals(_Telephone, value))
                {
                    OnTelephoneing(value);
                    _Telephone = value;
                    IsModified = true;
                    OnTelephoneed();
                }
            }
        }

        #endregion

        #region string Fax

        private string _Fax;
        [MaxLength(20)]
        public string Fax
        {
            get { return _Fax; }
            set
            {
                if (!object.Equals(_Fax, value))
                {
                    OnFaxing(value);
                    _Fax = value;
                    IsModified = true;
                    OnFaxed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_TITLE? Title

        private BLO.Objects.Enums.MediFast.Enum_TITLE? _Title;
        public BLO.Objects.Enums.MediFast.Enum_TITLE? Title
        {
            get { return _Title; }
            set
            {
                if (!object.Equals(_Title, value))
                {
                    OnTitleing(value);
                    _Title = value;
                    IsModified = true;
                    OnTitleed();
                }
            }
        }

        #endregion

        #region string ContactPerson

        private string _ContactPerson;
        [MaxLength(100)]
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set
            {
                if (!object.Equals(_ContactPerson, value))
                {
                    OnContactPersoning(value);
                    _ContactPerson = value;
                    IsModified = true;
                    OnContactPersoned();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    public partial class T_BANKBRANCH_BAK_20150914 : MediFastClass<T_BANKBRANCH_BAK_20150914>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IActive
    {
        public T_BANKBRANCH_BAK_20150914()
            : base()
        {
            OnCreated();
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnInsurerIDing(int value);
        partial void OnInsurerIDed();
        partial void OnBankIDing(byte value);
        partial void OnBankIDed();
        partial void OnBranching(string value);
        partial void OnBranched();
        partial void OnTelephoneing(string value);
        partial void OnTelephoneed();
        partial void OnFaxing(string value);
        partial void OnFaxed();
        partial void OnTitleing(byte? value);
        partial void OnTitleed();
        partial void OnContactPersoning(string value);
        partial void OnContactPersoned();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int InsurerID

        private int _InsurerID;
        [Required]
        public int InsurerID
        {
            get { return _InsurerID; }
            set
            {
                if (!object.Equals(_InsurerID, value))
                {
                    OnInsurerIDing(value);
                    _InsurerID = value;
                    IsModified = true;
                    OnInsurerIDed();
                }
            }
        }

        #endregion

        #region byte BankID

        private byte _BankID;
        [Required]
        public byte BankID
        {
            get { return _BankID; }
            set
            {
                if (!object.Equals(_BankID, value))
                {
                    OnBankIDing(value);
                    _BankID = value;
                    IsModified = true;
                    OnBankIDed();
                }
            }
        }

        #endregion

        #region string Branch

        private string _Branch;
        [MaxLength(100)]
        public string Branch
        {
            get { return _Branch; }
            set
            {
                if (!object.Equals(_Branch, value))
                {
                    OnBranching(value);
                    _Branch = value;
                    IsModified = true;
                    OnBranched();
                }
            }
        }

        #endregion

        #region string Telephone

        private string _Telephone;
        [MaxLength(20)]
        public string Telephone
        {
            get { return _Telephone; }
            set
            {
                if (!object.Equals(_Telephone, value))
                {
                    OnTelephoneing(value);
                    _Telephone = value;
                    IsModified = true;
                    OnTelephoneed();
                }
            }
        }

        #endregion

        #region string Fax

        private string _Fax;
        [MaxLength(20)]
        public string Fax
        {
            get { return _Fax; }
            set
            {
                if (!object.Equals(_Fax, value))
                {
                    OnFaxing(value);
                    _Fax = value;
                    IsModified = true;
                    OnFaxed();
                }
            }
        }

        #endregion

        #region byte? Title

        private byte? _Title;
        public byte? Title
        {
            get { return _Title; }
            set
            {
                if (!object.Equals(_Title, value))
                {
                    OnTitleing(value);
                    _Title = value;
                    IsModified = true;
                    OnTitleed();
                }
            }
        }

        #endregion

        #region string ContactPerson

        private string _ContactPerson;
        [MaxLength(100)]
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set
            {
                if (!object.Equals(_ContactPerson, value))
                {
                    OnContactPersoning(value);
                    _ContactPerson = value;
                    IsModified = true;
                    OnContactPersoned();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("Bank", typeof(E_BANK), "ID")]
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    [ForeignKey("Title", typeof(E_TITLE), "ID")]
    public partial class T_BANKINFO : MediFastClass<T_BANKINFO>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator
    {
        public T_BANKINFO()
            : base()
        {
            OnCreated();
        }

        public static T_BANKINFO SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_BANKINFO.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnBanking(BLO.Objects.Enums.MediFast.Enum_BANK? value);
        partial void OnBanked();
        partial void OnBranching(string value);
        partial void OnBranched();
        partial void OnTelephoneing(string value);
        partial void OnTelephoneed();
        partial void OnFaxing(string value);
        partial void OnFaxed();
        partial void OnTitleing(BLO.Objects.Enums.MediFast.Enum_TITLE? value);
        partial void OnTitleed();
        partial void OnContactPersoning(string value);
        partial void OnContactPersoned();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();
        partial void OnEmailing(string value);
        partial void OnEmailed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_BANK? Bank

        private BLO.Objects.Enums.MediFast.Enum_BANK? _Bank;
        public BLO.Objects.Enums.MediFast.Enum_BANK? Bank
        {
            get { return _Bank; }
            set
            {
                if (!object.Equals(_Bank, value))
                {
                    OnBanking(value);
                    _Bank = value;
                    IsModified = true;
                    OnBanked();
                }
            }
        }

        #endregion

        #region string Branch

        private string _Branch;
        [MaxLength(100)]
        public string Branch
        {
            get { return _Branch; }
            set
            {
                if (!object.Equals(_Branch, value))
                {
                    OnBranching(value);
                    _Branch = value;
                    IsModified = true;
                    OnBranched();
                }
            }
        }

        #endregion

        #region string Telephone

        private string _Telephone;
        [MaxLength(20)]
        public string Telephone
        {
            get { return _Telephone; }
            set
            {
                if (!object.Equals(_Telephone, value))
                {
                    OnTelephoneing(value);
                    _Telephone = value;
                    IsModified = true;
                    OnTelephoneed();
                }
            }
        }

        #endregion

        #region string Fax

        private string _Fax;
        [MaxLength(20)]
        public string Fax
        {
            get { return _Fax; }
            set
            {
                if (!object.Equals(_Fax, value))
                {
                    OnFaxing(value);
                    _Fax = value;
                    IsModified = true;
                    OnFaxed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_TITLE? Title

        private BLO.Objects.Enums.MediFast.Enum_TITLE? _Title;
        public BLO.Objects.Enums.MediFast.Enum_TITLE? Title
        {
            get { return _Title; }
            set
            {
                if (!object.Equals(_Title, value))
                {
                    OnTitleing(value);
                    _Title = value;
                    IsModified = true;
                    OnTitleed();
                }
            }
        }

        #endregion

        #region string ContactPerson

        private string _ContactPerson;
        [MaxLength(100)]
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set
            {
                if (!object.Equals(_ContactPerson, value))
                {
                    OnContactPersoning(value);
                    _ContactPerson = value;
                    IsModified = true;
                    OnContactPersoned();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

        #region string Email

        private string _Email;
        [Required]
        [MaxLength(200)]
        public string Email
        {
            get { return _Email; }
            set
            {
                if (!object.Equals(_Email, value))
                {
                    OnEmailing(value);
                    _Email = value;
                    IsModified = true;
                    OnEmailed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    public partial class T_CASEINVOICE : MediFastClass<T_CASEINVOICE>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IName
    {
        public T_CASEINVOICE()
            : base()
        {
            OnCreated();
        }

        public static T_CASEINVOICE SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CASEINVOICE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnCaseMonthlying(int value);
        partial void OnCaseMonthlyed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnOrderNoing(string value);
        partial void OnOrderNoed();
        partial void OnCompleteDateing(DateTime value);
        partial void OnCompleteDateed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnIDNoing(string value);
        partial void OnIDNoed();
        partial void OnPolicyNoing(string value);
        partial void OnPolicyNoed();
        partial void OnPayableing(Currency value);
        partial void OnPayableed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int CaseMonthly

        private int _CaseMonthly;
        [Required]
        public int CaseMonthly
        {
            get { return _CaseMonthly; }
            set
            {
                if (!object.Equals(_CaseMonthly, value))
                {
                    OnCaseMonthlying(value);
                    _CaseMonthly = value;
                    IsModified = true;
                    OnCaseMonthlyed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region string OrderNo

        private string _OrderNo;
        [Required]
        [MaxLength(20)]
        public string OrderNo
        {
            get { return _OrderNo; }
            set
            {
                if (!object.Equals(_OrderNo, value))
                {
                    OnOrderNoing(value);
                    _OrderNo = value;
                    IsModified = true;
                    OnOrderNoed();
                }
            }
        }

        #endregion

        #region DateTime CompleteDate

        private DateTime _CompleteDate;
        [Required]
        [DateOnly]
        public DateTime CompleteDate
        {
            get { return _CompleteDate; }
            set
            {
                if (!object.Equals(_CompleteDate, value))
                {
                    OnCompleteDateing(value);
                    _CompleteDate = value;
                    IsModified = true;
                    OnCompleteDateed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(200)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region string IDNo

        private string _IDNo;
        [Required]
        [MaxLength(20)]
        public string IDNo
        {
            get { return _IDNo; }
            set
            {
                if (!object.Equals(_IDNo, value))
                {
                    OnIDNoing(value);
                    _IDNo = value;
                    IsModified = true;
                    OnIDNoed();
                }
            }
        }

        #endregion

        #region string PolicyNo

        private string _PolicyNo;
        [Required]
        [MaxLength(20)]
        public string PolicyNo
        {
            get { return _PolicyNo; }
            set
            {
                if (!object.Equals(_PolicyNo, value))
                {
                    OnPolicyNoing(value);
                    _PolicyNo = value;
                    IsModified = true;
                    OnPolicyNoed();
                }
            }
        }

        #endregion

        #region Currency Payable

        private Currency _Payable;
        [Required]
        public Currency Payable
        {
            get { return _Payable; }
            set
            {
                if (!object.Equals(_Payable, value))
                {
                    OnPayableing(value);
                    _Payable = value;
                    IsModified = true;
                    OnPayableed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("CaseInvoiceID", typeof(T_CASEINVOICE), "ID")]
    [ForeignKey("CaseMedicalTestRequirementID", typeof(T_CASEMEDICALTESTREQUIREMENT), "ID")]
    public partial class T_CASEINVOICEITEM : MediFastClass<T_CASEINVOICEITEM>, ITable
    {
        public T_CASEINVOICEITEM()
            : base()
        {
            OnCreated();
        }

        public static T_CASEINVOICEITEM SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CASEINVOICEITEM.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnCaseInvoiceIDing(int value);
        partial void OnCaseInvoiceIDed();
        partial void OnCaseMedicalTestRequirementIDing(int value);
        partial void OnCaseMedicalTestRequirementIDed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int CaseInvoiceID

        private int _CaseInvoiceID;
        [Required]
        public int CaseInvoiceID
        {
            get { return _CaseInvoiceID; }
            set
            {
                if (!object.Equals(_CaseInvoiceID, value))
                {
                    OnCaseInvoiceIDing(value);
                    _CaseInvoiceID = value;
                    IsModified = true;
                    OnCaseInvoiceIDed();
                }
            }
        }

        #endregion

        #region int CaseMedicalTestRequirementID

        private int _CaseMedicalTestRequirementID;
        [Required]
        public int CaseMedicalTestRequirementID
        {
            get { return _CaseMedicalTestRequirementID; }
            set
            {
                if (!object.Equals(_CaseMedicalTestRequirementID, value))
                {
                    OnCaseMedicalTestRequirementIDing(value);
                    _CaseMedicalTestRequirementID = value;
                    IsModified = true;
                    OnCaseMedicalTestRequirementIDed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("Category", typeof(E_TESTREQUIREMENTCATEGORY), "ID")]
    public partial class T_CASEMEDICALTESTREQUIREMENT : MediFastClass<T_CASEMEDICALTESTREQUIREMENT>, ITable, IName
    {
        public T_CASEMEDICALTESTREQUIREMENT()
            : base()
        {
            OnCreated();
        }

        public static T_CASEMEDICALTESTREQUIREMENT SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CASEMEDICALTESTREQUIREMENT.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnCaseMonthlyIDing(int value);
        partial void OnCaseMonthlyIDed();
        partial void OnCategorying(BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY value);
        partial void OnCategoryed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnCostsing(Currency? value);
        partial void OnCostsed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int CaseMonthlyID

        private int _CaseMonthlyID;
        [Required]
        public int CaseMonthlyID
        {
            get { return _CaseMonthlyID; }
            set
            {
                if (!object.Equals(_CaseMonthlyID, value))
                {
                    OnCaseMonthlyIDing(value);
                    _CaseMonthlyID = value;
                    IsModified = true;
                    OnCaseMonthlyIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY Category

        private BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY _Category;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY Category
        {
            get { return _Category; }
            set
            {
                if (!object.Equals(_Category, value))
                {
                    OnCategorying(value);
                    _Category = value;
                    IsModified = true;
                    OnCategoryed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(50)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region Currency? Costs

        private Currency? _Costs;
        public Currency? Costs
        {
            get { return _Costs; }
            set
            {
                if (!object.Equals(_Costs, value))
                {
                    OnCostsing(value);
                    _Costs = value;
                    IsModified = true;
                    OnCostsed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("InsurerID", typeof(T_COMPANY), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    public partial class T_CASEMONTHLY : MediFastClass<T_CASEMONTHLY>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator
    {
        public T_CASEMONTHLY()
            : base()
        {
            OnCreated();
        }

        public static T_CASEMONTHLY SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CASEMONTHLY.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnInsurerIDing(int value);
        partial void OnInsurerIDed();
        partial void OnYearing(short value);
        partial void OnYeared();
        partial void OnMonthing(byte value);
        partial void OnMonthed();
        partial void OnTotaling(Currency value);
        partial void OnTotaled();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int InsurerID

        private int _InsurerID;
        [Required]
        public int InsurerID
        {
            get { return _InsurerID; }
            set
            {
                if (!object.Equals(_InsurerID, value))
                {
                    OnInsurerIDing(value);
                    _InsurerID = value;
                    IsModified = true;
                    OnInsurerIDed();
                }
            }
        }

        #endregion

        #region short Year

        private short _Year;
        [Required]
        public short Year
        {
            get { return _Year; }
            set
            {
                if (!object.Equals(_Year, value))
                {
                    OnYearing(value);
                    _Year = value;
                    IsModified = true;
                    OnYeared();
                }
            }
        }

        #endregion

        #region byte Month

        private byte _Month;
        [Required]
        public byte Month
        {
            get { return _Month; }
            set
            {
                if (!object.Equals(_Month, value))
                {
                    OnMonthing(value);
                    _Month = value;
                    IsModified = true;
                    OnMonthed();
                }
            }
        }

        #endregion

        #region Currency Total

        private Currency _Total;
        [Required]
        public Currency Total
        {
            get { return _Total; }
            set
            {
                if (!object.Equals(_Total, value))
                {
                    OnTotaling(value);
                    _Total = value;
                    IsModified = true;
                    OnTotaled();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("AssignedTo", typeof(T_USER), "ID")]
    [ForeignKey("CaseStatus", typeof(E_CASESTATUS), "ID")]
    [ForeignKey("Country", typeof(E_COUNTRY), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("DefermentReferenceID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("InsCaseStatus", typeof(E_INSURERCASESTATUS), "ID")]
    [ForeignKey("InsurerID", typeof(T_COMPANY), "ID")]
    [ForeignKey("InsurerPIC", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    [ForeignKey("PolicyType", typeof(T_POLICYTYPE), "ID")]
    [ForeignKey("State", typeof(E_STATE), "ID")]
    [ForeignKey("Supervisor", typeof(T_USER), "ID")]
    public partial class T_CLIENTCASE : MediFastClass<T_CLIENTCASE>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IActive
    {
        public T_CLIENTCASE()
            : base()
        {
            OnCreated();
        }

        public static T_CLIENTCASE SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CLIENTCASE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientNameing(string value);
        partial void OnClientNameed();
        partial void OnNRICing(string value);
        partial void OnNRICed();
        partial void OnLANoing(string value);
        partial void OnLANoed();
        partial void OnPolicyNoing(string value);
        partial void OnPolicyNoed();
        partial void OnPolicyTypeing(int? value);
        partial void OnPolicyTypeed();
        partial void OnDOBing(DateTime? value);
        partial void OnDOBed();
        partial void OnClientNoing(string value);
        partial void OnClientNoed();
        partial void OnDueDateing(DateTime? value);
        partial void OnDueDateed();
        partial void OnEmailing(string value);
        partial void OnEmailed();
        partial void OnMobileNoing(string value);
        partial void OnMobileNoed();
        partial void OnOfficePhoneing(string value);
        partial void OnOfficePhoneed();
        partial void OnHomePhoneing(string value);
        partial void OnHomePhoneed();
        partial void OnFaxNoing(string value);
        partial void OnFaxNoed();
        partial void OnAddress1ing(string value);
        partial void OnAddress1ed();
        partial void OnAddress2ing(string value);
        partial void OnAddress2ed();
        partial void OnCitying(string value);
        partial void OnCityed();
        partial void OnPostcodeing(string value);
        partial void OnPostcodeed();
        partial void OnStateing(BLO.Objects.Enums.MediFast.Enum_STATE? value);
        partial void OnStateed();
        partial void OnCountrying(BLO.Objects.Enums.MediFast.Enum_COUNTRY? value);
        partial void OnCountryed();
        partial void OnCaseStatusing(BLO.Objects.Enums.MediFast.Enum_CASESTATUS? value);
        partial void OnCaseStatused();
        partial void OnCaseStatusDateing(DateTime? value);
        partial void OnCaseStatusDateed();
        partial void OnInsCaseStatusing(BLO.Objects.Enums.MediFast.Enum_INSURERCASESTATUS value);
        partial void OnInsCaseStatused();
        partial void OnInsCaseStatusDateing(DateTime value);
        partial void OnInsCaseStatusDateed();
        partial void OnAssignedToing(int? value);
        partial void OnAssignedToed();
        partial void OnAssignedDateing(DateTime? value);
        partial void OnAssignedDateed();
        partial void OnSupervisoring(int? value);
        partial void OnSupervisored();
        partial void OnSupervisorAssignedDateing(DateTime? value);
        partial void OnSupervisorAssignedDateed();
        partial void OnInsurerIDing(int value);
        partial void OnInsurerIDed();
        partial void OnInsurerPICing(int? value);
        partial void OnInsurerPICed();
        partial void OnInsurerPICDateing(DateTime? value);
        partial void OnInsurerPICDateed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();
        partial void OnDefermenting(byte value);
        partial void OnDefermented();
        partial void OnDefermentReferenceIDing(int? value);
        partial void OnDefermentReferenceIDed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region string ClientName

        private string _ClientName;
        [Required]
        [MaxLength(200)]
        public string ClientName
        {
            get { return _ClientName; }
            set
            {
                if (!object.Equals(_ClientName, value))
                {
                    OnClientNameing(value);
                    _ClientName = value;
                    IsModified = true;
                    OnClientNameed();
                }
            }
        }

        #endregion

        #region string NRIC

        private string _NRIC;
        [Required]
        [MaxLength(12)]
        public string NRIC
        {
            get { return _NRIC; }
            set
            {
                if (!object.Equals(_NRIC, value))
                {
                    OnNRICing(value);
                    _NRIC = value;
                    IsModified = true;
                    OnNRICed();
                }
            }
        }

        #endregion

        #region string LANo

        private string _LANo;
        [MaxLength(20)]
        public string LANo
        {
            get { return _LANo; }
            set
            {
                if (!object.Equals(_LANo, value))
                {
                    OnLANoing(value);
                    _LANo = value;
                    IsModified = true;
                    OnLANoed();
                }
            }
        }

        #endregion

        #region string PolicyNo

        private string _PolicyNo;
        [Required]
        [MaxLength(20)]
        public string PolicyNo
        {
            get { return _PolicyNo; }
            set
            {
                if (!object.Equals(_PolicyNo, value))
                {
                    OnPolicyNoing(value);
                    _PolicyNo = value;
                    IsModified = true;
                    OnPolicyNoed();
                }
            }
        }

        #endregion

        #region int? PolicyType

        private int? _PolicyType;
        public int? PolicyType
        {
            get { return _PolicyType; }
            set
            {
                if (!object.Equals(_PolicyType, value))
                {
                    OnPolicyTypeing(value);
                    _PolicyType = value;
                    IsModified = true;
                    OnPolicyTypeed();
                }
            }
        }

        #endregion

        #region DateTime? DOB

        private DateTime? _DOB;
        [DateOnly]
        public DateTime? DOB
        {
            get { return _DOB; }
            set
            {
                if (!object.Equals(_DOB, value))
                {
                    OnDOBing(value);
                    _DOB = value;
                    IsModified = true;
                    OnDOBed();
                }
            }
        }

        #endregion

        #region string ClientNo

        private string _ClientNo;
        [MaxLength(20)]
        public string ClientNo
        {
            get { return _ClientNo; }
            set
            {
                if (!object.Equals(_ClientNo, value))
                {
                    OnClientNoing(value);
                    _ClientNo = value;
                    IsModified = true;
                    OnClientNoed();
                }
            }
        }

        #endregion

        #region DateTime? DueDate

        private DateTime? _DueDate;
        [DateOnly]
        public DateTime? DueDate
        {
            get { return _DueDate; }
            set
            {
                if (!object.Equals(_DueDate, value))
                {
                    OnDueDateing(value);
                    _DueDate = value;
                    IsModified = true;
                    OnDueDateed();
                }
            }
        }

        #endregion

        #region string Email

        private string _Email;
        [MaxLength(100)]
        public string Email
        {
            get { return _Email; }
            set
            {
                if (!object.Equals(_Email, value))
                {
                    OnEmailing(value);
                    _Email = value;
                    IsModified = true;
                    OnEmailed();
                }
            }
        }

        #endregion

        #region string MobileNo

        private string _MobileNo;
        [Required]
        [MaxLength(20)]
        public string MobileNo
        {
            get { return _MobileNo; }
            set
            {
                if (!object.Equals(_MobileNo, value))
                {
                    OnMobileNoing(value);
                    _MobileNo = value;
                    IsModified = true;
                    OnMobileNoed();
                }
            }
        }

        #endregion

        #region string OfficePhone

        private string _OfficePhone;
        [MaxLength(20)]
        public string OfficePhone
        {
            get { return _OfficePhone; }
            set
            {
                if (!object.Equals(_OfficePhone, value))
                {
                    OnOfficePhoneing(value);
                    _OfficePhone = value;
                    IsModified = true;
                    OnOfficePhoneed();
                }
            }
        }

        #endregion

        #region string HomePhone

        private string _HomePhone;
        [MaxLength(20)]
        public string HomePhone
        {
            get { return _HomePhone; }
            set
            {
                if (!object.Equals(_HomePhone, value))
                {
                    OnHomePhoneing(value);
                    _HomePhone = value;
                    IsModified = true;
                    OnHomePhoneed();
                }
            }
        }

        #endregion

        #region string FaxNo

        private string _FaxNo;
        [MaxLength(20)]
        public string FaxNo
        {
            get { return _FaxNo; }
            set
            {
                if (!object.Equals(_FaxNo, value))
                {
                    OnFaxNoing(value);
                    _FaxNo = value;
                    IsModified = true;
                    OnFaxNoed();
                }
            }
        }

        #endregion

        #region string Address1

        private string _Address1;
        [MaxLength(100)]
        public string Address1
        {
            get { return _Address1; }
            set
            {
                if (!object.Equals(_Address1, value))
                {
                    OnAddress1ing(value);
                    _Address1 = value;
                    IsModified = true;
                    OnAddress1ed();
                }
            }
        }

        #endregion

        #region string Address2

        private string _Address2;
        [MaxLength(100)]
        public string Address2
        {
            get { return _Address2; }
            set
            {
                if (!object.Equals(_Address2, value))
                {
                    OnAddress2ing(value);
                    _Address2 = value;
                    IsModified = true;
                    OnAddress2ed();
                }
            }
        }

        #endregion

        #region string City

        private string _City;
        [MaxLength(50)]
        public string City
        {
            get { return _City; }
            set
            {
                if (!object.Equals(_City, value))
                {
                    OnCitying(value);
                    _City = value;
                    IsModified = true;
                    OnCityed();
                }
            }
        }

        #endregion

        #region string Postcode

        private string _Postcode;
        [MaxLength(10)]
        public string Postcode
        {
            get { return _Postcode; }
            set
            {
                if (!object.Equals(_Postcode, value))
                {
                    OnPostcodeing(value);
                    _Postcode = value;
                    IsModified = true;
                    OnPostcodeed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_STATE? State

        private BLO.Objects.Enums.MediFast.Enum_STATE? _State;
        public BLO.Objects.Enums.MediFast.Enum_STATE? State
        {
            get { return _State; }
            set
            {
                if (!object.Equals(_State, value))
                {
                    OnStateing(value);
                    _State = value;
                    IsModified = true;
                    OnStateed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_COUNTRY? Country

        private BLO.Objects.Enums.MediFast.Enum_COUNTRY? _Country;
        public BLO.Objects.Enums.MediFast.Enum_COUNTRY? Country
        {
            get { return _Country; }
            set
            {
                if (!object.Equals(_Country, value))
                {
                    OnCountrying(value);
                    _Country = value;
                    IsModified = true;
                    OnCountryed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_CASESTATUS? CaseStatus

        private BLO.Objects.Enums.MediFast.Enum_CASESTATUS? _CaseStatus;
        public BLO.Objects.Enums.MediFast.Enum_CASESTATUS? CaseStatus
        {
            get { return _CaseStatus; }
            set
            {
                if (!object.Equals(_CaseStatus, value))
                {
                    OnCaseStatusing(value);
                    _CaseStatus = value;
                    IsModified = true;
                    OnCaseStatused();
                }
            }
        }

        #endregion

        #region DateTime? CaseStatusDate

        private DateTime? _CaseStatusDate;
        [DateIncludeTime]
        public DateTime? CaseStatusDate
        {
            get { return _CaseStatusDate; }
            set
            {
                if (!object.Equals(_CaseStatusDate, value))
                {
                    OnCaseStatusDateing(value);
                    _CaseStatusDate = value;
                    IsModified = true;
                    OnCaseStatusDateed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_INSURERCASESTATUS InsCaseStatus

        private BLO.Objects.Enums.MediFast.Enum_INSURERCASESTATUS _InsCaseStatus;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_INSURERCASESTATUS InsCaseStatus
        {
            get { return _InsCaseStatus; }
            set
            {
                if (!object.Equals(_InsCaseStatus, value))
                {
                    OnInsCaseStatusing(value);
                    _InsCaseStatus = value;
                    IsModified = true;
                    OnInsCaseStatused();
                }
            }
        }

        #endregion

        #region DateTime InsCaseStatusDate

        private DateTime _InsCaseStatusDate;
        [Required]
        [DateIncludeTime]
        public DateTime InsCaseStatusDate
        {
            get { return _InsCaseStatusDate; }
            set
            {
                if (!object.Equals(_InsCaseStatusDate, value))
                {
                    OnInsCaseStatusDateing(value);
                    _InsCaseStatusDate = value;
                    IsModified = true;
                    OnInsCaseStatusDateed();
                }
            }
        }

        #endregion

        #region int? AssignedTo

        private int? _AssignedTo;
        public int? AssignedTo
        {
            get { return _AssignedTo; }
            set
            {
                if (!object.Equals(_AssignedTo, value))
                {
                    OnAssignedToing(value);
                    _AssignedTo = value;
                    IsModified = true;
                    OnAssignedToed();
                }
            }
        }

        #endregion

        #region DateTime? AssignedDate

        private DateTime? _AssignedDate;
        [DateIncludeTime]
        public DateTime? AssignedDate
        {
            get { return _AssignedDate; }
            set
            {
                if (!object.Equals(_AssignedDate, value))
                {
                    OnAssignedDateing(value);
                    _AssignedDate = value;
                    IsModified = true;
                    OnAssignedDateed();
                }
            }
        }

        #endregion

        #region int? Supervisor

        private int? _Supervisor;
        public int? Supervisor
        {
            get { return _Supervisor; }
            set
            {
                if (!object.Equals(_Supervisor, value))
                {
                    OnSupervisoring(value);
                    _Supervisor = value;
                    IsModified = true;
                    OnSupervisored();
                }
            }
        }

        #endregion

        #region DateTime? SupervisorAssignedDate

        private DateTime? _SupervisorAssignedDate;
        [DateIncludeTime]
        public DateTime? SupervisorAssignedDate
        {
            get { return _SupervisorAssignedDate; }
            set
            {
                if (!object.Equals(_SupervisorAssignedDate, value))
                {
                    OnSupervisorAssignedDateing(value);
                    _SupervisorAssignedDate = value;
                    IsModified = true;
                    OnSupervisorAssignedDateed();
                }
            }
        }

        #endregion

        #region int InsurerID

        private int _InsurerID;
        [Required]
        public int InsurerID
        {
            get { return _InsurerID; }
            set
            {
                if (!object.Equals(_InsurerID, value))
                {
                    OnInsurerIDing(value);
                    _InsurerID = value;
                    IsModified = true;
                    OnInsurerIDed();
                }
            }
        }

        #endregion

        #region int? InsurerPIC

        private int? _InsurerPIC;
        public int? InsurerPIC
        {
            get { return _InsurerPIC; }
            set
            {
                if (!object.Equals(_InsurerPIC, value))
                {
                    OnInsurerPICing(value);
                    _InsurerPIC = value;
                    IsModified = true;
                    OnInsurerPICed();
                }
            }
        }

        #endregion

        #region DateTime? InsurerPICDate

        private DateTime? _InsurerPICDate;
        [DateIncludeTime]
        public DateTime? InsurerPICDate
        {
            get { return _InsurerPICDate; }
            set
            {
                if (!object.Equals(_InsurerPICDate, value))
                {
                    OnInsurerPICDateing(value);
                    _InsurerPICDate = value;
                    IsModified = true;
                    OnInsurerPICDateed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

        #region byte Deferment

        private byte _Deferment;
        [Required]
        public byte Deferment
        {
            get { return _Deferment; }
            set
            {
                if (!object.Equals(_Deferment, value))
                {
                    OnDefermenting(value);
                    _Deferment = value;
                    IsModified = true;
                    OnDefermented();
                }
            }
        }

        #endregion

        #region int? DefermentReferenceID

        private int? _DefermentReferenceID;
        public int? DefermentReferenceID
        {
            get { return _DefermentReferenceID; }
            set
            {
                if (!object.Equals(_DefermentReferenceID, value))
                {
                    OnDefermentReferenceIDing(value);
                    _DefermentReferenceID = value;
                    IsModified = true;
                    OnDefermentReferenceIDed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("CaseStatus", typeof(E_CASESTATUS), "ID")]
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    public partial class T_CLIENTCASEACTION : MediFastClass<T_CLIENTCASEACTION>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator
    {
        public T_CLIENTCASEACTION()
            : base()
        {
            OnCreated();
        }

        public static T_CLIENTCASEACTION SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CLIENTCASEACTION.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnCaseStatusing(BLO.Objects.Enums.MediFast.Enum_CASESTATUS? value);
        partial void OnCaseStatused();
        partial void OnRemarksing(string value);
        partial void OnRemarksed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_CASESTATUS? CaseStatus

        private BLO.Objects.Enums.MediFast.Enum_CASESTATUS? _CaseStatus;
        public BLO.Objects.Enums.MediFast.Enum_CASESTATUS? CaseStatus
        {
            get { return _CaseStatus; }
            set
            {
                if (!object.Equals(_CaseStatus, value))
                {
                    OnCaseStatusing(value);
                    _CaseStatus = value;
                    IsModified = true;
                    OnCaseStatused();
                }
            }
        }

        #endregion

        #region string Remarks

        private string _Remarks;
        [Required]
        [MaxLength(16)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                if (!object.Equals(_Remarks, value))
                {
                    OnRemarksing(value);
                    _Remarks = value;
                    IsModified = true;
                    OnRemarksed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("FormID", typeof(T_FORM), "ID")]
    public partial class T_CLIENTCASEFORM : MediFastClass<T_CLIENTCASEFORM>, ITable
    {
        public T_CLIENTCASEFORM()
            : base()
        {
            OnCreated();
        }

        public static T_CLIENTCASEFORM SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CLIENTCASEFORM.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnFormIDing(int value);
        partial void OnFormIDed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region int FormID

        private int _FormID;
        [Required]
        public int FormID
        {
            get { return _FormID; }
            set
            {
                if (!object.Equals(_FormID, value))
                {
                    OnFormIDing(value);
                    _FormID = value;
                    IsModified = true;
                    OnFormIDed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("MedicalTestRequirement", typeof(T_MEDICALTESTREQUIREMENT), "ID")]
    public partial class T_CLIENTMEDICALTESTREQUIREMENT : MediFastClass<T_CLIENTMEDICALTESTREQUIREMENT>, ITable
    {
        public T_CLIENTMEDICALTESTREQUIREMENT()
            : base()
        {
            OnCreated();
        }

        public static T_CLIENTMEDICALTESTREQUIREMENT SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CLIENTMEDICALTESTREQUIREMENT.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnMedicalTestRequirementing(int value);
        partial void OnMedicalTestRequiremented();
        partial void OnMedifastReceivedDateing(DateTime? value);
        partial void OnMedifastReceivedDateed();
        partial void OnDespatchDateing(DateTime? value);
        partial void OnDespatchDateed();
        partial void OnInsurerReceivedDateing(DateTime? value);
        partial void OnInsurerReceivedDateed();
        partial void OnRemarksing(string value);
        partial void OnRemarksed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region int MedicalTestRequirement

        private int _MedicalTestRequirement;
        [Required]
        public int MedicalTestRequirement
        {
            get { return _MedicalTestRequirement; }
            set
            {
                if (!object.Equals(_MedicalTestRequirement, value))
                {
                    OnMedicalTestRequirementing(value);
                    _MedicalTestRequirement = value;
                    IsModified = true;
                    OnMedicalTestRequiremented();
                }
            }
        }

        #endregion

        #region DateTime? MedifastReceivedDate

        private DateTime? _MedifastReceivedDate;
        [DateIncludeTime]
        public DateTime? MedifastReceivedDate
        {
            get { return _MedifastReceivedDate; }
            set
            {
                if (!object.Equals(_MedifastReceivedDate, value))
                {
                    OnMedifastReceivedDateing(value);
                    _MedifastReceivedDate = value;
                    IsModified = true;
                    OnMedifastReceivedDateed();
                }
            }
        }

        #endregion

        #region DateTime? DespatchDate

        private DateTime? _DespatchDate;
        [DateIncludeTime]
        public DateTime? DespatchDate
        {
            get { return _DespatchDate; }
            set
            {
                if (!object.Equals(_DespatchDate, value))
                {
                    OnDespatchDateing(value);
                    _DespatchDate = value;
                    IsModified = true;
                    OnDespatchDateed();
                }
            }
        }

        #endregion

        #region DateTime? InsurerReceivedDate

        private DateTime? _InsurerReceivedDate;
        [DateIncludeTime]
        public DateTime? InsurerReceivedDate
        {
            get { return _InsurerReceivedDate; }
            set
            {
                if (!object.Equals(_InsurerReceivedDate, value))
                {
                    OnInsurerReceivedDateing(value);
                    _InsurerReceivedDate = value;
                    IsModified = true;
                    OnInsurerReceivedDateed();
                }
            }
        }

        #endregion

        #region string Remarks

        private string _Remarks;
        [MaxLength(500)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                if (!object.Equals(_Remarks, value))
                {
                    OnRemarksing(value);
                    _Remarks = value;
                    IsModified = true;
                    OnRemarksed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    public partial class T_CLIENTMESSAGE : MediFastClass<T_CLIENTMESSAGE>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator
    {
        public T_CLIENTMESSAGE()
            : base()
        {
            OnCreated();
        }

        public static T_CLIENTMESSAGE SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_CLIENTMESSAGE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnSubjecting(string value);
        partial void OnSubjected();
        partial void OnRemarksing(string value);
        partial void OnRemarksed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region string Subject

        private string _Subject;
        [MaxLength(200)]
        public string Subject
        {
            get { return _Subject; }
            set
            {
                if (!object.Equals(_Subject, value))
                {
                    OnSubjecting(value);
                    _Subject = value;
                    IsModified = true;
                    OnSubjected();
                }
            }
        }

        #endregion

        #region string Remarks

        private string _Remarks;
        [MaxLength(16)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                if (!object.Equals(_Remarks, value))
                {
                    OnRemarksing(value);
                    _Remarks = value;
                    IsModified = true;
                    OnRemarksed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("CompanyType", typeof(E_COMPANYTYPE), "ID")]
    [ForeignKey("Country", typeof(E_COUNTRY), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    [ForeignKey("State", typeof(E_STATE), "ID")]
    public partial class T_COMPANY : MediFastClass<T_COMPANY>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IActive
    {
        public T_COMPANY()
            : base()
        {
            OnCreated();
        }

        public static T_COMPANY SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_COMPANY.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnCompanyTypeing(BLO.Objects.Enums.MediFast.Enum_COMPANYTYPE value);
        partial void OnCompanyTypeed();
        partial void OnCompanyNameing(string value);
        partial void OnCompanyNameed();
        partial void OnCompanyCodeing(string value);
        partial void OnCompanyCodeed();
        partial void OnEmailing(string value);
        partial void OnEmailed();
        partial void OnSecondEmailing(string value);
        partial void OnSecondEmailed();
        partial void OnAddress1ing(string value);
        partial void OnAddress1ed();
        partial void OnAddress2ing(string value);
        partial void OnAddress2ed();
        partial void OnCitying(string value);
        partial void OnCityed();
        partial void OnPostcodeing(string value);
        partial void OnPostcodeed();
        partial void OnStateing(BLO.Objects.Enums.MediFast.Enum_STATE? value);
        partial void OnStateed();
        partial void OnCountrying(BLO.Objects.Enums.MediFast.Enum_COUNTRY? value);
        partial void OnCountryed();
        partial void OnTelephone1ing(string value);
        partial void OnTelephone1ed();
        partial void OnTelephone2ing(string value);
        partial void OnTelephone2ed();
        partial void OnFax1ing(string value);
        partial void OnFax1ed();
        partial void OnFax2ing(string value);
        partial void OnFax2ed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_COMPANYTYPE CompanyType

        private BLO.Objects.Enums.MediFast.Enum_COMPANYTYPE _CompanyType;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_COMPANYTYPE CompanyType
        {
            get { return _CompanyType; }
            set
            {
                if (!object.Equals(_CompanyType, value))
                {
                    OnCompanyTypeing(value);
                    _CompanyType = value;
                    IsModified = true;
                    OnCompanyTypeed();
                }
            }
        }

        #endregion

        #region string CompanyName

        private string _CompanyName;
        [Required]
        [MaxLength(200)]
        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {
                if (!object.Equals(_CompanyName, value))
                {
                    OnCompanyNameing(value);
                    _CompanyName = value;
                    IsModified = true;
                    OnCompanyNameed();
                }
            }
        }

        #endregion

        #region string CompanyCode

        private string _CompanyCode;
        [MaxLength(20)]
        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (!object.Equals(_CompanyCode, value))
                {
                    OnCompanyCodeing(value);
                    _CompanyCode = value;
                    IsModified = true;
                    OnCompanyCodeed();
                }
            }
        }

        #endregion

        #region string Email

        private string _Email;
        [MaxLength(300)]
        public string Email
        {
            get { return _Email; }
            set
            {
                if (!object.Equals(_Email, value))
                {
                    OnEmailing(value);
                    _Email = value;
                    IsModified = true;
                    OnEmailed();
                }
            }
        }

        #endregion

        #region string SecondEmail

        private string _SecondEmail;
        [MaxLength(300)]
        public string SecondEmail
        {
            get { return _SecondEmail; }
            set
            {
                if (!object.Equals(_SecondEmail, value))
                {
                    OnSecondEmailing(value);
                    _SecondEmail = value;
                    IsModified = true;
                    OnSecondEmailed();
                }
            }
        }

        #endregion

        #region string Address1

        private string _Address1;
        [MaxLength(200)]
        public string Address1
        {
            get { return _Address1; }
            set
            {
                if (!object.Equals(_Address1, value))
                {
                    OnAddress1ing(value);
                    _Address1 = value;
                    IsModified = true;
                    OnAddress1ed();
                }
            }
        }

        #endregion

        #region string Address2

        private string _Address2;
        [MaxLength(200)]
        public string Address2
        {
            get { return _Address2; }
            set
            {
                if (!object.Equals(_Address2, value))
                {
                    OnAddress2ing(value);
                    _Address2 = value;
                    IsModified = true;
                    OnAddress2ed();
                }
            }
        }

        #endregion

        #region string City

        private string _City;
        [MaxLength(50)]
        public string City
        {
            get { return _City; }
            set
            {
                if (!object.Equals(_City, value))
                {
                    OnCitying(value);
                    _City = value;
                    IsModified = true;
                    OnCityed();
                }
            }
        }

        #endregion

        #region string Postcode

        private string _Postcode;
        [MaxLength(10)]
        public string Postcode
        {
            get { return _Postcode; }
            set
            {
                if (!object.Equals(_Postcode, value))
                {
                    OnPostcodeing(value);
                    _Postcode = value;
                    IsModified = true;
                    OnPostcodeed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_STATE? State

        private BLO.Objects.Enums.MediFast.Enum_STATE? _State;
        public BLO.Objects.Enums.MediFast.Enum_STATE? State
        {
            get { return _State; }
            set
            {
                if (!object.Equals(_State, value))
                {
                    OnStateing(value);
                    _State = value;
                    IsModified = true;
                    OnStateed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_COUNTRY? Country

        private BLO.Objects.Enums.MediFast.Enum_COUNTRY? _Country;
        public BLO.Objects.Enums.MediFast.Enum_COUNTRY? Country
        {
            get { return _Country; }
            set
            {
                if (!object.Equals(_Country, value))
                {
                    OnCountrying(value);
                    _Country = value;
                    IsModified = true;
                    OnCountryed();
                }
            }
        }

        #endregion

        #region string Telephone1

        private string _Telephone1;
        [MaxLength(20)]
        public string Telephone1
        {
            get { return _Telephone1; }
            set
            {
                if (!object.Equals(_Telephone1, value))
                {
                    OnTelephone1ing(value);
                    _Telephone1 = value;
                    IsModified = true;
                    OnTelephone1ed();
                }
            }
        }

        #endregion

        #region string Telephone2

        private string _Telephone2;
        [MaxLength(20)]
        public string Telephone2
        {
            get { return _Telephone2; }
            set
            {
                if (!object.Equals(_Telephone2, value))
                {
                    OnTelephone2ing(value);
                    _Telephone2 = value;
                    IsModified = true;
                    OnTelephone2ed();
                }
            }
        }

        #endregion

        #region string Fax1

        private string _Fax1;
        [MaxLength(20)]
        public string Fax1
        {
            get { return _Fax1; }
            set
            {
                if (!object.Equals(_Fax1, value))
                {
                    OnFax1ing(value);
                    _Fax1 = value;
                    IsModified = true;
                    OnFax1ed();
                }
            }
        }

        #endregion

        #region string Fax2

        private string _Fax2;
        [MaxLength(20)]
        public string Fax2
        {
            get { return _Fax2; }
            set
            {
                if (!object.Equals(_Fax2, value))
                {
                    OnFax2ing(value);
                    _Fax2 = value;
                    IsModified = true;
                    OnFax2ed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("ClientCaseID", typeof(T_CLIENTCASE), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("EventLogType", typeof(E_EVENTLOGTYPE), "ID")]
    public partial class T_EVENTLOG : MediFastClass<T_EVENTLOG>, ITable, ICreatedDate
    {
        public T_EVENTLOG()
            : base()
        {
            OnCreated();
        }

        public static T_EVENTLOG SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_EVENTLOG.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnEventLogTypeing(BLO.Objects.Enums.MediFast.Enum_EVENTLOGTYPE value);
        partial void OnEventLogTypeed();
        partial void OnDescriptioning(string value);
        partial void OnDescriptioned();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_EVENTLOGTYPE EventLogType

        private BLO.Objects.Enums.MediFast.Enum_EVENTLOGTYPE _EventLogType;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_EVENTLOGTYPE EventLogType
        {
            get { return _EventLogType; }
            set
            {
                if (!object.Equals(_EventLogType, value))
                {
                    OnEventLogTypeing(value);
                    _EventLogType = value;
                    IsModified = true;
                    OnEventLogTypeed();
                }
            }
        }

        #endregion

        #region string Description

        private string _Description;
        [Required]
        [MaxLength(500)]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (!object.Equals(_Description, value))
                {
                    OnDescriptioning(value);
                    _Description = value;
                    IsModified = true;
                    OnDescriptioned();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("InsurerID", typeof(T_COMPANY), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    public partial class T_FORM : MediFastClass<T_FORM>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IActive
    {
        public T_FORM()
            : base()
        {
            OnCreated();
        }

        public static T_FORM SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_FORM.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnInsurerIDing(int value);
        partial void OnInsurerIDed();
        partial void OnDescriptioning(string value);
        partial void OnDescriptioned();
        partial void OnFilePathing(string value);
        partial void OnFilePathed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int InsurerID

        private int _InsurerID;
        [Required]
        public int InsurerID
        {
            get { return _InsurerID; }
            set
            {
                if (!object.Equals(_InsurerID, value))
                {
                    OnInsurerIDing(value);
                    _InsurerID = value;
                    IsModified = true;
                    OnInsurerIDed();
                }
            }
        }

        #endregion

        #region string Description

        private string _Description;
        [Required]
        [MaxLength(200)]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (!object.Equals(_Description, value))
                {
                    OnDescriptioning(value);
                    _Description = value;
                    IsModified = true;
                    OnDescriptioned();
                }
            }
        }

        #endregion

        #region string FilePath

        private string _FilePath;
        [Required]
        [MaxLength(200)]
        public string FilePath
        {
            get { return _FilePath; }
            set
            {
                if (!object.Equals(_FilePath, value))
                {
                    OnFilePathing(value);
                    _FilePath = value;
                    IsModified = true;
                    OnFilePathed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("Category", typeof(E_TESTREQUIREMENTCATEGORY), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("InsurerID", typeof(T_COMPANY), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    public partial class T_MEDICALTESTREQUIREMENT : MediFastClass<T_MEDICALTESTREQUIREMENT>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IName, IActive
    {
        public T_MEDICALTESTREQUIREMENT()
            : base()
        {
            OnCreated();
        }

        public static T_MEDICALTESTREQUIREMENT SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_MEDICALTESTREQUIREMENT.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnInsurerIDing(int? value);
        partial void OnInsurerIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();
        partial void OnCategorying(BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY value);
        partial void OnCategoryed();
        partial void OnCostsing(Currency? value);
        partial void OnCostsed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int? InsurerID

        private int? _InsurerID;
        public int? InsurerID
        {
            get { return _InsurerID; }
            set
            {
                if (!object.Equals(_InsurerID, value))
                {
                    OnInsurerIDing(value);
                    _InsurerID = value;
                    IsModified = true;
                    OnInsurerIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(50)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY Category

        private BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY _Category;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_TESTREQUIREMENTCATEGORY Category
        {
            get { return _Category; }
            set
            {
                if (!object.Equals(_Category, value))
                {
                    OnCategorying(value);
                    _Category = value;
                    IsModified = true;
                    OnCategoryed();
                }
            }
        }

        #endregion

        #region Currency? Costs

        private Currency? _Costs;
        public Currency? Costs
        {
            get { return _Costs; }
            set
            {
                if (!object.Equals(_Costs, value))
                {
                    OnCostsing(value);
                    _Costs = value;
                    IsModified = true;
                    OnCostsed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("Bank", typeof(E_BANK), "ID")]
    [ForeignKey("CreatedBy", typeof(T_USER), "ID")]
    [ForeignKey("ModifiedBy", typeof(T_USER), "ID")]
    [ForeignKey("PanelType", typeof(E_PANELTYPE), "ID")]
    [ForeignKey("State", typeof(E_STATE), "ID")]
    public partial class T_PANELDOCTOR : MediFastClass<T_PANELDOCTOR>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IActive
    {
        public T_PANELDOCTOR()
            : base()
        {
            OnCreated();
        }

        public static T_PANELDOCTOR SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_PANELDOCTOR.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnPanelTypeing(BLO.Objects.Enums.MediFast.Enum_PANELTYPE value);
        partial void OnPanelTypeed();
        partial void OnClinicCodeing(string value);
        partial void OnClinicCodeed();
        partial void OnClinicNameing(string value);
        partial void OnClinicNameed();
        partial void OnDoctorNameing(string value);
        partial void OnDoctorNameed();
        partial void OnTeling(string value);
        partial void OnTeled();
        partial void OnFaxing(string value);
        partial void OnFaxed();
        partial void OnAddressing(string value);
        partial void OnAddressed();
        partial void OnCitying(string value);
        partial void OnCityed();
        partial void OnStateing(BLO.Objects.Enums.MediFast.Enum_STATE value);
        partial void OnStateed();
        partial void OnFacilitying(string value);
        partial void OnFacilityed();
        partial void OnBanking(BLO.Objects.Enums.MediFast.Enum_BANK? value);
        partial void OnBanked();
        partial void OnBankAccountNoing(string value);
        partial void OnBankAccountNoed();
        partial void OnBankAccountNameing(string value);
        partial void OnBankAccountNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_PANELTYPE PanelType

        private BLO.Objects.Enums.MediFast.Enum_PANELTYPE _PanelType;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_PANELTYPE PanelType
        {
            get { return _PanelType; }
            set
            {
                if (!object.Equals(_PanelType, value))
                {
                    OnPanelTypeing(value);
                    _PanelType = value;
                    IsModified = true;
                    OnPanelTypeed();
                }
            }
        }

        #endregion

        #region string ClinicCode

        private string _ClinicCode;
        [Required]
        [MaxLength(10)]
        public string ClinicCode
        {
            get { return _ClinicCode; }
            set
            {
                if (!object.Equals(_ClinicCode, value))
                {
                    OnClinicCodeing(value);
                    _ClinicCode = value;
                    IsModified = true;
                    OnClinicCodeed();
                }
            }
        }

        #endregion

        #region string ClinicName

        private string _ClinicName;
        [Required]
        [MaxLength(100)]
        public string ClinicName
        {
            get { return _ClinicName; }
            set
            {
                if (!object.Equals(_ClinicName, value))
                {
                    OnClinicNameing(value);
                    _ClinicName = value;
                    IsModified = true;
                    OnClinicNameed();
                }
            }
        }

        #endregion

        #region string DoctorName

        private string _DoctorName;
        [Required]
        [MaxLength(200)]
        public string DoctorName
        {
            get { return _DoctorName; }
            set
            {
                if (!object.Equals(_DoctorName, value))
                {
                    OnDoctorNameing(value);
                    _DoctorName = value;
                    IsModified = true;
                    OnDoctorNameed();
                }
            }
        }

        #endregion

        #region string Tel

        private string _Tel;
        [Required]
        [MaxLength(20)]
        public string Tel
        {
            get { return _Tel; }
            set
            {
                if (!object.Equals(_Tel, value))
                {
                    OnTeling(value);
                    _Tel = value;
                    IsModified = true;
                    OnTeled();
                }
            }
        }

        #endregion

        #region string Fax

        private string _Fax;
        [MaxLength(20)]
        public string Fax
        {
            get { return _Fax; }
            set
            {
                if (!object.Equals(_Fax, value))
                {
                    OnFaxing(value);
                    _Fax = value;
                    IsModified = true;
                    OnFaxed();
                }
            }
        }

        #endregion

        #region string Address

        private string _Address;
        [MaxLength(100)]
        public string Address
        {
            get { return _Address; }
            set
            {
                if (!object.Equals(_Address, value))
                {
                    OnAddressing(value);
                    _Address = value;
                    IsModified = true;
                    OnAddressed();
                }
            }
        }

        #endregion

        #region string City

        private string _City;
        [Required]
        [MaxLength(50)]
        public string City
        {
            get { return _City; }
            set
            {
                if (!object.Equals(_City, value))
                {
                    OnCitying(value);
                    _City = value;
                    IsModified = true;
                    OnCityed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_STATE State

        private BLO.Objects.Enums.MediFast.Enum_STATE _State;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_STATE State
        {
            get { return _State; }
            set
            {
                if (!object.Equals(_State, value))
                {
                    OnStateing(value);
                    _State = value;
                    IsModified = true;
                    OnStateed();
                }
            }
        }

        #endregion

        #region string Facility

        private string _Facility;
        [MaxLength(500)]
        public string Facility
        {
            get { return _Facility; }
            set
            {
                if (!object.Equals(_Facility, value))
                {
                    OnFacilitying(value);
                    _Facility = value;
                    IsModified = true;
                    OnFacilityed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_BANK? Bank

        private BLO.Objects.Enums.MediFast.Enum_BANK? _Bank;
        public BLO.Objects.Enums.MediFast.Enum_BANK? Bank
        {
            get { return _Bank; }
            set
            {
                if (!object.Equals(_Bank, value))
                {
                    OnBanking(value);
                    _Bank = value;
                    IsModified = true;
                    OnBanked();
                }
            }
        }

        #endregion

        #region string BankAccountNo

        private string _BankAccountNo;
        [MaxLength(20)]
        public string BankAccountNo
        {
            get { return _BankAccountNo; }
            set
            {
                if (!object.Equals(_BankAccountNo, value))
                {
                    OnBankAccountNoing(value);
                    _BankAccountNo = value;
                    IsModified = true;
                    OnBankAccountNoed();
                }
            }
        }

        #endregion

        #region string BankAccountName

        private string _BankAccountName;
        [MaxLength(50)]
        public string BankAccountName
        {
            get { return _BankAccountName; }
            set
            {
                if (!object.Equals(_BankAccountName, value))
                {
                    OnBankAccountNameing(value);
                    _BankAccountName = value;
                    IsModified = true;
                    OnBankAccountNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("InsurerID", typeof(T_COMPANY), "ID")]
    public partial class T_POLICYTYPE : MediFastClass<T_POLICYTYPE>, ITable, IDate, ICreatedDate, IModifiedDate, ICreator, IName, IActive
    {
        public T_POLICYTYPE()
            : base()
        {
            OnCreated();
        }

        public static T_POLICYTYPE SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_POLICYTYPE.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnInsurerIDing(int? value);
        partial void OnInsurerIDed();
        partial void OnCodeing(string value);
        partial void OnCodeed();
        partial void OnNameing(string value);
        partial void OnNameed();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedBying(int value);
        partial void OnCreatedByed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedBying(int value);
        partial void OnModifiedByed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int? InsurerID

        private int? _InsurerID;
        public int? InsurerID
        {
            get { return _InsurerID; }
            set
            {
                if (!object.Equals(_InsurerID, value))
                {
                    OnInsurerIDing(value);
                    _InsurerID = value;
                    IsModified = true;
                    OnInsurerIDed();
                }
            }
        }

        #endregion

        #region string Code

        private string _Code;
        [Required]
        [MaxLength(50)]
        public string Code
        {
            get { return _Code; }
            set
            {
                if (!object.Equals(_Code, value))
                {
                    OnCodeing(value);
                    _Code = value;
                    IsModified = true;
                    OnCodeed();
                }
            }
        }

        #endregion

        #region string Name

        private string _Name;
        [Required]
        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!object.Equals(_Name, value))
                {
                    OnNameing(value);
                    _Name = value;
                    IsModified = true;
                    OnNameed();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region int CreatedBy

        private int _CreatedBy;
        [Required]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (!object.Equals(_CreatedBy, value))
                {
                    OnCreatedBying(value);
                    _CreatedBy = value;
                    IsModified = true;
                    OnCreatedByed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region int ModifiedBy

        private int _ModifiedBy;
        [Required]
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                if (!object.Equals(_ModifiedBy, value))
                {
                    OnModifiedBying(value);
                    _ModifiedBy = value;
                    IsModified = true;
                    OnModifiedByed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    [ForeignKey("CompanyID", typeof(T_COMPANY), "ID")]
    [ForeignKey("Country", typeof(E_COUNTRY), "ID")]
    [ForeignKey("PanelDoctorID", typeof(T_PANELDOCTOR), "ID")]
    [ForeignKey("State", typeof(E_STATE), "ID")]
    [ForeignKey("UserRole", typeof(E_USERROLE), "ID")]
    public partial class T_USER : MediFastClass<T_USER>, ITable, IDate, ICreatedDate, IModifiedDate, IActive
    {
        public T_USER()
            : base()
        {
            OnCreated();
        }

        public static T_USER SelectExact(int _ID)
        {
            if (_ID <= 0)
                return null;
            var whereList = new List<string>();
            whereList.Add(new WhereClause("ID={0}", _ID).Where);
            return T_USER.SelectSingle(new WhereClause(string.Join(" and ", whereList.ToArray())));
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnCompanyIDing(int? value);
        partial void OnCompanyIDed();
        partial void OnPanelDoctorIDing(int? value);
        partial void OnPanelDoctorIDed();
        partial void OnUserRoleing(BLO.Objects.Enums.MediFast.Enum_USERROLE value);
        partial void OnUserRoleed();
        partial void OnLoginIDing(string value);
        partial void OnLoginIDed();
        partial void OnPasswording(string value);
        partial void OnPassworded();
        partial void OnFullNameing(string value);
        partial void OnFullNameed();
        partial void OnNRICing(string value);
        partial void OnNRICed();
        partial void OnContactNoing(string value);
        partial void OnContactNoed();
        partial void OnSecondContactNoing(string value);
        partial void OnSecondContactNoed();
        partial void OnEmailing(string value);
        partial void OnEmailed();
        partial void OnAddress1ing(string value);
        partial void OnAddress1ed();
        partial void OnAddress2ing(string value);
        partial void OnAddress2ed();
        partial void OnPostcodeing(string value);
        partial void OnPostcodeed();
        partial void OnCitying(string value);
        partial void OnCityed();
        partial void OnStateing(BLO.Objects.Enums.MediFast.Enum_STATE? value);
        partial void OnStateed();
        partial void OnCountrying(BLO.Objects.Enums.MediFast.Enum_COUNTRY? value);
        partial void OnCountryed();
        partial void OnIPAddressing(string value);
        partial void OnIPAddressed();
        partial void OnIsPermenantUsering(bool value);
        partial void OnIsPermenantUsered();
        partial void OnActiveing(bool value);
        partial void OnActiveed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();
        partial void OnModifiedDateing(DateTime value);
        partial void OnModifiedDateed();

        #endregion

        #region int ID

        private int _ID;
        [PrimaryKey]
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int? CompanyID

        private int? _CompanyID;
        public int? CompanyID
        {
            get { return _CompanyID; }
            set
            {
                if (!object.Equals(_CompanyID, value))
                {
                    OnCompanyIDing(value);
                    _CompanyID = value;
                    IsModified = true;
                    OnCompanyIDed();
                }
            }
        }

        #endregion

        #region int? PanelDoctorID

        private int? _PanelDoctorID;
        public int? PanelDoctorID
        {
            get { return _PanelDoctorID; }
            set
            {
                if (!object.Equals(_PanelDoctorID, value))
                {
                    OnPanelDoctorIDing(value);
                    _PanelDoctorID = value;
                    IsModified = true;
                    OnPanelDoctorIDed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_USERROLE UserRole

        private BLO.Objects.Enums.MediFast.Enum_USERROLE _UserRole;
        [Required]
        public BLO.Objects.Enums.MediFast.Enum_USERROLE UserRole
        {
            get { return _UserRole; }
            set
            {
                if (!object.Equals(_UserRole, value))
                {
                    OnUserRoleing(value);
                    _UserRole = value;
                    IsModified = true;
                    OnUserRoleed();
                }
            }
        }

        #endregion

        #region string LoginID

        private string _LoginID;
        [Required]
        [MaxLength(50)]
        public string LoginID
        {
            get { return _LoginID; }
            set
            {
                if (!object.Equals(_LoginID, value))
                {
                    OnLoginIDing(value);
                    _LoginID = value;
                    IsModified = true;
                    OnLoginIDed();
                }
            }
        }

        #endregion

        #region string Password

        private string _Password;
        [Required]
        [MaxLength(100)]
        public string Password
        {
            get { return _Password; }
            set
            {
                if (!object.Equals(_Password, value))
                {
                    OnPasswording(value);
                    _Password = value;
                    IsModified = true;
                    OnPassworded();
                }
            }
        }

        #endregion

        #region string FullName

        private string _FullName;
        [Required]
        [MaxLength(150)]
        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (!object.Equals(_FullName, value))
                {
                    OnFullNameing(value);
                    _FullName = value;
                    IsModified = true;
                    OnFullNameed();
                }
            }
        }

        #endregion

        #region string NRIC

        private string _NRIC;
        [MaxLength(12)]
        public string NRIC
        {
            get { return _NRIC; }
            set
            {
                if (!object.Equals(_NRIC, value))
                {
                    OnNRICing(value);
                    _NRIC = value;
                    IsModified = true;
                    OnNRICed();
                }
            }
        }

        #endregion

        #region string ContactNo

        private string _ContactNo;
        [MaxLength(20)]
        public string ContactNo
        {
            get { return _ContactNo; }
            set
            {
                if (!object.Equals(_ContactNo, value))
                {
                    OnContactNoing(value);
                    _ContactNo = value;
                    IsModified = true;
                    OnContactNoed();
                }
            }
        }

        #endregion

        #region string SecondContactNo

        private string _SecondContactNo;
        [MaxLength(20)]
        public string SecondContactNo
        {
            get { return _SecondContactNo; }
            set
            {
                if (!object.Equals(_SecondContactNo, value))
                {
                    OnSecondContactNoing(value);
                    _SecondContactNo = value;
                    IsModified = true;
                    OnSecondContactNoed();
                }
            }
        }

        #endregion

        #region string Email

        private string _Email;
        [MaxLength(50)]
        public string Email
        {
            get { return _Email; }
            set
            {
                if (!object.Equals(_Email, value))
                {
                    OnEmailing(value);
                    _Email = value;
                    IsModified = true;
                    OnEmailed();
                }
            }
        }

        #endregion

        #region string Address1

        private string _Address1;
        [MaxLength(200)]
        public string Address1
        {
            get { return _Address1; }
            set
            {
                if (!object.Equals(_Address1, value))
                {
                    OnAddress1ing(value);
                    _Address1 = value;
                    IsModified = true;
                    OnAddress1ed();
                }
            }
        }

        #endregion

        #region string Address2

        private string _Address2;
        [MaxLength(200)]
        public string Address2
        {
            get { return _Address2; }
            set
            {
                if (!object.Equals(_Address2, value))
                {
                    OnAddress2ing(value);
                    _Address2 = value;
                    IsModified = true;
                    OnAddress2ed();
                }
            }
        }

        #endregion

        #region string Postcode

        private string _Postcode;
        [MaxLength(10)]
        public string Postcode
        {
            get { return _Postcode; }
            set
            {
                if (!object.Equals(_Postcode, value))
                {
                    OnPostcodeing(value);
                    _Postcode = value;
                    IsModified = true;
                    OnPostcodeed();
                }
            }
        }

        #endregion

        #region string City

        private string _City;
        [MaxLength(50)]
        public string City
        {
            get { return _City; }
            set
            {
                if (!object.Equals(_City, value))
                {
                    OnCitying(value);
                    _City = value;
                    IsModified = true;
                    OnCityed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_STATE? State

        private BLO.Objects.Enums.MediFast.Enum_STATE? _State;
        public BLO.Objects.Enums.MediFast.Enum_STATE? State
        {
            get { return _State; }
            set
            {
                if (!object.Equals(_State, value))
                {
                    OnStateing(value);
                    _State = value;
                    IsModified = true;
                    OnStateed();
                }
            }
        }

        #endregion

        #region BLO.Objects.Enums.MediFast.Enum_COUNTRY? Country

        private BLO.Objects.Enums.MediFast.Enum_COUNTRY? _Country;
        public BLO.Objects.Enums.MediFast.Enum_COUNTRY? Country
        {
            get { return _Country; }
            set
            {
                if (!object.Equals(_Country, value))
                {
                    OnCountrying(value);
                    _Country = value;
                    IsModified = true;
                    OnCountryed();
                }
            }
        }

        #endregion

        #region string IPAddress

        private string _IPAddress;
        [MaxLength(20)]
        public string IPAddress
        {
            get { return _IPAddress; }
            set
            {
                if (!object.Equals(_IPAddress, value))
                {
                    OnIPAddressing(value);
                    _IPAddress = value;
                    IsModified = true;
                    OnIPAddressed();
                }
            }
        }

        #endregion

        #region bool IsPermenantUser

        private bool _IsPermenantUser;
        [Required]
        public bool IsPermenantUser
        {
            get { return _IsPermenantUser; }
            set
            {
                if (!object.Equals(_IsPermenantUser, value))
                {
                    OnIsPermenantUsering(value);
                    _IsPermenantUser = value;
                    IsModified = true;
                    OnIsPermenantUsered();
                }
            }
        }

        #endregion

        #region bool Active

        private bool _Active;
        [Required]
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (!object.Equals(_Active, value))
                {
                    OnActiveing(value);
                    _Active = value;
                    IsModified = true;
                    OnActiveed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

        #region DateTime ModifiedDate

        private DateTime _ModifiedDate;
        [Required]
        [DateIncludeTime]
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set
            {
                if (!object.Equals(_ModifiedDate, value))
                {
                    OnModifiedDateing(value);
                    _ModifiedDate = value;
                    IsModified = true;
                    OnModifiedDateed();
                }
            }
        }

        #endregion

    }
    public partial class V_CLIENTCASEACTION : MediFastClass<V_CLIENTCASEACTION>, IView, ICreatedDate
    {
        public V_CLIENTCASEACTION()
            : base()
        {
            OnCreated();
        }

        #region partial method

        partial void OnCreated();
        partial void OnIDing(int value);
        partial void OnIDed();
        partial void OnClientCaseIDing(int value);
        partial void OnClientCaseIDed();
        partial void OnCaseStatusing(byte? value);
        partial void OnCaseStatused();
        partial void OnRemarksing(string value);
        partial void OnRemarksed();
        partial void OnFullNameing(string value);
        partial void OnFullNameed();
        partial void OnCreatedDateing(DateTime value);
        partial void OnCreatedDateed();

        #endregion

        #region int ID

        private int _ID;
        [Required]
        public int ID
        {
            get { return _ID; }
            set
            {
                if (!object.Equals(_ID, value))
                {
                    OnIDing(value);
                    _ID = value;
                    IsModified = true;
                    OnIDed();
                }
            }
        }

        #endregion

        #region int ClientCaseID

        private int _ClientCaseID;
        [Required]
        public int ClientCaseID
        {
            get { return _ClientCaseID; }
            set
            {
                if (!object.Equals(_ClientCaseID, value))
                {
                    OnClientCaseIDing(value);
                    _ClientCaseID = value;
                    IsModified = true;
                    OnClientCaseIDed();
                }
            }
        }

        #endregion

        #region byte? CaseStatus

        private byte? _CaseStatus;
        public byte? CaseStatus
        {
            get { return _CaseStatus; }
            set
            {
                if (!object.Equals(_CaseStatus, value))
                {
                    OnCaseStatusing(value);
                    _CaseStatus = value;
                    IsModified = true;
                    OnCaseStatused();
                }
            }
        }

        #endregion

        #region string Remarks

        private string _Remarks;
        [Required]
        [MaxLength(16)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                if (!object.Equals(_Remarks, value))
                {
                    OnRemarksing(value);
                    _Remarks = value;
                    IsModified = true;
                    OnRemarksed();
                }
            }
        }

        #endregion

        #region string FullName

        private string _FullName;
        [Required]
        [MaxLength(150)]
        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (!object.Equals(_FullName, value))
                {
                    OnFullNameing(value);
                    _FullName = value;
                    IsModified = true;
                    OnFullNameed();
                }
            }
        }

        #endregion

        #region DateTime CreatedDate

        private DateTime _CreatedDate;
        [Required]
        [DateIncludeTime]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (!object.Equals(_CreatedDate, value))
                {
                    OnCreatedDateing(value);
                    _CreatedDate = value;
                    IsModified = true;
                    OnCreatedDateed();
                }
            }
        }

        #endregion

    }
}

