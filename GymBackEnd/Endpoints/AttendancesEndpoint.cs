using System;
using GymBackEnd.Data;
using GymBackEnd.Data.Mapping;
using GymBackEnd.Endpoints.EndpointsConfigurations;
using GymBackEnd.GymDtos.AttendancesDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Endpoints;

public static class AttendancesEndpoint
{
    private const string _GetbyIDEndpointName = "GetAttendanceById";

    public static RouteGroupBuilder AttendanceEndpoints(this WebApplication app)
    {
        var EndpointGroup = app.MapGroup("Attendance").WithParameterValidation() .RequireAuthorization();
        EndpointGroup.MapGet("/", async (GymContext dbcontext) =>
        {

            return Results.Ok(await dbcontext.Attendances
             .Include(attendance => attendance.Subscriber)
             .Select(attendance => attendance.ToSummaryDto())
             .AsNoTracking().ToListAsync());

        });

        EndpointGroup.MapGet("/{id}", async (int id, GymContext dbContext) =>
        {

            Entities.Attendances? attendance = await dbContext.Attendances.FindAsync(id);
            return attendance is null ? Results.NotFound() : Results.Ok(attendance.ToDto());


        }).WithName(_GetbyIDEndpointName);

        EndpointGroup.MapPost("/", async (AddAttendancesDto Attendance, GymContext dbcontext) =>
        {
            try
            {
                //throw new ArgumentException("This is for test");
                Entities.Attendances NewAttendance = Attendance.ToRecord();


                await dbcontext.Attendances.AddAsync(NewAttendance);
                await dbcontext.SaveChangesAsync();


                //This is for tracking behavor of table 
                clsSettings.LoggerMessage("A new record added to attendance table",
                null, "GymBackEnd/Endpoints/AttendancesEndpoint/AddAttendance",
                clsSettings.enLogLevel.Info, dbcontext);

                return Results.CreatedAtRoute(_GetbyIDEndpointName,
                 new { id = NewAttendance.Id }, NewAttendance.ToDto());
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while adding new attendace",
                ex.Message, "GymBackEnd/Endpoints/AttendancesEndpoint",
                clsSettings.enLogLevel.Error, dbcontext);
                return Results.Problem("Internal server error");
            }
        });

        EndpointGroup.MapPut("/{id}", async (int id, UpdateAttendacesDto attendance, GymContext dbContext) =>
        {
            try
            {
                var Atttendance = await dbContext.Attendances.FindAsync(id);
                if (Atttendance == null) return Results.NotFound();
                Atttendance.CheckoutTime = attendance.CheckoutTime;
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                clsSettings.LoggerMessage("Error while updatingattendace",
                ex.Message, "GymBackEnd/Endpoints/AttendancesEndpoint/UpdateEndpoint.",
                clsSettings.enLogLevel.Error, dbContext);
                return Results.Problem("Internal server error");
            }
        });
        return EndpointGroup;
    }
}