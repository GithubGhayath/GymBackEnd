using System;

namespace GymBackEnd.Entities;

public class EmergencyContacts
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Relationship { get; set; }
}
