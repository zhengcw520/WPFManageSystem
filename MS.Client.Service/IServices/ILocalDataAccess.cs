namespace MS.Client.Service
{
    public interface ILocalDataAccess
    {
        List<string[]> GetLocalFileInfo();
        List<string> GetIcons();
    }
}
