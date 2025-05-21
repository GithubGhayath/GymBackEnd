using System;

namespace GymBackEnd.Endpoints.EndpointsConfigurations;

public static class EndpointConfig
{
    public static void BuildEndPoints(this WebApplication app)
    {
        app.WorkoutPlanStatusEndpoint();
        app.WorkoutPlanEndpoint();
        app.PaymentMethodEndPoints();
        app.PaymentStatusEndPoint();
        app.PaymentEndPoint();
        app.AttendanceEndpoints();
        app.SubscribersEndpoint();
        app.MembershipTypesEndPoints();
        app.EmergencyContactEndpoint();
        app.TrainerEndpoints();
    }
}
