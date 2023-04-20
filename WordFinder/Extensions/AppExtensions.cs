using WordFinder.Middlewares;

namespace WordFinder.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHanlderMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
