using NorthwindModel;
using Warehouse.DTOs;

namespace Warehouse.Interfaces
{
    public interface INorthwindService
    {
        IEnumerable<Product> GetProducts(string productName);

        IEnumerable<SupplierDTO> GetSuppliersOrdersSum();

        IEnumerable<Employee> GetAllEmployees();

        IEnumerable<Supplier> GetAllSuppliers();

        IEnumerable<Category> GetAllCategories();
    }
}
