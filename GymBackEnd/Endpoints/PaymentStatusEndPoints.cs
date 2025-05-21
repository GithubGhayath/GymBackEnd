using System;
using System.Runtime.CompilerServices;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.GymDtos.PaymentStatusDtos;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class PaymentStatusEndPoints
{
    private const string _GetByIdEndPointName = "GetPaymentStatusByID";

    public static RouteGroupBuilder PaymentStatusEndPoint(this WebApplication app)
    {
        var EndpointGroup = app.MapGroup("PaymentStatus").WithParameterValidation() .RequireAuthorization();


        EndpointGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.PaymentStatus.Select(p => p.ToSummaryDto()).ToListAsync());
        });

        EndpointGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            var status = await dbContext.PaymentStatus.FindAsync(id);
            if (status == null) return Results.NoContent();
            return Results.Ok(status.ToDetailDto());
        }).WithName(_GetByIdEndPointName);

        EndpointGroup.MapPost("/", async (AddNewPaymentStatusDto Status, GymContext dbContext) =>
        {
            try
            {
                var NewStatus = Status.ToRecord();
                await dbContext.PaymentStatus.AddAsync(NewStatus);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute
                (_GetByIdEndPointName, new { id = NewStatus.Id }, NewStatus.ToDetailDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new PaymentStatus",
                ex.Message, "GymBackEnd/Endpoints/PaymentStatusEndPoints/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapPut("/{id}", async (int id, UpdatePaymentStatusDto status, GymContext dbContext) =>
        {
            try
            {
                var UpdatedStatus = await dbContext.PaymentStatus.FindAsync(id);
                dbContext.Entry(UpdatedStatus!).CurrentValues.SetValues(status.ToRecord(id));
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while Updating PaymentStatus",
                ex.Message, "GymBackEnd/Endpoints/PaymentStatusEndPoints/UpdateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            try
            {
                await dbContext.PaymentStatus.Where(p => p.Id == id).ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while deleting PaymentStatus",
                ex.Message, "GymBackEnd/Endpoints/PaymentStatusEndPoints/deleteEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        return EndpointGroup;
    }

}
