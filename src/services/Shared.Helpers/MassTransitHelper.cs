using Mapster;
using MassTransit;

namespace Shared.Helpers
{
    public static class MassTransitHelper
    {
        public static async Task PublishObj<TObj, TMessage>(
            this IBus bus,
            TObj obj,
            CancellationToken cancellationToken)
        {
            var message = obj.Adapt<TMessage>();

            await bus.Publish(message!, cancellationToken);
        }
    }
}
