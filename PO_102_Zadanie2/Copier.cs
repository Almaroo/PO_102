namespace PO_102_Zadanie2;

public class Copier: BaseDevice, ICopier
{
    public int PrintCounter => Printer.PrintCounter;
    public int ScanCounter => Scanner.ScanCounter;

    private IScanner Scanner { get; }
    private IPrinter Printer { get; }
    
    public Copier(IScanner scanner, IPrinter printer)
    {
        Scanner = scanner;
        Printer = printer;
    }

    public override void PowerOn()
    {
        if (GetState() == IDevice.State.on)
            return;

        base.PowerOn();
        Scanner.PowerOn();
        Printer.PowerOn();
    }

    public override void PowerOff()
    {
        if (GetState() == IDevice.State.off)
            return;

        base.PowerOff();
        Scanner.PowerOff();
        Printer.PowerOff();
    }

    public void Print(in IDocument document)
    {
        Printer.Print(in document);
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType)
    {
        Scanner.Scan(out document, formatType);
    }
    
    public void ScanAndPrint() 
    {
        Scan(out IDocument document, IDocument.FormatType.JPG);
        Print(document);
    }
}