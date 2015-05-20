// ----------------------------------------------------------------------------
// <copyright file="WorkerRole.cs" company="SOGETI Spain">
//     Copyright © 2015 SOGETI Spain. All rights reserved.
//     Example of a task scheduler in the cloud by Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace QuartzWorkerRole
{
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Quartz;
    using Quartz.Impl;

    /// <summary>
    /// Represents the worker role.
    /// </summary>
    public class WorkerRole : RoleEntryPoint
    {
        #region Fields

        /// <summary>
        /// The scheduler.
        /// </summary>
        private IScheduler scheduler;

        /// <summary>
        /// The cancellation token source.
        /// </summary>
        private readonly CancellationTokenSource cancellationTokenSource =
            new CancellationTokenSource();

        /// <summary>
        /// The run complete event.
        /// </summary>
        private readonly ManualResetEvent runCompleteEvent =
            new ManualResetEvent(false);

        #endregion Fields

        #region Methods

        /// <summary>
        /// Runs code that is intended to be run for the life of the role instance.
        /// </summary>
        public override void Run()
        {
            Trace.TraceInformation("QuartzWorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        /// <summary>
        /// Runs code that initializes a role instance.
        /// </summary>
        /// <returns><b>true</b> if initialization succeeds; otherwise, <b>false.</b></returns>
        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            this.StartScheduler();
            bool result = base.OnStart();

            Trace.TraceInformation("QuartzWorkerRole has been started");

            return result;
        }

        /// <summary>
        /// Runs code when a role instance is to be stopped.
        /// </summary>
        public override void OnStop()
        {
            Trace.TraceInformation("QuartzWorkerRole is stopping");

            this.scheduler.Shutdown(false);
            this.scheduler = null;

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("QuartzWorkerRole has stopped");
        }

        /// <summary>
        /// Starts the scheduler.
        /// </summary>
        private void StartScheduler()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            this.scheduler = schedulerFactory.GetScheduler();

            IJobDetail jobDetail = JobBuilder.Create<ReportingMaintenanceJob>()
                .WithIdentity("ReportingMaintenance", "ReportingGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ReportingTrigger", "ReportingGroup")
                .ForJob(jobDetail)
                .StartNow()
                //.WithSimpleSchedule(x => x.RepeatForever().WithIntervalInHours(1))
                .WithSimpleSchedule(x => x.RepeatForever().WithIntervalInSeconds(30))
                .Build();

            this.scheduler.ScheduleJob(jobDetail, trigger);
            this.scheduler.Start();
        }

        /// <summary>
        /// Runs code asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The started task.</returns>
        private Task RunAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    //Trace.TraceInformation("Working");
                    Thread.Sleep(1000);
                }
            });
        }

        #endregion Methods
    }
}