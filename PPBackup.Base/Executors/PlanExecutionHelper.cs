﻿using PPBackup.Base;
using PPBackup.Base.Model;
using PPBackup.Base.SystemOperations;
using System;
using System.Threading.Tasks;

namespace PPBackup.Base.Executors
{
    public class PlanExecutionHelper
    {
        private readonly Application application;
        private readonly ISystemOperations systemOperations;

        public PlanExecutionHelper(Application application, ISystemOperations systemOperations)
        {
            this.application = application;
            this.systemOperations = systemOperations;
        }

        public async Task ExecuteAsync(BackupPlan plan, PlanExecutionEvents events)
        {
            await Task.Run(() =>
            {
                try
                {
                    events.IsRunning(true);

                    var placeholders = new Placeholders();
                    systemOperations.LoadSystemPlaceholders(placeholders);

                    int stepsFinished = 0;
                    events.Progress(0);
                    int planProgress = 0;
                    foreach (var step in plan.Steps)
                    {
                        var stepExecution = application.Services.Get<IStepExecution>(s => s.Type == step.StepType);
                        if (stepExecution != null)
                        {
                            var stepEvents = new StepExecutionEvents();
                            stepEvents.ProgressUpdated += (o, e) => events.Progress(planProgress + e.Progress / plan.Steps.Count);
                            stepEvents.StatusTextUpdated += (o, e) => events.StatusText(e.StatusText);
                            stepExecution.Execute(step, placeholders, stepEvents);
                            stepsFinished++;
                            planProgress = stepsFinished * 100 / plan.Steps.Count;
                            events.Progress(planProgress);
                        }
                        else
                        {
                            events.HasErrors(true, $"Unknown execution step '{step.StepType}'");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    events.HasErrors(true, ex.Message);
                }
                finally
                {
                    events.IsRunning(false);
                }
            });
        }
    }
}
