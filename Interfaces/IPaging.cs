using DBO.Data.Attributes;

namespace DBO.Data.Interfaces
{
    [AssignProperty("rn", typeof(HiddenAttribute))]
    public interface IPaging
    {
        long? rn { get; set; }
    }
}
