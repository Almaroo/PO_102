namespace PO_102_Zadanie3;

public interface IFax : IDevice
{
    void Send(in IDocument document, string faxNumber);
    int SentFaxCounter { get; }
}