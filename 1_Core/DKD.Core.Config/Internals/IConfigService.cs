namespace DKD.Core.Config
{
    public interface IConfigService
    {
        string GetConfig(string fileName);
        void SaveConfig(string fileName, string content);
        string GetFilePath(string fileName);
    }
}
