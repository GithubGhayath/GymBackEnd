using System;
using GymBackEnd.Data;
using GymBackEnd.Entities;
namespace GymBackEnd.Endpoints.EndpointsConfigurations;

public static class clsSettings
{

    public enum enLogLevel { Error = 2, Warning = 1, Info = 0 };
    /// <summary>
    /// This type for inter mode of info 
    /// it contains three level:
    /// 2: means error.
    /// 1: means warning.
    /// 0: means info.
    /// this is helpfull for sort and create the logs in database.
    /// </summary>
    public static enLogLevel Mode { get; set; }

    public static void LoggerMessage(string Message, string? Exception, string ErrorLocation,
    enLogLevel Level, GymContext dbContext)
    {

        dbContext.Logs.Add(new Logs
        {
            Level = (byte)Level,
            Message = Message,
            Context = ErrorLocation,
            Exception = Exception,
            TimeStamp=DateTime.Now
        });
        dbContext.SaveChanges();
    }
}