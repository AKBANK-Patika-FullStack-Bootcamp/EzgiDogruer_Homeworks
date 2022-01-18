
using DryCleanerAPI.Log;
namespace DryCleanerAPI.Log
{
    public class Logger
    {
       
        public void createLog(string Message)
        {
            string Path = @"C:\Users\ezgid\VisualStudio\\DryCleanerAPI\\DryCleanerAPI\Log\";
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            FileStream fs = new FileStream(Path + fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString() + " : " + Message);
            sw.Flush();
            sw.Close();
            fs.Close();
            
        }
    }
}
