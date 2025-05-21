using System;

namespace GymBackEnd.Entities;

public class MembershipTypes
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
