namespace PO_102_Zadanie2;

public interface ICopier : IScanner, IPrinter
{
    void ScanAndPrint();
}