using System;
using GymBackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymBackEnd.Data;

public class GymContext(DbContextOptions<GymContext> options) : DbContext(options)
{
    public DbSet<Subscribers> Subscribers{get;set;}
    public DbSet<MembershipTypes>MembershipTypes{get;set;}
    public DbSet<Trainers>Trainers{get;set;}
    public DbSet<Payments>Payments{get;set;}
    public DbSet<PaymentMethods>PaymentMethods{get;set;}
    public DbSet<PaymentStatus>PaymentStatus{get;set;}
    public DbSet<Attendances>Attendances{get;set;}
    public DbSet<EmergencyContacts>EmergencyContacts{get;set;}
    public DbSet<WorkoutPlans>WorkoutPlans{get;set;}
    public DbSet<WorkoutPlanStatus>WorkoutPlanStatus{get;set;}
    public DbSet<Logs>Logs{get;set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MembershipTypes>().HasData(
            new {Id=1,Type="Basic Monthly",Price=30m,Description="Standard access, valid for 30 days"},
            new {Id=2,Type="Basic Annual ",Price=300m,Description="Standard access, valid for 1 year"},
            new {Id=3,Type="Premium Monthly",Price=60m,Description="Extra perks like personal training, sauna access, etc., valid for 30 days"},
            new {Id=4,Type="Premium Annual",Price=600m,Description="All premium features, valid for 1 year"},
            new {Id=5,Type="Day Pass",Price=10m,Description="One-time access for a single day"},
            new {Id=6,Type="Trial",Price=.0m,Description="Free limited access for new users"}
        );
        modelBuilder.Entity<PaymentMethods>().HasData(
            new{Id=1,MethodName="Cash"},
            new{Id=2,MethodName="Credit Card"},
            new{Id=3,MethodName="Debit Card"},
            new{Id=4,MethodName="Bank Transfer"},
            new{Id=5,MethodName="Mobile Payment"},
            new{Id=6,MethodName="Cryptocurrency"}
        );
        modelBuilder.Entity<PaymentStatus>().HasData(
            new{Id=1,StatusName="Pending"},
            new{Id=2,StatusName="Completed"},
            new{Id=3,StatusName="Failed"},
            new{Id=4,StatusName="Refunded"},
            new{Id=5,StatusName="Cancelled"}
        );

        modelBuilder.Entity<WorkoutPlanStatus>().HasData(
            new WorkoutPlanStatus { Id = 1, Status = "Active" },
            new WorkoutPlanStatus { Id = 2, Status = "Inactive" },
            new WorkoutPlanStatus { Id = 3, Status = "Completed" },
            new WorkoutPlanStatus { Id = 4, Status = "Paused" },
            new WorkoutPlanStatus { Id = 5, Status = "Cancelled" }
        );
    }
}
