using System;

namespace GymBackEnd.Entities;

public class WorkoutPlans
{
    public int Id { get; set; }
    public string? PlanName { get; set; }

    //This column modifyed and make this table referece to subscribers table using SubscriberID key
    public int SubscriberID { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public int StatusID { get; set; }
    public WorkoutPlanStatus? Status { get; set; }
    public Subscribers?Subscriber{get;set;}
}
