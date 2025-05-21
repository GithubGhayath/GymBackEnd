using System;
using System.Diagnostics;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.PaymentsDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperPayment
{
    public static PaymentSummaryDto ToSummaryDto(this Payments payment)
    {
        return new(payment.PaymentStatusID,payment.PaymentMethodID,payment.SubscriberID
        ,payment.PaymentDate,payment.TotlaFees,payment.PaymentDetails!);
    }
    public static PaymentDetailsDto ToDetailDto(this Payments payment)
    {
        return new(payment.Id,payment.PaymentStatusID,payment.PaymentMethodID,payment.SubscriberID
        ,payment.PaymentDate,payment.TotlaFees,payment.PaymentDetails!);
    }
    public static Payments ToRecord(this AddNewPayment payment)
    {
        return new Payments{
            PaymentStatusID=payment.PaymentStatusID,
            PaymentDate=DateTime.Now,
            SubscriberID=payment.SubscriberID,
            TotlaFees=payment.TotlaFees,
            PaymentDetails=payment.PaymentDetails,
            PaymentMethodID=payment.PaymentMehtodID
        };
    }

    public static Payments ToRecord(this UpdatePayment payment,DateTime? PaymentDate,int id,int PaymentMethodID,int Subscriberid)
    {
        return new Payments{
            Id=id,
            PaymentStatusID=payment.PaymentStatusID,
            TotlaFees=payment.TotlaFees,
            PaymentDetails=payment.PaymentDetails,
            PaymentMethodID=PaymentMethodID,
            SubscriberID=Subscriberid,
            PaymentDate=PaymentDate
        };
    }
}
