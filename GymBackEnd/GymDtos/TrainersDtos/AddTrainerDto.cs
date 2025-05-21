using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.TrainersDtos;

public record class AddTrainerDto 

([Required]string Name,[Required] string Spacialty,[Required] string PhoneNumber,
[Required]string Email,[Required] string availability,[Required] decimal Price);