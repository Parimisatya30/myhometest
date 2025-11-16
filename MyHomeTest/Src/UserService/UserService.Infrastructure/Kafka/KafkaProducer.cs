using Confluent.Kafka;

namespace UserService.Infrastructure.Kafka
{
    /// <summary>
    /// Kafka producer wrapper for sending messages to Kafka topics.
    /// Used by the UserService to publish events such as "UserCreated".
    /// </summary>
    public class KafkaProducer
    {
        /// <summary>
        /// Underlying Confluent Kafka producer instance.
        /// Thread-safe and reused for all messages in the service.
        /// </summary>
        private readonly IProducer<string, string> _producer;

        /// <summary>
        /// Constructor: initializes the Kafka producer with the provided bootstrap server.
        /// </summary>
        /// <param name="bootstrapServers">Kafka server endpoint (e.g., "kafka:9092").</param>
        public KafkaProducer(string bootstrapServers)
        {
            // Producer configuration: specifies the Kafka cluster to connect to.
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            // Build a producer for string key/value messages.
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        /// <summary>
        /// Sends a message asynchronously to the specified Kafka topic.
        /// </summary>
        /// <param name="topic">The target Kafka topic.</param>
        /// <param name="message">Message content as a serialized string.</param>
        public async Task ProduceAsync(string topic, string message)
        {
            // Generate a unique key for the message to allow Kafka partitioning.
            var kafkaMessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            };

            // Send the message asynchronously to Kafka.
            await _producer.ProduceAsync(topic, kafkaMessage);
        }
    }
}
