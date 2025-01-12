using System.IO;

namespace DataPersistence.Core
{
    internal sealed class DefaultFilePersistence : IFilePersistence
    {
        private const string FullFileNameFormat = "{0}.{1}";
        private const string ExtensionName = "json";
        
        private readonly string _path;

        public DefaultFilePersistence(string path)
        {
            _path = path;
        }

        public void Save(string fileName, string data) => File.WriteAllText(GetFullPath(fileName), data);

        public string Load(string fileName) =>
            File.Exists(GetFullPath(fileName)) ?
                File.ReadAllText(GetFullPath(fileName)) :
                string.Empty;
        
        private string GetFullPath(string fileName)
        {
            var fullFileName = string.Format(FullFileNameFormat, fileName, ExtensionName);
            return Path.Combine(_path, fullFileName);
        }
    }
}