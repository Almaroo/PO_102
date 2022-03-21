namespace PO_102_Zadanie3;

public class Scanner : BaseDevice, IScanner
{
    public int ScanCounter { get; private set; }
    public void Scan(out IDocument document, IDocument.FormatType formatType)
    {
        if (GetState() == IDevice.State.off)
        {
            document = null;
            return;
        }

        ++ScanCounter;

        document = formatType switch 
        {
            IDocument.FormatType.JPG => new ImageDocument($"ImageScan{ScanCounter}"),
            IDocument.FormatType.PDF => new PDFDocument($"PDFScan{ScanCounter}"),
            IDocument.FormatType.TXT => new TextDocument($"TextScan{ScanCounter}"),
            _ => throw new ArgumentException($"Incorrect {nameof(formatType)}"),
        };

        Console.WriteLine($"{DateTime.Now:g} Scan: {document.GetFileName()}.{document.GetFormatType().ToString().ToLower()}");
    }
}