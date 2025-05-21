using System;
using System.Data.Common;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.GymDtos.MembershipTypesDtos;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class MembershipTypesEndpoints
{
    private const string _GetMembershipByIDName = "GetMembershipByID";
    public static RouteGroupBuilder MembershipTypesEndPoints(this WebApplication app)
    {
        var EndpointGroup = app.MapGroup("MembershipTypes").WithParameterValidation() .RequireAuthorization();
        EndpointGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.MembershipTypes.Select(m => m.ToSummaryDto()).ToListAsync());
        });
        EndpointGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            var Type = await dbContext.MembershipTypes.FindAsync(id);

            if (Type == null) return Results.NotFound();
            return Results.Ok(Type.ToDetailDto());
        }).WithName(_GetMembershipByIDName);

        EndpointGroup.MapPost("/", async (AddMembershipTypesDto Type, GymContext dbContext) =>
        {
            try
            {
                var NewType = Type.ToRecord();
                await dbContext.MembershipTypes.AddAsync(NewType);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute
                (_GetMembershipByIDName, new { id = NewType.Id }, NewType.ToDetailDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new MembershipType",
                ex.Message, "GymBackEnd/Endpoints/MembershipTypesEndpoints/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapPut("/{id}", async (int id, UpdateMembershipTypesDto Type, GymContext dbContext) =>
        {
            try
            {
                var CurrentType = await dbContext.MembershipTypes.FindAsync(id);
                if (CurrentType == null) return Results.NotFound();
                dbContext.Entry(CurrentType!).CurrentValues.SetValues(Type.ToRecord(id));
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while Updating MembershipType",
                ex.Message, "GymBackEnd/Endpoints/MembershipTypesEndpoints/UpdateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            try
            {
                await dbContext.MembershipTypes.Where(m => m.Id == id).ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while Deleting MembershipType",
                ex.Message, "GymBackEnd/Endpoints/MembershipTypesEndpoints/DeleteEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        return EndpointGroup;
    }
}
