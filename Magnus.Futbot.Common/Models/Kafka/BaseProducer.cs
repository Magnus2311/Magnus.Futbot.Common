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

        public void Produce(TValue value)
            => Producer.ProduceAsync(Topic, new Message<TKey, TValue> { Value = value });

    }
}
