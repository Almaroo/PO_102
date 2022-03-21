using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_102_Zadanie1
{
    public class Copier : BaseDevice, IScanner, IPrinter
    {
        public int ScanCounter { get; private set; }
        public int PrintCounter { get; private set; }

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.off)
                return;
            
            ++PrintCounter;
            Console.WriteLine($"{DateTime.Now:g} Print: {document.GetFileName()}.{document.GetFormatType()}");
        }

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

        public void ScanAndPrint() 
        {
            Scan(out IDocument document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}
