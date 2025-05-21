using System;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.GymDtos.PaymentsDtos;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class PaymentEndPoints
{

    private const string _GetByIdEndpointName = "GetPaymentByID";
    public static RouteGroupBuilder PaymentEndPoint(this WebApplication app)
    {
        var EndpointGroup = app.MapGroup("Payment").WithParameterValidation() .RequireAuthorization();

        EndpointGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.Payments.Select(p => p.ToSummaryDto()).ToListAsync());
        });
        EndpointGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            var Payment = await dbContext.Payments.FindAsync(id);
            if (Payment == null) return Results.NotFound();
            return Results.Ok(Payment.ToDetailDto());
        }).WithName(_GetByIdEndpointName);
        EndpointGroup.MapPost("/", async (AddNewPayment payment, GymContext dbContext) =>
        {
            try
            {
                var NewPayment = payment.ToRecord();
                await dbContext.Payments.AddAsync(NewPayment);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute
                (_GetByIdEndpointName, new { id = NewPayment.Id }, NewPayment.ToDetailDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new Payment",
                ex.Message, "GymBackEnd/Endpoints/PaymentEndPoints/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapPut("/{id}", async (int id, UpdatePayment payment, GymContext dbContext) =>
        {
            try
            {
                var UpdatedPayment = await dbContext.Payments.FindAsync(id);
                if (UpdatedPayment == null) return Results.NotFound();
                dbContext.Entry(UpdatedPayment!).
                CurrentValues.SetValues
                (payment.ToRecord(UpdatedPayment.PaymentDate, id, UpdatedPayment.PaymentMethodID, UpdatedPayment.SubscriberID));
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while Updating Payment",
                ex.Message, "GymBackEnd/Endpoints/PaymentEndPoints/UpdateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
            return Results.NoContent();
        });
        return EndpointGroup;
    }
}