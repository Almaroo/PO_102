namespace PO_102_Zadanie4
{
    public class Copier : IScanner, IPrinter
    {
        public int ScanCounter { get; private set; }
        public int PrintCounter { get; private set; }

        public void Print(in IDocument document)
        {
            // Czy da się to zrobić z wykorzystaniem GetState() w momencie gdy jest zdefiniowana w IDevice jako abstract?
            if (((IPrinter) this).CurrentState == IDevice.State.Off)
                return;
            
            // wake up requested device if necessary
            if (((IPrinter) this).CurrentState == IDevice.State.Standby)
                ((IPrinter) this).StandbyOff();

            // send second module to sleep if necessary
            if (((IScanner) this).CurrentState != IDevice.State.Standby)
                ((IScanner) this).StandbyOn();
            
            ++PrintCounter;
            ConsoleHelpers.WriteLine($"{DateTime.Now:g} Print: {document.GetFileName()}.{document.GetFormatType()}", ConsoleHelpers.PrinterColor);

            if (PrintCounter % 3 == 0)
            {
                ConsoleHelpers.WriteLine("Printed 3 documents, printer will now enter standby mode", ConsoleHelpers.PrinterColor);
                ((IPrinter) this).StandbyOn();
            }
        }

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

            // send second module to sleep if necessary
            if (((IPrinter) this).CurrentState != IDevice.State.Standby)
                ((IPrinter) this).StandbyOn();
            
            ++ScanCounter;

            document = formatType switch 
            {
                IDocument.FormatType.JPG => new ImageDocument($"ImageScan{ScanCounter}"),
                IDocument.FormatType.PDF => new PDFDocument($"PDFScan{ScanCounter}"),
                IDocument.FormatType.TXT => new TextDocument($"TextScan{ScanCounter}"),
                _ => throw new ArgumentException($"Incorrect {nameof(formatType)}"),
            };

            ConsoleHelpers.WriteLine($"{DateTime.Now:g} Scan: {document.GetFileName()}.{document.GetFormatType().ToString().ToLower()}", ConsoleHelpers.ScannerColor);
            
            if (ScanCounter % 2 == 0)
            {
                ConsoleHelpers.WriteLine("Scanned 2 documents, scanner will now enter standby mode", ConsoleHelpers.ScannerColor);
                ((IScanner) this).StandbyOn();
            }
        }

        public void ScanAndPrint() 
        {
            Scan(out IDocument document, IDocument.FormatType.JPG);
            Print(document);
        }
        
        void IDevice.SetState(IDevice.State state)
        {
            ((IScanner) this).SetState(state);
            ((IPrinter) this).SetState(state);
        }

        IDevice.State IPrinter.CurrentState { get; set; }

        IDevice.State IScanner.CurrentState { get; set; }

        public int Counter { get; set; }
    }
}
