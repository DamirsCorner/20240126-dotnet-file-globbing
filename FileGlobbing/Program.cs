using GlobExpressions;
using Microsoft.Extensions.FileSystemGlobbing;

Console.WriteLine("Using Directory.GetFiles");
ListFiles(GetFilesWithDirectory);

Console.WriteLine();

Console.WriteLine("Using Microsoft.Extensions.FileSystemGlobbing");
ListFiles(GetFilesWithMatcher);

Console.WriteLine();

Console.WriteLine("Using Glob");
ListFiles(GetFilesWithGlob);

IList<string> GetFilesWithDirectory()
{
    return Directory.GetFiles(".", args[0]);
}

IList<string> GetFilesWithMatcher()
{
    var matcher = new Matcher();
    matcher.AddInclude(args[0]);
    return matcher.GetResultsInFullPath(".").ToList();
}

IList<string> GetFilesWithGlob()
{
    return Glob.Files(".", args[0]).ToList();
}

static void ListFiles(Func<IList<string>> fileSupplier)
{
    try
    {
        var files = fileSupplier();

        Console.WriteLine($"Found {files.Count} files:");
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
