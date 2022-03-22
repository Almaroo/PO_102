namespace PO_102_Zadanie5
{
    public class Copier : IPrinter, IScanner
    {
        private IScanner Scanner { get; }
        private IPrinter Printer { get; }

        public int ScanCounter => Scanner.Counter;
        public int PrintCounter => Printer.Counter;

        public Copier(IScanner scanner, IPrinter printer)
        {
            Scanner = scanner;
            Printer = printer;
        }
        
        public void Print(in IDocument document)
        {
            // send second module to sleep if necessary
            if (Scanner.CurrentState != IDevice.State.Standby)
               Scanner.StandbyOn();
            
            Printer.Print(in document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            // send second module to sleep if necessary
            if (Printer.CurrentState != IDevice.State.Standby)
                Printer.StandbyOn();
            
            Scanner.Scan(out document, formatType);
        }

        public void ScanAndPrint() 
        {
            Scan(out IDocument document, IDocument.FormatType.JPG);
            Print(document);
        }

        void IDevice.SetState(IDevice.State state)
        {
            Scanner.SetState(state);
            Printer.SetState(state);
        }

        public int Counter { get; set; }


        IDevice.State IScanner.CurrentState
        {
            get => Scanner.CurrentState;
            set => Scanner.SetState(value);
        }

        IDevice.State IPrinter.CurrentState 
        {
            get => Printer.CurrentState;
            set => Printer.SetState(value);
        }
    }
}
