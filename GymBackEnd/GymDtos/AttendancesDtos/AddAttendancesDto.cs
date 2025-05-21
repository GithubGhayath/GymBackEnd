using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.AttendancesDtos;

public record class AddAttendancesDto
(
    int SubscriberID,
    [Required] DateTime CheckInTime
);