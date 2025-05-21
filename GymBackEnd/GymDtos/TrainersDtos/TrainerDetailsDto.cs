namespace GymBackEnd.GymDtos.TrainersDtos;

public record class TrainerDetailsDto
(int Id, string Name, string Spacialty, string PhoneNumber,
string Email, string availability, decimal Price);