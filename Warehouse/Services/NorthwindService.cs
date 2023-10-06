using NorthwindModel;
using ODataWebExperimental.Northwind.Model;
using Warehouse.Interfaces;

namespace Warehouse.Services
{
    public class NorthwindService : INorthwindService
    {
        private readonly ILogger<NorthwindService> logger;
        private readonly NorthwindEntities context;

        public NorthwindService(ILogger<NorthwindService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            var serviceUri = configuration["OdataServiceUri"];
            this.context = new NorthwindEntities(new Uri(serviceUri));
        }

        public IEnumerable<Employee> GetAllEmployees() 
        {
            var employees = context.Employees.ToList();
            
            return employees;
        }
    }
}
