using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Basic_DependencyInjection.Controllers
{
    public class OperationController : ControllerBase
    {
        private readonly OperationService _operationService;
        private readonly IOperationTransient _operationTransient;
        private readonly IOperationScoped _operationScoped;
        private readonly IOperationSingleton _operationSingleton;

        public OperationController(OperationService operationService,
            IOperationTransient operationTransient,
            IOperationScoped operationScoped,
            IOperationSingleton operationSingleton)
        {
            _operationService = operationService;
            _operationTransient = operationTransient;
            _operationScoped = operationScoped;
            _operationSingleton = operationSingleton;
        }

        [HttpGet]
        [Route("/Operation")]
        public ActionResult Get()
        {
            var model = new
            {
                ControllerDependencies = new
                {
                    Transient = _operationTransient.OperationId,
                    Scoped = _operationScoped.OperationId,
                    Singleton = _operationSingleton.OperationId,
                },
                ServiceDependencies = new
                {
                    Transient = _operationService.TransientOperation.OperationId,
                    Scoped = _operationService.ScopedOperation.OperationId,
                    Singleton = _operationService.SingletonOperation.OperationId,
                }
            };

            return Ok(model); 
        }

        
    }
}
