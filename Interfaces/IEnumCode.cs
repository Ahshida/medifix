
namespace DBO.Data.Interfaces
{
    public partial interface IEnumCode : ITable
    {
        byte ID { get; set; }
        string Code { get; set; }
    }
}
