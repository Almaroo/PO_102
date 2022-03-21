using System;

namespace PO_102_Zadanie1
{
    public class MultifunctionalDevice : Copier, IFax
    {
        public int SentFaxCounter { get; private set; }
        
        public void Send(in IDocument document, string faxNumber)
        {
            if(GetState() == IDevice.State.off)
                return;

            ++SentFaxCounter;

            Console.WriteLine($"{DateTime.Now:g} Send via fax to {faxNumber}: {document.GetFileName()}");
        }

        public void ScanAndSend(string faxNumber)
        {
            Scan(out IDocument document, IDocument.FormatType.TXT);
            Send(document, faxNumber);
        }
    }
}