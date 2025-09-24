namespace Common.Shared.Log;

public record HttpLog(string IP, string URL, long TimeUsed, string Request, string Respon);