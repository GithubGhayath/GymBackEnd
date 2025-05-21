namespace GymBackEnd.GymDtos.WorkoutPlanDtos;

public record class WorkoutPlanDetailsDto(int Id,int SubscriberID,string PlanName,int Duration,decimal Price,int StatusID );

