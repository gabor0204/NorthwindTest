using NorthwindModel;

namespace Warehouse.Interfaces
{
    public interface INorthwindService
    {
        IEnumerable<Employee> GetAllEmployees();
    }
}
