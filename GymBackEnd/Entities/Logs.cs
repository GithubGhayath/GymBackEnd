using System;

namespace GymBackEnd.Entities;

public class Logs
{
   public int ID{get;set;}
   public string? Message{get;set;}
    public string? Exception{get;set;}
    //the level defind what is type of log Error: 2 , Warning: 1, Info: 0
    public byte Level{get;set;}
    public string? Context{get;set;}

    public DateTime TimeStamp{get;set;}
}
