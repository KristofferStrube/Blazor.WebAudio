namespace BlazorServer;

public interface IEvaluationContext<T> where T : EvaluationContext, IEvaluationContext<T>
{
    public static abstract T Create(IServiceProvider provider);
}
