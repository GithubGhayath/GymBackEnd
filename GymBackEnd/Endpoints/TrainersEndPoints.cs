using System;
using System.Data.Common;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.TrainersDtos;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class TrainersEndPoints
{
    private const string _GetTrainerByIDEndpointName = "GetTrainerById";
    public static RouteGroupBuilder TrainerEndpoints(this WebApplication app)
    {
        var EndpointGroup = app.MapGroup("Trainers").WithParameterValidation() .RequireAuthorization();
        EndpointGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.Trainers.Select(trainer => trainer.ToSummaryDto()).ToListAsync());
        });
        EndpointGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            var ExisttingTraner = await dbContext.Trainers.FindAsync(id);
            if (ExisttingTraner == null) return Results.NotFound();
            return Results.Ok(ExisttingTraner.ToDetailDto());
        }).WithName(_GetTrainerByIDEndpointName);
        EndpointGroup.MapPost("/", async (AddTrainerDto trainer, GymContext dbContext) =>
        {
            try
            {
                Trainers NewTrainer = trainer.ToRecord();
                await dbContext.Trainers.AddAsync(NewTrainer);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute
                (_GetTrainerByIDEndpointName, new { id = NewTrainer.Id }, NewTrainer.ToDetailDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new Trainer",
                ex.Message, "GymBackEnd/Endpoints/TrainersEndPoints/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapPut("/{id}", async (int id, UpdateTrainerDto trainer, GymContext dbContext) =>
        {
            try
            {
                //if we pass a instance from trainer type it will tranking the primary key 
                //so we need to pass the Primary key (ID) to instance to avoide exception
                var UpdatedTrainer = await dbContext.Trainers.FindAsync(id);
                if (UpdatedTrainer == null) return Results.NotFound();
                dbContext.Entry(UpdatedTrainer!).CurrentValues.SetValues(trainer.ToRecord(id));
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while updating Trainer",
                ex.Message, "GymBackEnd/Endpoints/TrainersEndPoints/updateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            try
            {
                await dbContext.Trainers.Where(t => t.Id == id).ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while deleting Trainer",
                ex.Message, "GymBackEnd/Endpoints/TrainersEndPoints/deleteEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        return EndpointGroup;
    }
}
