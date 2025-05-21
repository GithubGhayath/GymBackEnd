using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.WorkoutPlanDtos;

public record class UpdateWorkoutPlan([Required]string PlanName, [Required]int Duration,
[Required] decimal Price,[Required] int StatusID);

