namespace PO_102_Zadanie5;

public class Scanner : IScanner
{
    public void Scan(out IDocument document, IDocument.FormatType formatType)
    {
        // Czy da się to zrobić z wykorzystaniem GetState() w momencie gdy jest zdefiniowana w IDevice jako abstract?
        if (((IScanner) this).CurrentState == IDevice.State.Off)
        {
            document = null;
            return;
        }
            
        // wake up requested device if necessary
        if (((IScanner) this).CurrentState == IDevice.State.Standby)
            ((IScanner) this).StandbyOff();

        ++Counter;

        document = formatType switch 
        {
            IDocument.FormatType.JPG => new ImageDocument($"ImageScan{Counter}"),
            IDocument.FormatType.PDF => new PDFDocument($"PDFScan{Counter}"),
            IDocument.FormatType.TXT => new TextDocument($"TextScan{Counter}"),
            _ => throw new ArgumentException($"Incorrect {nameof(formatType)}"),
        };

        ConsoleHelpers.WriteLine($"{DateTime.Now:g} Scan: {document.GetFileName()}.{document.GetFormatType().ToString().ToLower()}", ConsoleHelpers.ScannerColor);
            
        if (Counter % 2 == 0)
        {
            ConsoleHelpers.WriteLine("Scanned 2 documents, scanner will now enter standby mode", ConsoleHelpers.ScannerColor);
            ((IScanner) this).StandbyOn();
        }
    }

    void IDevice.SetState(IDevice.State state)
    {
        ((IScanner) this).CurrentState = state;
    }

    IDevice.State IScanner.CurrentState { get; set; }

    public int Counter { get; set; }
}