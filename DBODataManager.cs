using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using DBO.Data.Interfaces;
using DBO.Data.Managers;
using DBO.Data.Objects;
using DBO.Data.Objects.Enums;

namespace DBO.Data
{
    public class DBODataManager : DataManager
    {
        protected override ConnectionStringSettings ConnectionString
        {
            get
            {
                if (_connectionString == null)
                    _connectionString = ConfigurationManager.ConnectionStrings["DBOConnectionString"];
                return _connectionString;
            }
        }
    }
}
