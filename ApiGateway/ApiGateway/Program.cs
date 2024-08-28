public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        
        // API Gateway ve yönlendirme yapılandırması
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapReverseProxy();
        });

        app.Run();
    }
}
