namespace PO_102_Zadanie3;

public class MultifunctionalDevice : BaseDevice, ICopier, IFax
{
    private Copier Copier { get; }
    private IFax Fax { get; }

    public int ScanCounter => Copier.ScanCounter;
    public int PrintCounter => Copier.PrintCounter;
    public int SentFaxCounter => Fax.SentFaxCounter;

    public MultifunctionalDevice(IScanner scanner, IPrinter printer, IFax fax)
    {
        Copier = new Copier(scanner, printer);
        Fax = fax;
    }
    
    public override void PowerOn()
    {
        if (GetState() == IDevice.State.on)
            return;

        base.PowerOn();
        Copier.PowerOn();
        Fax.PowerOn();
    }

    public override void PowerOff()
    {
        if(GetState() == IDevice.State.off)
            return;
        
        base.PowerOff();
        Copier.PowerOff();
        Fax.PowerOff();
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType)
    {
        Copier.Scan(out document, formatType);
    }

    public void Print(in IDocument document)
    {
        Copier.Print(in document);
    }
    
    public void ScanAndPrint()
    {
        Copier.ScanAndPrint();
    }

    public void Send(in IDocument document, string faxNumber)
    {
        Fax.Send(in document, faxNumber);
    }
    
    public void ScanAndSend(string faxNumber)
    {
        Scan(out IDocument document, IDocument.FormatType.TXT);
        Send(document, faxNumber);
    }
}