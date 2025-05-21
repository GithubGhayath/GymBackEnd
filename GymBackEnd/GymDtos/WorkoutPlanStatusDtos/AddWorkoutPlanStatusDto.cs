using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.WorkoutPlanStatusDtos;

public record class AddWorkoutPlanStatusDto
(
   [Required][StringLength(50)] string Status
);

