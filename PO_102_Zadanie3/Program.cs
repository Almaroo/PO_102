using PO_102_Zadanie3;

var xerox = new Copier(new Scanner(), new Printer());
xerox.PowerOn();
IDocument doc1 = new PDFDocument("aaa.pdf");
xerox.Print(in doc1);

IDocument doc2;
xerox.Scan(out doc2, IDocument.FormatType.TXT);

xerox.ScanAndPrint();
Console.WriteLine(xerox.Counter);
Console.WriteLine(xerox.PrintCounter);
Console.WriteLine(xerox.ScanCounter);


var multifunctionalDevice = new MultifunctionalDevice(new Scanner(), new Printer(), new Fax());
multifunctionalDevice.PowerOn();
IDocument pdfDocument = new PDFDocument("hello_world");
multifunctionalDevice.Print(pdfDocument);

multifunctionalDevice.Scan(out IDocument imageDocument, IDocument.FormatType.JPG);
multifunctionalDevice.Send(imageDocument, "+48-997-123456789");
            
multifunctionalDevice.ScanAndPrint();
multifunctionalDevice.ScanAndSend("+48-997-123456789");

Console.WriteLine(multifunctionalDevice.Counter);
Console.WriteLine(multifunctionalDevice.PrintCounter);
Console.WriteLine(multifunctionalDevice.ScanCounter);
Console.WriteLine(multifunctionalDevice.SentFaxCounter);