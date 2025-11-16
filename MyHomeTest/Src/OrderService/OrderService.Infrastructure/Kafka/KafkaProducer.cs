using Confluent.Kafka;

namespace OrderService.Infrastructure.Kafka
{
    /// <summary>
    /// A simple Kafka producer wrapper used for publishing messages to Kafka topics.
    /// </summary>
    public class KafkaProducer
    {
        // Underlying Kafka producer instance.
        private readonly IProducer<string, string> _producer;

        /// <summary>
        /// Initializes a new Kafka producer using the provided bootstrap server address.
        /// </summary>
        /// <param name="bootstrapServers">Kafka server address (e.g., "kafka:9092").</param>
        public KafkaProducer(string bootstrapServers)
        {
            // Producer configuration: defines how the client connects to the Kafka cluster.
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            // Builds a Kafka producer capable of sending string key-value messages.
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        /// <summary>
        /// Publishes a message to the specified Kafka topic asynchronously.
        /// </summary>
        /// <param name="topic">The Kafka topic to publish to.</param>
        /// <param name="message">The message payload.</param>
        public async Task ProduceAsync(string topic, string message)
        {
            // Produce a message with a generated GUID as the key and the provided value.
            await _producer.ProduceAsync(
                topic,
                new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = message
                }
            );
        }
    }
}
