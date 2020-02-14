using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyMaple
{
    public class MainWorker
    {
        static Task mTask;
        static ConcurrentQueue<Task> queueTasks = new ConcurrentQueue<Task>();
        static AutoResetEvent syncEvent = new AutoResetEvent(false);

        static MainWorker()
        {
            mTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (queueTasks.Count == 0)
                        syncEvent.WaitOne();
                    else
                    {
                        Task curTask;
                        if (queueTasks.TryDequeue(out curTask))
                        {
                            curTask.Start();
                            curTask = null;
                        }
                    }
                }
            });
            mTask.ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Util.LogTxt(t.Exception.Message, true);
                }
            }, TaskContinuationOptions.NotOnRanToCompletion);
        }

        public static void QueueTask(Action action, Action callback = null)
        {
            var task = new Task(action);
            AddQueueTask(task, callback);
        }

        private static void AddQueueTask(Task task, Action callback = null)
        {
            task.ContinueWith(t =>
            {
                //其实这里要返回成功与否的状态，但是项目里用不到，不写了CallSuccessFunc
                if (callback != null)
                {
                    callback();
                }
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(t =>
            {
                //其实这里要返回失败与否的状态，但是项目里用不到，不写了CallFailFunc
                if (callback != null)
                {
                    callback();
                }
            }, TaskContinuationOptions.NotOnRanToCompletion);
            queueTasks.Enqueue(task);
            syncEvent.Set();
        }
    }
}
