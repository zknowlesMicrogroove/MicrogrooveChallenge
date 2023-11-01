namespace MicrogrooveChallenge.Data
{
    /// <summary>
    /// A dirty solution borrowed from StackOverflow to allow me to place the SQLite db file in the solution directory.
    /// </summary>
    public static class DatabaseFileLocator
    {
        private static DirectoryInfo TryGetSolutionDirectoryInfo()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

        public static string GetConnectionString()
        {
            return $"Data Source={TryGetSolutionDirectoryInfo()}\\customers.db";
        }
    }
}
