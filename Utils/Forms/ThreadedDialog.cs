using System;
using System.ComponentModel;
using System.Windows.Forms;

using Utils.Extensions;

namespace Utils.Forms
{
    public partial class ThreadedDialog : Form
    {
        public BackgroundWorker Worker { get; set; }
        public Action<BackgroundWorker, DoWorkEventArgs> ExecFunc { get; set; }
        public Action<ProgressChangedEventArgs> UpdateFunc { get; set; }
        public Action<AsyncCompletedEventArgs> CompleteFunc { get; set; }

        public ThreadedDialog() { }
        public ThreadedDialog(BackgroundWorker wrkr) => initWorker(wrkr);
        public ThreadedDialog(BackgroundWorker wrkr, Action<BackgroundWorker, DoWorkEventArgs> execFunc, Action<AsyncCompletedEventArgs> completeFunc = null, Action<ProgressChangedEventArgs> updateFunc = null) : this(wrkr)
        {
            ExecFunc = !execFunc.IsNull() ? execFunc : throw new ArgumentException("Execute function cannot be null", "execFunc");
            UpdateFunc = !updateFunc.IsNull() ? updateFunc : null;
            CompleteFunc = !completeFunc.IsNull() ? completeFunc : null;
        }

        private void initWorker(BackgroundWorker wrkr)
        {
            Worker = wrkr;
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
        }

        public void Execute()
        {
            if (Worker != null)
            {
                if (ExecFunc != null) Worker.DoWork += (sender, e) => { ExecFunc(Worker, e); };
                else throw new Exception("No execute function for threading defined.");
                if (CompleteFunc != null) Worker.RunWorkerCompleted += (sender, e) => { CompleteFunc(e); };
                if (UpdateFunc != null) Worker.ProgressChanged += (sender, e) => { UpdateFunc(e); };
                Worker.RunWorkerAsync();
            }
            else throw new Exception("No BackgroundWorker for thread defined.");
        }

        public new void Dispose()
        {
            while (Worker.IsBusy) Worker.CancelAsync();
            Worker = null; ExecFunc = null;
            CompleteFunc = null; UpdateFunc = null;
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
