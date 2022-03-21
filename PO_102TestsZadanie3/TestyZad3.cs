using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using PO_102_Zadanie3;


namespace PO_102_TestsZadanie3
{
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestCopier
    {
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOn()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOff()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOff()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1, IDocument.FormatType.TXT);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOn()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1, IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void Copier_Scan_FormatTypeDocument()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOff()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Copier_PrintCounter()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1, IDocument.FormatType.TXT);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanCounter()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1, IDocument.FormatType.TXT);
            IDocument doc2;
            copier.Scan(out doc2, IDocument.FormatType.TXT);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1, IDocument.FormatType.TXT);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, copier.ScanCounter);
        }

        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var copier = new Copier(new Scanner(), new Printer());
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1, IDocument.FormatType.TXT);
            IDocument doc2;
            copier.Scan(out doc2, IDocument.FormatType.TXT);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1, IDocument.FormatType.TXT);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, copier.Counter);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice(new Scanner(), new Printer(), new Fax());

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using var consoleOutput = new ConsoleRedirectionToStringWriter();
            multifunctionalDevice.Send(new PDFDocument("abc.pdf"), "+48-997-123456789");
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
        }
        
        [TestMethod]
        public void MultifunctionalDevice_Send_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice(new Scanner(), new Printer(), new Fax());
            multifunctionalDevice.PowerOn();
            
            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using var consoleOutput = new ConsoleRedirectionToStringWriter();
            multifunctionalDevice.Send(new PDFDocument("abc.pdf"), "+48-997-123456789");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
        }
        
        [TestMethod]
        public void MultifunctionalDevice_ScanAndSend_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice(new Scanner(), new Printer(), new Fax());
            
            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using var consoleOutput = new ConsoleRedirectionToStringWriter();
            multifunctionalDevice.ScanAndSend("+48-997-123456789");
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
        }
        
        [TestMethod]
        public void MultifunctionalDevice_ScanAndSend_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice(new Scanner(), new Printer(), new Fax());
            multifunctionalDevice.PowerOn();
            
            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using var consoleOutput = new ConsoleRedirectionToStringWriter();
            multifunctionalDevice.ScanAndSend("+48-997-123456789");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
        }

        [TestMethod]
        public void MultifunctionalDevice_SentFaxCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice(new Scanner(), new Printer(), new Fax());
            multifunctionalDevice.PowerOn();
            
            multifunctionalDevice.Send(new PDFDocument("hello_world"), "+48-997-123456789");
            multifunctionalDevice.Send(new PDFDocument("world_hello"), "+48-997-123456789");
            
            multifunctionalDevice.PowerOff();
            
            multifunctionalDevice.Send(new PDFDocument("device_off"), "+48-997-123456789");
            
            multifunctionalDevice.PowerOn();
            
            multifunctionalDevice.ScanAndSend("+48-997-123456789");

            Assert.IsTrue(multifunctionalDevice.SentFaxCounter == 3);
        }

    }
}
