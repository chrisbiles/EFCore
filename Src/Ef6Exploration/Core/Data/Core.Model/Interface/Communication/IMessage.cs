namespace Core.Model.Interface.Communication;

public interface IMessage
{
    string From { get; set; }
    // ReSharper disable once InconsistentNaming
    string CC { get; set; }
    string Title { get; set; }
    string Body { get; set; }
    DateTime Sent { get; set; }
}