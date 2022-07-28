using Confluent.Kafka;
using Magnus.Futbot.Common.Models.Kafka.Serialization;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Magnus.Futbot.Common.Models.Kafka
{
    public abstract class BaseProducer<TKey, TValue>
    {
        public BaseProducer(IConfiguration configuration)
        {
            Configuration = configuration;
            var config = new ProducerConfig
            {
                BootstrapServers = Configuration["URLS:KAFKA:SERVER"],
                ClientId = Dns.GetHostName(),
            };

            Producer = new ProducerBuilder<TKey, TValue>(config)
                .SetValueSerializer(new Serializer<TValue>())
                .Build();
        }

        public IProducer<TKey, TValue> Producer { get; }

        public abstract string Topic { get; }

        protected IConfiguration Configuration { get; }

        public Task Produce(TValue value)
            => Producer.ProduceAsync(Topic, new Message<TKey, TValue> { Value = value });

        public Task Produce(TValue value, CancellationToken cancellationToken)
            => Producer.ProduceAsync(Topic, new Message<TKey, TValue> { Value = value }, cancellationToken);

        public Task Produce(TKey key, TValue value, CancellationToken cancellationToken)
            => Producer.ProduceAsync(Topic, new Message<TKey, TValue> { Key = key, Value = value }, cancellationToken);
    }
}
