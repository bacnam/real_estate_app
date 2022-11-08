namespace RealEstate.WebService
{
    public static class Log
    {
        //private static string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) ? Environment.GetEnvironmentVariable("HOME") : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        private static string Filename = ".gateway";//string.Format("{0}/.gateway", homePath);
        public static void WriteLine(string message)
        {
            try
            {

#if RELEASE
                string file = "log_" + DateTime.UtcNow.ToString("yyyy_MM_dd") + "_gateway.log";
                string filename = Path.Combine(Filename, file);
                if (!Directory.Exists(Filename))
                {
                    Directory.CreateDirectory(Filename);
                }
                string msg = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss") + "\t" + message + Environment.NewLine;
                System.IO.File.AppendAllText(filename, msg);
#endif
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                string filename = Path.Combine(Filename, "errors.log");
                if (!Directory.Exists(Filename))
                {
                    Directory.CreateDirectory(Filename);
                }
                string msg = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss") + "\t" + message + Environment.NewLine;
                System.IO.File.AppendAllText(filename, ex.Message);
                System.IO.File.AppendAllText(filename, ex.StackTrace);
            }
        }

        public static void WriteLine(int count)
        {
            throw new NotImplementedException();
        }
    }
}
