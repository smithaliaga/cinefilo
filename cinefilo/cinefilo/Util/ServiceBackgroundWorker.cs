namespace cinefilo.Util
{
    using System;
    using System.ComponentModel;
    using Xamarin.Forms;

    public class ServiceBackgroundWorker
    {

        private static ServiceBackgroundWorker instance = null;
        private bool EnableSendException = true;

        public static ServiceBackgroundWorker GetInstance()
        {
            if (instance == null)
            {
                instance = new ServiceBackgroundWorker();
            }
            return instance;
        }

        public void SetEnableSendException(bool value)
        {
            this.EnableSendException = value;
        }

        public void SendExceptionWorker(string error)
        {
            try
            {
                if (EnableSendException)
                {
                    var HandleExceptionWorker = new BackgroundWorker()
                    {
                        WorkerReportsProgress = false,
                        WorkerSupportsCancellation = false
                    };

                    HandleExceptionWorker.DoWork += (obj, e) => SendException(error);
                    HandleExceptionWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void SendException(string error)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
