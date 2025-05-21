using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.PaymentsDtos;

public record class UpdatePayment
    (
    [Required]int PaymentStatusID,
   [Required] decimal TotlaFees,
    string PaymentDetails);

