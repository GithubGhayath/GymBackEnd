using System;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Data;

public static class DataExtensions
{
    public static void MigrateDb (this WebApplication app)
    {
        using var Scope = app.Services.CreateAsyncScope();
        var dbContext = Scope.ServiceProvider.GetRequiredService<GymContext>();
        dbContext.Database.Migrate();
    }
}
