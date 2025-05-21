using System;

namespace GymBackEnd.Entities;

public class Trainers
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Spacialty { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? availability { get; set; }
    public decimal Price { get; set; }
}
