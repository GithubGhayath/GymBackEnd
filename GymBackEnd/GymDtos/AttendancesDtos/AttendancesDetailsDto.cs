namespace GymBackEnd.GymDtos.AttendancesDtos;

public record class AttendancesDetailsDto(int Id, int SubscriberID, DateTime? CheckInTime, DateTime? CheckoutTime);

