using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.SubscribersDtos;

public record class UpdateScbscriberDto
(
   [Required] int EmergencyContactID,
    int? TrainerID,
    [Required] int MembershipTypeID,
    [Required] string FullName,
    [Required] int Age,
    [Required] int Height,
    [Required] int Weight,
    [Required] string PhoneNumber,
    [Required] string Email,
   [Required] DateTime EndDate,
   [Required] bool Status
);