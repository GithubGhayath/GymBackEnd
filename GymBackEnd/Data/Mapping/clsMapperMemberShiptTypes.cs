using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.MembershipTypesDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperMemberShiptTypes
{
    public static MemeberShiptTypeSummaryDto ToSummaryDto(this MembershipTypes membership)
    {
        return new(membership.Type!,membership.Price,membership.Description!);
    }

    public static MembershipTypes ToRecord(this AddMembershipTypesDto Memberhship)
    {
        return new MembershipTypes
        {
            Price=Memberhship.Price,
            Description=Memberhship.Description,
            Type=Memberhship.Type
        };
    }

     public static MembershipTypes ToRecord(this UpdateMembershipTypesDto Memberhship,int Id)
    {
        return new MembershipTypes
        {
            Id=Id,
            Price=Memberhship.Price,
            Description=Memberhship.Description,
            Type=Memberhship.Type
        };
    }
    public static MembershipTypesDetailsDto ToDetailDto(this MembershipTypes Membership)
    {
        return new(Membership.Id,Membership.Type!,Membership.Price,Membership.Description!);
    }
}
