﻿namespace SampleBatch.Components.Activities.SuspendOrder
{
    using System;
    using System.Threading.Tasks;
    using MassTransit.Courier;
    using MassTransit.Courier.Exceptions;
    using Microsoft.Extensions.Logging;


    public class SuspendOrderActivity :
        IExecuteActivity<SuspendOrderArguments>
    {
        readonly ILogger _logger;

        public SuspendOrderActivity(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SuspendOrderActivity>();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<SuspendOrderArguments> context)
        {
            _logger.LogInformation("Suspending {OrderId}", context.Arguments.OrderId);

            var random = new Random(DateTime.Now.Millisecond);

            if (random.Next(1, 10) == 1)
                throw new RoutingSlipException("Order shipped, cannot suspend");

            await Task.Delay(random.Next(1, 7) * 1000);

            return context.Completed();
        }
    }
}