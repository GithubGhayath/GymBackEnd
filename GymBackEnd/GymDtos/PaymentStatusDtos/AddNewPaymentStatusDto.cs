using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.PaymentStatusDtos;

public record class AddNewPaymentStatusDto([Required]string StatusName);

