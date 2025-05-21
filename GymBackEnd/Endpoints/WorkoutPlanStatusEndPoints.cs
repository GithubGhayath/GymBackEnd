using System;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.GymDtos.WorkoutPlanStatusDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class WorkoutPlanStatusEndPoints
{
    private const string _GetByIdEndpoint = "GetWorkoutPlanStatus";
    public static RouteGroupBuilder WorkoutPlanStatusEndpoint(this WebApplication app)
    {
        var WorkoutPlanStatus = app.MapGroup("WorkoutPlanStatus")
        .WithParameterValidation()
        .RequireAuthorization();

        WorkoutPlanStatus.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.WorkoutPlanStatus.Select(w => w.ToSummaryDto()).ToListAsync());
        });

        WorkoutPlanStatus.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            var PlanStatus = await dbContext.WorkoutPlanStatus.FindAsync(id);
            if (PlanStatus == null) return Results.NotFound();
            return Results.Ok(PlanStatus.ToDetialDto());
        }).WithName(_GetByIdEndpoint);


        WorkoutPlanStatus.MapPost("/", async (AddWorkoutPlanStatusDto NewStatus, GymContext dbContext) =>
        {
            try
            {
                var PlanStatus = NewStatus.ToRecord();
                await dbContext.WorkoutPlanStatus.AddAsync(PlanStatus);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute
                (_GetByIdEndpoint, new { id = PlanStatus.Id }, PlanStatus.ToDetialDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new workoutplanstatus",
                ex.Message, "GymBackEnd/Endpoints/WorkoutPlanStatusEndPoints/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        WorkoutPlanStatus.MapPut("/{id}", async (int id, UpdateWorkoutPlanStatusDto UpdatedStatus, GymContext dbContext) =>
        {
            try
            {
                var CurrentStatus = await dbContext.WorkoutPlanStatus.FindAsync(id);
                dbContext.Entry(CurrentStatus!).CurrentValues.SetValues(UpdatedStatus.ToRecord(id));
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while updating workoutplanstatus",
                ex.Message, "GymBackEnd/Endpoints/WorkoutPlanStatusEndPoints/UpdateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        WorkoutPlanStatus.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            try
            {
                await dbContext.WorkoutPlanStatus.Where(w => w.Id == id).ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while deleting workoutplanstatus",
                ex.Message, "GymBackEnd/Endpoints/WorkoutPlanStatusEndPoints/DeleteEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        return WorkoutPlanStatus;
    }
}
