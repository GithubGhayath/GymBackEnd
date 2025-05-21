using System;

namespace GymBackEnd.Entities;

public class Payments
{
   

    public int Id{get;set;}
    public int PaymentStatusID{get;set;}
    public int PaymentMethodID{get;set;}
    public int SubscriberID{get;set;}
    public DateTime? PaymentDate{get;set;}
    public decimal TotlaFees{get;set;}
    public string? PaymentDetails{get;set;}
    public PaymentStatus?Status{get;set;}
    public PaymentMethods? PaymentMethod{get;set;}
}
