using Domain.Abstractions.Messages;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.EventStore.Contexts.Converters;

public class EventConverter()
    : ValueConverter<IDomainEvent?, string>(
        @event => JsonConvert.SerializeObject(@event, typeof(IDomainEvent), SerializerSettings()),
        jsonString => JsonConvert.DeserializeObject<IDomainEvent>(jsonString, DeserializerSettings()))
{
    private static JsonSerializerSettings SerializerSettings()
    {
        JsonSerializerSettings jsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        return jsonSerializerSettings;
    }

    private static JsonSerializerSettings DeserializerSettings()
    {
        JsonSerializerSettings jsonDeserializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ContractResolver = new PrivateSetterContractResolver()
        };

        return jsonDeserializerSettings;
    }
}