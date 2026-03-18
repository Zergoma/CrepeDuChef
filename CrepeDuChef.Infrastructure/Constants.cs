namespace CrepeDuChef.Infrastructure
{
    internal static class Constants
    {
        private static string DbFileName = "CrepeDuChef.db3";

        public static string GetDbPath()
        {
            string path =
                Path.Combine(
#if WINDOWS
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
#else
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
#endif
                    DbFileName
                    );
            return path;
        }
    }
}
