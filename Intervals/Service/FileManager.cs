using Intervals.Service.Interface;
using System.Diagnostics.CodeAnalysis;

namespace Intervals.Service
{
    [ExcludeFromCodeCoverage]
    public class FileManager : IFileManager
    {
        public StreamReader StreamReader(string pathToFile)
        {
            return new StreamReader(pathToFile);
        }
    }
}
