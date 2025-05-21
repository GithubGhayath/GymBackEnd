using System.ComponentModel.DataAnnotations;

namespace GymBackEnd.GymDtos.PaymentsDtos;

public record class AddNewPayment
    (
    int PaymentStatusID,
   [Required] int PaymentMehtodID,
    [Required] int SubscriberID,
   [Required] decimal TotlaFees,
   [Required] string PaymentDetails);

