using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.AttendancesDtos;

public record class UpdateAttendacesDto
(
    [Required] DateTime CheckoutTime
);

