
namespace DBO.Data.Interfaces
{
    public interface ICreator
    {
        int CreatedBy { get; set; }
        int ModifiedBy { get; set; }
    }
}
