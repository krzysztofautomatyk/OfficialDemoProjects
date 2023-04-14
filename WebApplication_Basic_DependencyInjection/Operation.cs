namespace WebApplication_Basic_DependencyInjection
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Guid OperationId { get; set; }

        public Operation()
        {
            OperationId = Guid.NewGuid();
        }
    }
}
