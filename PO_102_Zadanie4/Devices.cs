using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_102_Zadanie4
{
    public interface IDevice
    {
        enum State { Off, On, Standby };

        virtual void PowerOn()
        {
            ++Counter;
            ConsoleHelpers.WriteLine("Powering on device...", ConsoleHelpers.DeviceColor);
            SetState(State.On);
        }

        virtual void PowerOff()
        {
            ConsoleHelpers.WriteLine("Powering off device...", ConsoleHelpers.DeviceColor);
            SetState(State.Off);
        }

        virtual void StandbyOn()
        {
            ConsoleHelpers.WriteLine("Device entering standby mode...", ConsoleHelpers.DeviceColor);
            SetState(State.Standby);
        }

        virtual void StandbyOff()
        {
            ConsoleHelpers.WriteLine("Device leaving standby mode...", ConsoleHelpers.DeviceColor);
            SetState(State.On);
        }

        protected abstract void SetState(State state);

        int Counter { get; set; }
    }
    
    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);

        new void SetState(State state) => CurrentState = state;

        new void PowerOn()
        {
            ConsoleHelpers.WriteLine("Powering on printer...", ConsoleHelpers.PrinterColor);
            SetState(State.On);
        }

        new void PowerOff()
        {
            ConsoleHelpers.WriteLine("Shutting down printer...", ConsoleHelpers.PrinterColor);
            SetState(State.Off);
        }

        new void StandbyOn()
        {
            ConsoleHelpers.WriteLine("Printer entering standby mode...", ConsoleHelpers.PrinterColor);
            SetState(State.Standby);
        }

        new void StandbyOff()
        {
            ConsoleHelpers.WriteLine("Printer leaving standby mode...", ConsoleHelpers.PrinterColor);
            SetState(State.On);
        }

        State CurrentState { get; protected set; }
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);

        new void PowerOn()
        {
            ConsoleHelpers.WriteLine("Powering on scanner...", ConsoleHelpers.ScannerColor);
            SetState(State.On);
        }

        new void PowerOff()
        {
            ConsoleHelpers.WriteLine("Shutting down scanner...", ConsoleHelpers.ScannerColor);
            SetState(State.Off);
        }

        new void StandbyOn()
        {
            ConsoleHelpers.WriteLine("Scanner entering standby mode...", ConsoleHelpers.ScannerColor);
            SetState(State.Standby);
        }

        new void StandbyOff()
        {
            ConsoleHelpers.WriteLine("Scanner leaving standby mode...", ConsoleHelpers.ScannerColor);
            SetState(State.On);
        }

        new void SetState(State state) => CurrentState = state;
        State CurrentState { get; protected set; }
    }
}
