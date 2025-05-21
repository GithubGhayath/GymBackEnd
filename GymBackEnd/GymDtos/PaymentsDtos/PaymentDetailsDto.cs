namespace GymBackEnd.GymDtos.PaymentsDtos;

public record class PaymentDetailsDto
    (int Id,
    int PaymentStatusID,
    int PaymentMehtodID,
    int SubscriberID,
    DateTime? PaymentDate,
    decimal TotlaFees,
    string PaymentDetails);
