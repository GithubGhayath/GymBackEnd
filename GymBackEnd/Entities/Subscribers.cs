namespace GymBackEnd.Entities;

public class Subscribers
{
    public int Id { get; set; }
    public int EmergencyContactID { get; set; }
    //This column deleted and move it to workoutPlans table
    //public int? WorkPlanID { get; set; }
    // public WorkoutPlans?WorkoutPlan{get;set;}
    public int? TrainerID { get; set; }
    public int MembershipTypeID { get; set; }
    public string? FullName { get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool Status { get; set; }
    public Payments? Payment{get;set;}
    public Trainers? Trainer{get;set;}
    public MembershipTypes? MembershipType{get;set;}
    public EmergencyContacts? EmergencyContact{get;set;}
}
