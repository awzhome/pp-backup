﻿using PPBackup.Base.Config;
using PPBackup.Base.Plans;
using PPBackup.Base.Service;
using PPBackup.Base.Steps;
using PPBackup.Base.SystemOperations;
using System.Collections.Generic;

namespace PPBackup.Base
{
    static class BaseServices
    {
        public static void Bind(IAppServiceBinder binder)
        {
            ApplicationEvents applicationEvents = new();
            List<ExecutableBackupPlan> executablePlans = new();

            binder
                .Bind(applicationEvents)
                .Bind<IApplicationEvents>(applicationEvents)
                .Bind<IStreamReaderProvider, ScriptFileConfigurationProvider>()
                .Bind<ConfigurationManager>()
                .Bind<ScriptConfigurationReader>()
                .Bind(SystemOperationsFactory.CreateSystemOperations)
                .Bind<IStepExecution, SyncStepExecution>()
                .Bind<PlanExecutionHelper>()
                .Bind<IPlanExecutionCreator, ManualPlanExecution.Creator>()
                .Bind(executablePlans)
                .Bind<IEnumerable<ExecutableBackupPlan>>(executablePlans);
        }
    }
}
