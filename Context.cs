using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyMaple
{
    /// <summary>
    /// 基础上下文
    /// </summary>    
    public class Context
    {
        public CookieContainer MapleCookie { get; set; }
        
        public string NaverCookieStr { get; set; }

        public string MapleEncPwd { get; set; }

        public EasyMapleConfig Config { get; set; }

        /// <summary>
        /// 初始化上下文内容
        /// </summary>
        public Context()
        {
            Config = new EasyMapleConfig();
            NaverCookieStr = Config.DefaultNaverCookie;
        }

        public void ReLogin()
        {
            this.NaverCookieStr = string.Empty;
            this.MapleCookie = null;
            this.MapleEncPwd = string.Empty;
        }



    }

    [Serializable]
    public class LoginData
    {
        private string _guid = string.Empty;
        public string Guid { get => _guid; set => _guid = value; }

        private string _accountTag = string.Empty;
        public string AccountTag { get => _accountTag; set => _accountTag = value; }

        private string _accountCookieStr = string.Empty;
        public string AccountCookieStr { get => _accountCookieStr; set => _accountCookieStr = value; }

        private bool _isDefault = false;
        public bool IsDefault { get => _isDefault; set => _isDefault = value; }
    }

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
                    Util.LogTxt(t.Exception.Message);
                }
            }, TaskContinuationOptions.NotOnRanToCompletion);
        }

        public static void QueueTask(Context ctx, Action action)
        {
            var task = new Task(action);
            AddQueueTask(ctx, task);
        }

        private static void AddQueueTask(Context ctx, Task task)
        {
            task.ContinueWith(t =>
            {

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(t =>
            {

            }, TaskContinuationOptions.NotOnRanToCompletion);
            queueTasks.Enqueue(task);
            syncEvent.Set();
        }
    }
}
