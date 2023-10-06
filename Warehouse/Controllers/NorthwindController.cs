using Microsoft.AspNetCore.Mvc;
using NorthwindModel;
using Warehouse.Interfaces;

namespace Warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NorthwindController : ControllerBase
    {
        private readonly ILogger<NorthwindController> logger;
        private readonly INorthwindService northwindService;

        public NorthwindController(ILogger<NorthwindController> logger, INorthwindService northwindService)
        {
            this.logger = logger;
            this.northwindService = northwindService ?? throw new ArgumentNullException(nameof(northwindService));
        }

        [HttpGet(Name = "GetAllEmployees")]
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
    }
}