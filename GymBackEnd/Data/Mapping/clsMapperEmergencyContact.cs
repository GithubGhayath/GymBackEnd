using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.EmergencyContactsDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperEmergencyContact
{
    public static EmergencyContacts ToRecord(this AddEmergencyContactsDto contact)
    {
        return new EmergencyContacts
        {
            Name=contact.Name,
            PhoneNumber=contact.PhoneNumber,
            Relationship=contact.Relationship
        };
    }

     public static EmergencyContacts ToRecord(this UpdateEmergencyContactsDto contact,int ID)
    {
        return new EmergencyContacts
        {
            Id=ID,
            Name=contact.Name,
            PhoneNumber=contact.PhoneNumber,
            Relationship=contact.Relationship
        };
    }

    public static EmergencyContactsDetailsDto ToDetailDto(this EmergencyContacts contact)
    {
        return new(contact.Id,contact.Name!,contact.PhoneNumber!,contact.Relationship!);
    }

     public static EmergencyContactSummaryDetailsDto ToSummaryDto(this EmergencyContacts contact)
    {
        return new(contact.Name!,contact.PhoneNumber!,contact.Relationship!);
    }
}
