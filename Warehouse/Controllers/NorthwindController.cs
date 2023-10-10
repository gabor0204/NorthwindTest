using Microsoft.AspNetCore.Mvc;
using NorthwindModel;
using System.Net;
using Warehouse.DTOs;
using Warehouse.Interfaces;

namespace Warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class NorthwindController : ControllerBase
    {
        private readonly ILogger<NorthwindController> logger;
        private readonly INorthwindService northwindService;

        public NorthwindController(ILogger<NorthwindController> logger, INorthwindService northwindService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.northwindService = northwindService ?? throw new ArgumentNullException(nameof(northwindService));
        }

        [HttpGet("GetProducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public IEnumerable<Product> GetProducts(string? productName)
        {
            try
            {
                logger.LogInformation("GetProducts is called.");
                var result = this.northwindService.GetProducts(productName ?? string.Empty);
                logger.LogInformation("GetProducts calling successful.");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured during GetProducts calling.");
                throw ex;
            }
        }

        [HttpGet("GetSupplierOrderSum")]
        [ProducesResponseType(typeof(IEnumerable<SupplierDTO>), (int)HttpStatusCode.OK)]
        public IEnumerable<SupplierDTO> GetSupplierOrderSum()
        {
            try
            {
                logger.LogInformation("GetSupplierOrderSum is called.");
                var result = this.northwindService.GetSuppliersOrdersSum();
                logger.LogInformation("GetSupplierOrderSum calling successful.");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured during GetSupplierOrderSum calling.");
                throw ex;
            }
        }

        [HttpGet("GetAllEmployees")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), (int)HttpStatusCode.OK)]
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                logger.LogInformation("GetAllEmployees is called.");
                var result = this.northwindService.GetAllEmployees();
                logger.LogInformation("GetAllEmployees calling successful.");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured during GetAllEmployees calling.");
                throw ex;
            }
        }

        [HttpGet("GetAllSuppliers")]
        [ProducesResponseType(typeof(IEnumerable<Supplier>), (int)HttpStatusCode.OK)]
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            try
            {
                logger.LogInformation("GetAllSuppliers is called.");
                var result = this.northwindService.GetAllSuppliers();
                logger.LogInformation("GetAllSuppliers calling successful.");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured during GetAllSuppliers calling.");
                throw ex;
            }
        }

        [HttpGet("GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                logger.LogInformation("GetAllCategories is called.");
                var result = this.northwindService.GetAllCategories();
                logger.LogInformation("GetAllCategories calling successful.");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured during GetAllCategories calling.");
                throw ex;
            }
        }
    }
}