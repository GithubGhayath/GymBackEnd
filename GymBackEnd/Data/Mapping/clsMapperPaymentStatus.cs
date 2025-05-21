using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.PaymentStatusDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperPaymentStatus
{
    public static PaymenstatusSummaryDto ToSummaryDto(this PaymentStatus status)
    {
        return new(status.StatusName!);
    }
    public static PaymentStatusDetailsDto ToDetailDto(this PaymentStatus status)
    {
        return new(status.Id,status.StatusName!);
    }
    public static PaymentStatus ToRecord(this AddNewPaymentStatusDto status)
    {
        return new PaymentStatus{StatusName=status.StatusName};
    }
    public static PaymentStatus ToRecord(this UpdatePaymentStatusDto status,int ID)
    {
        return new PaymentStatus{Id=ID,StatusName=status.StatusName};
    }
}
