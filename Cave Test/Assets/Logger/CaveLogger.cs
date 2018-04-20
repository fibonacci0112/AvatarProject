
namespace CaveLogger
{
    class ErrorLog
    {
        private static ErrorLog instance;
        private System.IO.StreamWriter file;

        private ErrorLog()
        {
            file = new System.IO.StreamWriter("../errorlog.txt", true);
        }

        ~ErrorLog()
        {
            file.Close();
        }

        public static ErrorLog GetInstance()
        {
            if (instance == null)
                instance = new ErrorLog();
            return instance;
        }

        public void Log(string log)
        {
            file.WriteLine(System.DateTime.Now.ToString() + " -> " + log);
            file.Flush();
        }
    }
}
