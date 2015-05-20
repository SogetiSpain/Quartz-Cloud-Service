// ----------------------------------------------------------------------------
// <copyright file="ReportingMaintenanceJob.cs" company="SOGETI Spain">
//     Copyright © 2015 SOGETI Spain. All rights reserved.
//     Example of a task scheduler in the cloud by Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace QuartzWorkerRole
{
    using System.Diagnostics;
    using Quartz;

    /// <summary>
    /// Represents the reporting maintenance job.
    /// </summary>
    public class ReportingMaintenanceJob : IJob
    {
        #region Methods

        /// <summary>
        /// Executes this job.
        /// </summary>
        /// <param name="context">The execution context.</param>
        public void Execute(IJobExecutionContext context)
        {
            Trace.TraceInformation("Executing a reporting maintance...!");
            // TODO: Include here the code to perform the reporting maintenance.
        }

        #endregion Methods
    }
}