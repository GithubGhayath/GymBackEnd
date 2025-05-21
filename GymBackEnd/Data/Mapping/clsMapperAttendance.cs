using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.AttendancesDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperAttendance
{
    public static Attendances ToRecord(this AddAttendancesDto attendance)
    {
        return new Attendances
        {
            CheckInTime = attendance.CheckInTime,
            SubscriberID = attendance.SubscriberID
        };
    }

    public static AttendancesDetailsDto ToDto(this Attendances attendances)
    {
        return new
        (
            attendances.Id,
            attendances.SubscriberID,
            attendances.CheckInTime,
            attendances.CheckoutTime
        );
    }

    public static AttendanceSummaryDto ToSummaryDto(this Attendances attendances)
    {
        return new
        (
           SubscriberName: attendances.Subscriber!.FullName!,
           CheckInTime: attendances.CheckInTime,
           CheckOutTime: attendances.CheckoutTime
        );
    }

}
