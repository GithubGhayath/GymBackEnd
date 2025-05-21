using System;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.SubscribersDtos;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class SubscriberEndpoints
{
    private const string _GetSubscriberByIDEndPointName = "GetSubscriberByID";
    public static RouteGroupBuilder SubscribersEndpoint(this WebApplication app)
    {
        var EndpointGroup = app.MapGroup("Subscribers").WithParameterValidation() .RequireAuthorization();

        EndpointGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await
                dbContext.Subscribers.Select(s => s.ToSummaryDto())
                .AsNoTracking().ToListAsync());
        });

        EndpointGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {
            var Subscriber = await dbContext.Subscribers.FindAsync(id);
            if (Subscriber == null) return Results.NotFound();
            return Results.Ok(Subscriber.ToDetailDto());
        }).WithName(_GetSubscriberByIDEndPointName);

        EndpointGroup.MapPost("/", async (AddSubscriberDto Subscriber, GymContext dbContext) =>
        {
            try
            {
                Subscribers NewSubscriber = Subscriber.ToRecord();
                await dbContext.AddAsync(NewSubscriber);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute
                (_GetSubscriberByIDEndPointName, new { id = NewSubscriber.Id }, NewSubscriber.ToDetailDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new Subscriber",
                ex.Message, "GymBackEnd/Endpoints/SubscriberEndpoints/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        EndpointGroup.MapPut("/{id}", async (int id, UpdateScbscriberDto subscriber, GymContext dbContext) =>
        {
            try
            {
                var UpdatedSubscriber = await dbContext.Subscribers.FindAsync(id);
                if (UpdatedSubscriber == null) return Results.NotFound();
                dbContext.Entry(UpdatedSubscriber!).CurrentValues.SetValues(subscriber.ToRecord(id));
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while updating Subscriber",
                ex.Message, "GymBackEnd/Endpoints/SubscriberEndpoints/UpdateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        EndpointGroup.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            try
            {
                //Get subscriber if exists
                var Subscriber = await dbContext.Subscribers.FindAsync(id);


                if (Subscriber != null)
                {
                    //For remove Emergency Contact about this subscriber
                    dbContext.EmergencyContacts.Remove
                    (dbContext.EmergencyContacts.Find(Subscriber.EmergencyContactID)!);

                    //For remove all attendances about this subscriber
                    dbContext.Attendances.RemoveRange
                    (dbContext.Attendances.Where(a => a.SubscriberID == id).ToList());


                    //For remove all Payment about this subscriber
                    dbContext.Payments.RemoveRange
                    (dbContext.Payments.Where(p => p.SubscriberID == id).ToList());


                    //For remove  Workout plans about this subscriber
                    dbContext.WorkoutPlans.RemoveRange
                    (dbContext.WorkoutPlans.Where(p => p.SubscriberID == id).ToList());

                    //Then remove the existing subscriber
                    dbContext.Subscribers.Remove(Subscriber);
                    await dbContext.SaveChangesAsync();
                }
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while deleting Subscriber",
                ex.Message, "GymBackEnd/Endpoints/SubscriberEndpoints/deleteEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        return EndpointGroup;
    }
}
