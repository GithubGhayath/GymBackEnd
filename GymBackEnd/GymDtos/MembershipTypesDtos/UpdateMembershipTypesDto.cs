using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.MembershipTypesDtos;

public record class UpdateMembershipTypesDto([Required]string Type, decimal Price,[Required] string Description);