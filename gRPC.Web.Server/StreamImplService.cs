
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

public class StreamImplService: StreamService.StreamServiceBase {
    
    private readonly List<string> _messages = new List<string>()
    {
      "Hello",
      "World",
      "!"
    };
    
    public override async Task FetchResponse(
        Request request, 
        IServerStreamWriter<Response> responseStream, 
        ServerCallContext context)
    {
        var i = 0;
        while (!context.CancellationToken.IsCancellationRequested)
        {
            i++;
            foreach (var message in _messages)
            {
                await responseStream.WriteAsync(new Response()
                {
                    Result = $"{message} {i}"
                });

                Thread.Sleep(750);
            }
        }
    }
}