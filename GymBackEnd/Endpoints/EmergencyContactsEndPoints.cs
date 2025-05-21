using System;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.GymDtos.EmergencyContactsDtos;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class EmergencyContactsEndPoints
{
    private const string _GetByIdEndpointName = "GetEmergencyContactByID";
    public static RouteGroupBuilder EmergencyContactEndpoint(this WebApplication app)
    {
        var EndpointGroup = app.MapGroup("EmergencyContacts").WithParameterValidation() .RequireAuthorization();
        EndpointGroup.MapGet("/", async (GymContext dbContext) =>
        {
            return Results.Ok(await dbContext.EmergencyContacts.Select(e => e.ToSummaryDto()).ToListAsync());
        });
        EndpointGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {

            var Contact = await dbContext.EmergencyContacts.FindAsync(id);
            if (Contact == null) return Results.NotFound();
            return Results.Ok(Contact.ToDetailDto());
        }).WithName(_GetByIdEndpointName);
        EndpointGroup.MapPost("/", async (AddEmergencyContactsDto contact, GymContext dbContext) =>
        {
            try
            {
            var NewContact = contact.ToRecord();
            await dbContext.EmergencyContacts.AddAsync(NewContact);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute
            (_GetByIdEndpointName, new { id = NewContact.Id }, NewContact.ToDetailDto());
            }  catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new EmergencyContact",
                ex.Message, "GymBackEnd/Endpoints/EmergencyContactsEndpoint/AddEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        EndpointGroup.MapPut("/{id}", async (int id, UpdateEmergencyContactsDto contact, GymContext dbContext) =>
        {
            try{
            var UpdatedContact =await dbContext.EmergencyContacts.FindAsync(id);
            if (UpdatedContact == null) return Results.NotFound();
            dbContext.Entry(UpdatedContact!).CurrentValues.SetValues(contact.ToRecord(id));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
            } catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while Updating  EmergencyContact",
                ex.Message, "GymBackEnd/Endpoints/EmergencyContactsEndpoint/UpdateEndpiont.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });

        return EndpointGroup;
    }
}
