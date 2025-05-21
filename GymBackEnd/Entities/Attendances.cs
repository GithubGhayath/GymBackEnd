using System;

namespace GymBackEnd.Entities;

public class Attendances
{
    public int Id { get; set; }
    public int SubscriberID { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckoutTime { get; set; }
    public Subscribers?Subscriber{get;set;}
}
