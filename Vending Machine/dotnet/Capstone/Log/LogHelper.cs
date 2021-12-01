using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public static class LogHelper 
    {
        public static void Log(LogTypes target, string message)
        {
            if (target == LogTypes.Audit)
            {
                AuditLog au = new AuditLog();
                au.Log(message);
            }
            else if (target == LogTypes.Sales)
            {
                SalesLog sa = new SalesLog();
                sa.Log(message);
            }
        }

    }
}
