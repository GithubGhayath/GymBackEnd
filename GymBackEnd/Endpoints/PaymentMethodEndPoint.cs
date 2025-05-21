using System;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.GymDtos.PaymentMethodDtos;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class PaymentMethodEndPoint
{
    private const string _GetByIdEndPointName = "GetMethodByID";


    public static RouteGroupBuilder PaymentMethodEndPoints(this WebApplication app)
    {
        var PaymentMethodGroup = app.MapGroup("PaymentMethod").WithParameterValidation() .RequireAuthorization();

        PaymentMethodGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.PaymentMethods.Select(p => p.ToSummaryDto()).ToListAsync());
        });

        PaymentMethodGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            var Method = await dbContext.PaymentMethods.FindAsync(id);
            if (Method == null) return Results.NotFound();
            return Results.Ok(Method.ToDetailDto());
        }).WithName(_GetByIdEndPointName);
        PaymentMethodGroup.MapPost("/", async (AddPaymentMethod Method, GymContext dbContext) =>
        {
            try
            {
                var NewMethod = Method.ToRecord();
                await dbContext.PaymentMethods.AddAsync(NewMethod);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute
                (_GetByIdEndPointName, new { id = NewMethod.Id }, NewMethod.ToDetailDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new PaymentMethod",
                ex.Message, "GymBackEnd/Endpoints/PaymentMethodEndPoints/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        PaymentMethodGroup.MapPut("/{id}", async (int id, UpdatePaymentMethodDto Method, GymContext dbContext) =>
        {
            try
            {
                var UpdatedMethod = await dbContext.PaymentMethods.FindAsync(id);
                if (UpdatedMethod == null) return Results.NotFound();
                dbContext.Entry(UpdatedMethod!).CurrentValues.SetValues(Method.ToRecord(id));
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while updating Paymentmethod",
                ex.Message, "GymBackEnd/Endpoints/PaymentMethodEndPoints/UpdateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        PaymentMethodGroup.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            try
            {
                await dbContext.PaymentMethods.Where(p => p.Id == id).ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while delete Paymentmethod",
                ex.Message, "GymBackEnd/Endpoints/PaymentMethodEndPoints/DeleteEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        return PaymentMethodGroup;
    }
}
