using ConsoleApp_Basic_DependecyInjection.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Basic_DependecyInjection
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {

        public Guid OperationId { get; set; }

        public Operation() {
            OperationId = Guid.NewGuid();
        }

    }
 
}
