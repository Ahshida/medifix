using System;

namespace DBO.Data.Interfaces
{
    public interface IDate
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
    public interface ICreatedDate
    {
        DateTime CreatedDate { get; set; }
    }
    public interface IModifiedDate
    {
        DateTime ModifiedDate { get; set; }
    }
}
