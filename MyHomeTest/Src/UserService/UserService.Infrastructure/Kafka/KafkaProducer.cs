using Confluent.Kafka;

namespace UserService.Infrastructure.Kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
        }
    }
}
