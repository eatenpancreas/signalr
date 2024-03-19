using SignalRApp.SubscribeTableDependencies;

namespace SignalRApp.MiddlewareExtensions;

public static class ApplicationBuilderExtensions {
    public static void UseProductTableDependency(this IApplicationBuilder app, string? connectionString) {
        if (connectionString != null) {
            var serviceProvider = app.ApplicationServices;
            var productTableDependency = serviceProvider.GetService<ISubscribeProductTableDependency>();
            productTableDependency?.SubscribeTableDependency(connectionString);
        }
    }
}