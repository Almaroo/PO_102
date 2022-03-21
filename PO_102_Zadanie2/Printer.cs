namespace PO_102_Zadanie2;

public class Printer : BaseDevice, IPrinter
{
    public int PrintCounter { get; private set; }
    
    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.off)
            return;
            
        ++PrintCounter;
        Console.WriteLine($"{DateTime.Now:g} Print: {document.GetFileName()}.{document.GetFormatType()}");
    }
}