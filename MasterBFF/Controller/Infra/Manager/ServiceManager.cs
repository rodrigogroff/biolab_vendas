using System;
using Master.Controller.Infra;

namespace Master.Controller.Manager
{
    public class ServiceManager : IDisposable
    {
        public MasterController MyController;

        public ServiceManager(MasterController myController)
        {
            this.MyController = myController;
        }

        public void Dispose()
        {
            MyController.FinishService();
        }
    }
}
