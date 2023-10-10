using NorthwindModel;
using ODataWebExperimental.Northwind.Model;
using Warehouse.DTOs;
using Warehouse.Interfaces;

namespace Warehouse.Services
{
    public class NorthwindService : INorthwindService
    {
        private readonly NorthwindEntities context;

        public NorthwindService(IConfiguration configuration)
        {
            var serviceUri = configuration["OdataServiceUri"];
            this.context = new NorthwindEntities(new Uri(serviceUri));
        }
        public IEnumerable<Product> GetProducts(string productName)
        {
            var products = context.Products.Where(x => x.ProductName.StartsWith(productName)).ToList();

            return products.OrderBy(x => x.ProductName);
        }

        public IEnumerable<SupplierDTO> GetSuppliersOrdersSum()
        {
            var orderDetails = context.Order_Details.ToList();
            var products = context.Products.ToList();
            var suppliers = context.Suppliers.ToList();

            var productIdsWithOrderdPrice = orderDetails.GroupBy(x => x.ProductID)
                                .ToDictionary(x => x.Key,
                                              x => x.Sum(y => y.UnitPrice * (decimal)(1 - y.Discount) * y.Quantity));
            
            var orderedProductIds = productIdsWithOrderdPrice.Select(x => x.Key);
            var orderdProductsWithSupplierIds = products.Where(x => orderedProductIds.Contains(x.ProductID)).ToDictionary(x => x.ProductID, x => x.SupplierID).Distinct();

            var orderdProductsWithSupplierCompanyname = new Dictionary<int, string>();
            var suppliersWithoutOrder = new List<SupplierDTO>();
            foreach (var supplier in suppliers)
            {
                var productWithSupplier = orderdProductsWithSupplierIds.FirstOrDefault(x => x.Value == supplier.SupplierID);
                if (productWithSupplier.Key != 0 && productWithSupplier.Value != null)
                {
                    orderdProductsWithSupplierCompanyname.Add(productWithSupplier.Key, supplier.CompanyName);
                }
                else 
                {
                    suppliersWithoutOrder.Add(new SupplierDTO { CompanyName = supplier.CompanyName, OrderSum = 0 });
                }
            }

            var supplierWithOrderedSum = productIdsWithOrderdPrice.Join(orderdProductsWithSupplierCompanyname,
                                                      orderProduct => orderProduct.Key,
                                                      productWithSupplier => productWithSupplier.Key,
                                                      (orderedProduct, productWithSupplier) => new SupplierDTO
                                                      {
                                                          CompanyName = productWithSupplier.Value,
                                                          OrderSum = orderedProduct.Value
                                                      });

            return supplierWithOrderedSum.Concat(suppliersWithoutOrder).OrderByDescending(x => x.OrderSum);
        }

        public IEnumerable<Employee> GetAllEmployees() 
        {
            var employees = context.Employees.ToList();
            
            return employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            var suppliers = context.Suppliers.ToList();

            return suppliers.OrderBy(x => x.CompanyName);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = context.Categories.ToList();

            return categories.OrderBy(x => x.CategoryName);
        }
    }
}
