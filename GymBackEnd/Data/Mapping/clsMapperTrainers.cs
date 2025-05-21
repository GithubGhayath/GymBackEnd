using System;
using GymBackEnd.Entities;
using GymBackEnd.GymDtos.TrainersDtos;

namespace GymBackEnd.Data.Mapping;

public static class clsMapperTrainers
{
    public static Trainers ToRecord(this AddTrainerDto traner)
    {
        return new Trainers
        {
            Name = traner.Name,
            Spacialty = traner.Spacialty,
            PhoneNumber = traner.PhoneNumber,
            Email = traner.Email,
            availability = traner.availability,
            Price = traner.Price
        };
    }
    public static Trainers ToRecord(this UpdateTrainerDto traner,int Id)
    {
        return new Trainers
        {
            Id=Id,
            Name = traner.Name,
            Spacialty = traner.Spacialty,
            PhoneNumber = traner.PhoneNumber,
            Email = traner.Email,
            availability = traner.availability,
            Price = traner.Price
        };
    }

    public static TrainerDetailsDto ToDetailDto(this Trainers traner)
    {
        return new
        (
            traner.Id,
            traner.Name!,
            traner.Spacialty!,
            traner.PhoneNumber!,
            traner.Email!,
            traner.availability!,
            traner.Price
        );
    }

    public static TrainerSummaryDto ToSummaryDto(this Trainers traner)
    {
        return new
        (
            traner.Name!,
            traner.Spacialty!,
            traner.PhoneNumber!,
            traner.Email!,
            traner.availability!,
            traner.Price
        );
    }
}
