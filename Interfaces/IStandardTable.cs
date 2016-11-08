using System;

namespace DBO.Data.Interfaces
{
    public interface IStandardTable : ITable
    {
        int ID { get; set; }
        bool IsModified { get; set; }
        int CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        int ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
