using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Basic_DependecyInjection.Controllers
{
    public class IOperation
    {
        Guid OperationID { get; }
    }

    public interface IOperationTransient : IOperation
    {
    }

    public interface IOperationScoped : IOperation
    {
    }

    public interface IOperationSingleton : IOperation
    {
    }
}

