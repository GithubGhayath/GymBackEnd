namespace GymBackEnd.GymDtos.PaymentsDtos;

public record class PaymentSummaryDto
   (int PaymentStatusID,
    int PaymentMehtodID,
    int SubscriberID,
    DateTime? PaymentDate,
    decimal TotlaFees,
    string PaymentDetails);
