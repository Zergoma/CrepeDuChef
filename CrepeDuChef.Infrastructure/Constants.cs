namespace CrepeDuChef.Infrastructure
{
    internal static class Constants
    {
        private static string DbFileName = "CrepeDuChef.db3";

        public static string GetDbPath()
        {
            string path =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    DbFileName
                    );
            return path;
        }
    }
}
