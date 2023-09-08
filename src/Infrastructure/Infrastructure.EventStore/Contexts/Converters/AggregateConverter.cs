﻿using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Identities;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.EventStore.Contexts.Converters;

public class AggregateConverter<TAggregate, TId>() :
    ValueConverter<TAggregate?, string>(
        @event => JsonConvert.SerializeObject(@event, typeof(TAggregate), SerializerSettings()),
        jsonString => JsonConvert.DeserializeObject<TAggregate>(jsonString, DeserializerSettings()))
    where TAggregate : IAggregateRoot<TId>
    where TId : IIdentifier, new()
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