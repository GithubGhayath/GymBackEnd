using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.WorkoutPlanDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperWorkoutPlan
{
    public static WorkoutPlanSummaryDto ToSummaryDto(this WorkoutPlans workout)
    {
        return new(workout.Subscriber!.FullName!,
        workout.PlanName!,workout.Duration,workout.Price,workout.Status!.Status!);
    }

    public static WorkoutPlanDetailsDto ToDetialDto(this WorkoutPlans workout)
    {
        return new(workout.Id,workout.SubscriberID,workout.PlanName!,workout.Duration,workout.Price,workout.StatusID);
    }
    public static WorkoutPlans ToRecord(this AddWorkoutPlanDto plan)
    {
        return new WorkoutPlans
        {
            PlanName=plan.PlanName,
            Duration=plan.Duration,
            Price=plan.Price,
            StatusID=plan.StatusID,
            SubscriberID=plan.SubscriberID
        };
    }
     public static WorkoutPlans ToRecord(this UpdateWorkoutPlan plan,int id,int SubscriberID)
    {
        return new WorkoutPlans
        {
            Id=id,
            PlanName=plan.PlanName,
            Duration=plan.Duration,
            Price=plan.Price,
            StatusID=plan.StatusID,
            SubscriberID=SubscriberID
        };
    }
}
