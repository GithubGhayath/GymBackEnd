using System;
using Microsoft.AspNetCore.Http.HttpResults;
using GymBackEnd.GymDtos.WorkoutPlanDtos;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using GymBackEnd.Endpoints.EndpointsConfigurations;
public static class WorkoutPlanEndPoint
{
    private const string _GetByIdEndpointName = "eGetByID";
    public static RouteGroupBuilder WorkoutPlanEndpoint(this WebApplication app)
    {
        var WorkoutPlansGroup = app.MapGroup("WorkoutPlan").WithParameterValidation() .RequireAuthorization();

        WorkoutPlansGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.WorkoutPlans.Include(s => s.Subscriber).Include(s => s.Status)
            .Select(w => w.ToSummaryDto()).AsNoTracking().ToListAsync());
        });

        WorkoutPlansGroup.MapGet("/{id}", (int id, GymContext dbContext) =>
        {
            var Plan = dbContext.WorkoutPlans.Find(id);
            if (Plan == null) return Results.NotFound();
            return Results.Ok(Plan.ToDetialDto());
        }).WithName(_GetByIdEndpointName);

        WorkoutPlansGroup.MapPost("/", (AddWorkoutPlanDto Plan, GymContext dbContext) =>
        {
            try
            {
                var NewPlan = Plan.ToRecord();
                dbContext.WorkoutPlans.Add(NewPlan);
                dbContext.SaveChanges();
                return Results.CreatedAtRoute
                (_GetByIdEndpointName, new { id = NewPlan.Id }, NewPlan.ToDetialDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new workoutPlan",
                ex.Message, "GymBackEnd/Endpoints/WorkoutPlanEndPoint/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        WorkoutPlansGroup.MapPut("/{id}", (int id, UpdateWorkoutPlan Plan, GymContext dbContext) =>
        {
            try
            {
                var UpdatedWorkOutPlan = dbContext.WorkoutPlans.Find(id);
                if (UpdatedWorkOutPlan == null) return Results.NotFound();
                dbContext.Entry(UpdatedWorkOutPlan!).CurrentValues.SetValues(Plan.ToRecord(id, UpdatedWorkOutPlan!.SubscriberID));
                dbContext.SaveChanges();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while updating workoutPlan",
                ex.Message, "GymBackEnd/Endpoints/WorkoutPlanEndPoint/updateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        WorkoutPlansGroup.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            try
            {
                await dbContext.WorkoutPlans.Where(w => w.Id == id).ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while deleting workoutPlan",
                ex.Message, "GymBackEnd/Endpoints/WorkoutPlanEndPoint/deleteEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        return WorkoutPlansGroup;
    }
}