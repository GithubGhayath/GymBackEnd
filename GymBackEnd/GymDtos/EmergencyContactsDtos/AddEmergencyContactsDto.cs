using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.EmergencyContactsDtos;

public record class AddEmergencyContactsDto(
    [Required][StringLength(20)] string Name,
    [Required] string PhoneNumber,
    [Required][StringLength(30)] string Relationship);
