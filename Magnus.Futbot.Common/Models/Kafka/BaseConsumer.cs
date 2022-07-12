using Confluent.Kafka;
using Magnus.Futbot.Common.Models.Kafka.Serialization;
using Microsoft.Extensions.Configuration;

namespace Magnus.Futbot.Common.Models.Kafka
{
    public abstract class BaseConsumer<TKey, TValue>
    {
        public BaseConsumer(IConfiguration configuration)
        {
            Configuration = configuration;

            var config = new ConsumerConfig
            {
                BootstrapServers = Configuration["URLS:KAFKA:SERVER"],
                GroupId = GetType().Name,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            Consumer = new ConsumerBuilder<TKey, TValue>(config)
                .SetValueDeserializer(new Deserializer<TValue>())
                .Build();
            Consumer.Subscribe(Topic);
        }


        public IConsumer<TKey, TValue> Consumer { get; }
        public bool Cancelled { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public abstract string Topic { get; }

        protected IConfiguration Configuration { get; }

        public void Subscribe()
        {
            Consumer.Subscribe(Topic);
        }

        public ConsumeResult<TKey, TValue> Consume(CancellationToken cancellationToken = default)
            => Consumer.Consume(cancellationToken);
    }
}
