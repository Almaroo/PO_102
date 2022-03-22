namespace PO_102_Zadanie5;

public class Printer : IPrinter
{
    public void Print(in IDocument document)
    {
        // Czy da się to zrobić z wykorzystaniem GetState() w momencie gdy jest zdefiniowana w IDevice jako abstract?
        if (((IPrinter) this).CurrentState == IDevice.State.Off)
            return;
            
        // wake up requested device if necessary
        if (((IPrinter) this).CurrentState == IDevice.State.Standby)
            ((IPrinter) this).StandbyOff();
        
        ++Counter;
        ConsoleHelpers.WriteLine($"{DateTime.Now:g} Print: {document.GetFileName()}.{document.GetFormatType()}", ConsoleHelpers.PrinterColor);

        if (Counter % 3 == 0)
        {
            ConsoleHelpers.WriteLine("Printed 3 documents, printer will now enter standby mode", ConsoleHelpers.PrinterColor);
            ((IPrinter) this).StandbyOn();
        }
    }

    void IDevice.SetState(IDevice.State state)
    {
        ((IPrinter) this).CurrentState = state;
    }

    IDevice.State IPrinter.CurrentState { get; set; }

    public int Counter { get; set; }
}