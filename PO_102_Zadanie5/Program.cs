using System;

namespace PO_102_Zadanie5
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier(new Scanner(), new Printer());
            ((IDevice)xerox).PowerOn();
            
            ((IDevice) xerox).StandbyOn();
            ((IDevice) xerox).StandbyOff();
            
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);
            xerox.Print(in doc1);
            xerox.Print(in doc1);
            xerox.Print(in doc1);
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2, IDocument.FormatType.TXT);
            xerox.Scan(out doc2, IDocument.FormatType.TXT);
            xerox.Scan(out doc2, IDocument.FormatType.TXT);
            xerox.Scan(out doc2, IDocument.FormatType.TXT);
            xerox.Scan(out doc2, IDocument.FormatType.TXT);

            xerox.ScanAndPrint();
            Console.WriteLine(xerox.Counter);
            Console.WriteLine(xerox.PrintCounter);
            Console.WriteLine(xerox.ScanCounter);
        }
    }
}
