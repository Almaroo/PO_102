namespace PO_102_Zadanie3;

public interface ICopier : IScanner, IPrinter
{
    void ScanAndPrint();
}