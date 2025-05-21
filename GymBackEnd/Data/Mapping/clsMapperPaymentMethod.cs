using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.PaymentMethodDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperPaymentMethod
{
    public static PaymentMethodSummaryDto ToSummaryDto(this PaymentMethods payment)
    {
        return new(payment.MethodName!);
    }
    public static PaymentMethodDetails ToDetailDto(this PaymentMethods payment)
    {
        return new(payment.Id, payment.MethodName!);
    }
    public static PaymentMethods ToRecord(this AddPaymentMethod method)
    {
        return new PaymentMethods { MethodName = method.MethodName };
    }
    public static PaymentMethods ToRecord(this UpdatePaymentMethodDto method, int id)
    {
        return new PaymentMethods { Id = id, MethodName = method.MethodName };
    }

}
