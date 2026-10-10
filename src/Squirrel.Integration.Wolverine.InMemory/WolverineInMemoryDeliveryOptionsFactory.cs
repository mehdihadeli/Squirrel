using System.Globalization;
using Squirrel.Abstractions.Messages;
using Wolverine;
using MessageHeaders = Squirrel.Core.Messages.MessageHeaders;

namespace Squirrel.Integration.Wolverine.InMemory;

internal static class WolverineInMemoryDeliveryOptionsFactory
{
    internal static DeliveryOptions Build(IMessageEnvelopeBase envelope)
    {
        var options = new DeliveryOptions
        {
            CorrelationId = envelope.Metadata.CorrelationId.ToString(),
            CausationId = envelope.Metadata.CausationId?.ToString(),
        };

        options.WithHeader(MessageHeaders.MessageId, envelope.Metadata.MessageId.ToString());
        options.WithHeader(MessageHeaders.Type, envelope.Metadata.MessageType);
        options.WithHeader(MessageHeaders.Name, envelope.Metadata.Name);
        options.WithHeader(MessageHeaders.CausationId, envelope.Metadata.CausationId?.ToString());
        options.WithHeader(MessageHeaders.CorrelationId, envelope.Metadata.CorrelationId.ToString());
        options.WithHeader(MessageHeaders.Created, envelope.Metadata.Created.ToString(CultureInfo.InvariantCulture));

        foreach (var header in envelope.Metadata.Headers)
        {
            if (header.Value is not null)
            {
                options.WithHeader(header.Key, header.Value.ToString()!);
            }
        }

        return options;
    }
}