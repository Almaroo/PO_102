using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_102_Zadanie1
{
    interface IFax : IDevice
    {
        void Send(in IDocument document, string faxNumber);
    }
}
