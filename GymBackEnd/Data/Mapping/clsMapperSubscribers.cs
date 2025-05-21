using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.SubscribersDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperSubscribers
{
    public static Subscribers ToRecord(this AddSubscriberDto subscriber)
    {
        return new Subscribers
        {
            EmergencyContactID = subscriber.EmergencyContactID,
            TrainerID = subscriber.TrainerID,
            MembershipTypeID = subscriber.MembershipTypeID,
            FullName = subscriber.FullName,
            Age = subscriber.Age,
            Height = subscriber.Height,
            Weight = subscriber.Weight,
            PhoneNumber = subscriber.PhoneNumber,
            Email = subscriber.Email,
            StartDate = DateTime.Now,
            Status = true,
            EndDate = null
        };
    }
    public static Subscribers ToRecord(this UpdateScbscriberDto subscriber,int Id)
    {
        return new Subscribers
        {
            Id=Id,
            EmergencyContactID = subscriber.EmergencyContactID,
            TrainerID = subscriber.TrainerID,
            MembershipTypeID = subscriber.MembershipTypeID,
            FullName = subscriber.FullName,
            Age = subscriber.Age,
            Height = subscriber.Height,
            Weight = subscriber.Weight,
            PhoneNumber = subscriber.PhoneNumber,
            Email = subscriber.Email,
            Status = subscriber.Status,
            EndDate = subscriber.EndDate
        };
    }
    public static SubscribersDetailsDto ToDetailDto(this Subscribers subscriber)
    {
        return new(
           subscriber.Id,
           subscriber.EmergencyContactID,
           subscriber.TrainerID,
           subscriber.MembershipTypeID,
           subscriber.FullName!,
           subscriber.Age,
           subscriber.Height,
           subscriber.Weight,
           subscriber.PhoneNumber!,
           subscriber.Email!,
           subscriber.StartDate,
           subscriber.EndDate,
           subscriber.Status
        );
    }
     public static SubscriberSummaryDto ToSummaryDto(this Subscribers subscriber)
    {
        return new(
           subscriber.FullName!,
           subscriber.Age,
           subscriber.Weight,
           subscriber.Height,
           subscriber.PhoneNumber!,
           subscriber.Email!,
           subscriber.Status
        );
    }
}
