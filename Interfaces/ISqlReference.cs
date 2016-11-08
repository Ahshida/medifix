using System;
using System.Collections.Generic;

namespace DBO.Data.Objects
{
    public interface ISqlReference
    {
        Guid Guid { get; }
        string Sql { get; set; }
        Dictionary<string, Guid> SqlRelationShips { get; }
        object ReturnValue { get; set; }
        Type TableType { get; set; }
    }
}
