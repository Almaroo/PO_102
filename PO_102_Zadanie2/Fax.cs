namespace PO_102_Zadanie2;

public class Fax : BaseDevice, IFax
{
    public void Send(in IDocument document, string faxNumber)
    {
        if(GetState() == IDevice.State.off)
            return;

        ++SentFaxCounter;

        Console.WriteLine($"{DateTime.Now:g} Send via fax to {faxNumber}: {document.GetFileName()}");
    }

    public int SentFaxCounter { get; private set; }
}