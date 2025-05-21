namespace GymBackEnd.GymDtos.SubscribersDtos;

public record class SubscribersDetailsDto
(
    int Id,
    int EmergencyContactID,
    int? TrainerID,
    int MembershipTypeID,
    string FullName,
    int Age,
    int Height,
    int Weight,
    string PhoneNumber,
    string Email,
    DateTime StartDate,
    DateTime? EndDate,
    bool Status
);
