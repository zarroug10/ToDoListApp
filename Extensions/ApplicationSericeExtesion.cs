

using ToDoListApp.Data.Repositories;
using ToDoListApp.Interface;

namespace ToDoListApp.Extensions;

public static class ApplicationSericeExtesion
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IToDoRepository, ToDoRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}
