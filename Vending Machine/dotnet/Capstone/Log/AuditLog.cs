using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class AuditLog : LogBase
    {
        private string FileName { get; } = "Log.txt";
        private string Location { get; } = Environment.CurrentDirectory;
        private string FullPath { get; }

        public AuditLog()
        {
            FullPath = Path.Combine(Location, FileName);
        }

        public override void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter(FullPath, true))
            {
                sw.WriteLine(message);
            }
        }    
    }
}
