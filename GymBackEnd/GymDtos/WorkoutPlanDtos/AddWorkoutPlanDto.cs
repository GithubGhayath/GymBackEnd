using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.WorkoutPlanDtos;

public record class AddWorkoutPlanDto
([Required]string PlanName,[Required]int Duration,[Required]decimal Price,[Required]int StatusID,int SubscriberID );
