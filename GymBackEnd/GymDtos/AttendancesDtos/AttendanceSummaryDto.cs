namespace GymBackEnd.GymDtos.AttendancesDtos;

public record class AttendanceSummaryDto(string SubscriberName,
DateTime? CheckInTime, DateTime? CheckOutTime);
