namespace DemoLibrary
{
    public static class Examples
    {
        public static string ExampleLoadTextFile(string path)
        {
            if (path.Length < 10)
            {
                throw new System.IO.FileNotFoundException();
            }

            return "The file is loaded successfully";
        }
    }
}
