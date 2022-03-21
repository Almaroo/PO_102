namespace PO_102_Zadanie2;

public interface IFax : IDevice
{
    void Send(in IDocument document, string faxNumber);
    int SentFaxCounter { get; }
}