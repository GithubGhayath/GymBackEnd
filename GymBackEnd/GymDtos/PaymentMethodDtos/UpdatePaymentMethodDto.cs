using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.PaymentMethodDtos;
 
public record class UpdatePaymentMethodDto([Required]string MethodName);

