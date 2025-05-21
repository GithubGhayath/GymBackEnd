using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.PaymentStatusDtos;

public record class UpdatePaymentStatusDto([Required]string StatusName);

 