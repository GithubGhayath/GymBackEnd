using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.WorkoutPlanStatusDtos;

public record class UpdateWorkoutPlanStatusDto([Required][StringLength(50)] string Status);

