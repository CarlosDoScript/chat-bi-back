
namespace Chat.Bi.SharedKernel.Cqrs.Implementations;

public class Mediator(
    IServiceProvider serviceProvider
    )
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = serviceProvider.GetService(handlerType) 
            ?? throw new InvalidOperationException($"Handler não encontrado para {request.GetType().Name}");

        return await ((Task<TResponse>)handler.Handle((dynamic)request));
    }
}
