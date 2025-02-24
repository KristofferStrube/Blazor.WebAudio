
namespace BlazorServer;

public class EvaluationContext : IEvaluationContext<EvaluationContext>
{
    public object? Result { get; set; }

    public Exception? Exception { get; set; }

    public Func<Task<object?>>? AfterRenderAsync { get; set; }

    public static EvaluationContext Create(IServiceProvider provider)
    {
        return new EvaluationContext();
    }
}
