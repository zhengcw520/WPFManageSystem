namespace MS.Client.DAL
{
    public interface ILocalDataAccess
    {
        List<string[]> GetLocalFileInfo();
        List<string> GetIcons();
    }
}
