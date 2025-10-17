using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceAnalyzer.Domain.Enums
{
    public enum InvoiceStatus
    {
        Pending = 0,
        Processed = 1,
        Failed = 2,
        Reviewed = 3
    }
}
