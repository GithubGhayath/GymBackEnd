using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.PaymentMethodDtos;
 
public record class AddPaymentMethod([Required]string MethodName);

