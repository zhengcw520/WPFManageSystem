namespace MS.Client.LogInfoModule
{
    public class LogInfoModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<VisLogView>();
            containerRegistry.RegisterForNavigation<OpLogView>();
        }
    }
}
