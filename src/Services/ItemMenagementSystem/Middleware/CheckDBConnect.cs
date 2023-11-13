using ItemManagementSystem.Model.Repository;

namespace ItemManagementSystem.Middleware
{
    public class CheckDBConnect
    {
        private readonly RequestDelegate _next;
        public CheckDBConnect(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IRepository repository)
        {
            if (await repository.IsConnectAsync() is false)
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            else
                await _next(context);
        }
    }
}
