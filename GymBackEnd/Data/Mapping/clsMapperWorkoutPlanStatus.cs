using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.WorkoutPlanStatusDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperWorkoutPlanStatus
{
    public static WorkoutPlanStatusSummaryDto ToSummaryDto(this WorkoutPlanStatus workout)
    {
        return new(workout.Status!);
    }
     public static WorkoutPlanStatusDetailsDto ToDetialDto(this WorkoutPlanStatus workout)
    {
        return new(workout.Id,workout.Status!);
    }
    public static WorkoutPlanStatus ToRecord(this AddWorkoutPlanStatusDto Plan)
    {
        return new WorkoutPlanStatus{Status=Plan.Status};
    }
    public static WorkoutPlanStatus ToRecord(this UpdateWorkoutPlanStatusDto Plan,int ID)
    {
        return new WorkoutPlanStatus{Id=ID,Status=Plan.Status};
    }
}
