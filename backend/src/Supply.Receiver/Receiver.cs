using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Core.MessageBroker.Options;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Events.VeiculoEvents;
using Supply.Domain.Events.VeiculoMarcaEvents;
using Supply.Domain.Events.VeiculoModeloEvents;
using Supply.Receiver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Receiver
{
    public class Receiver : BackgroundService
    {
        private readonly IEnumerable<QueueInfo> _queueList;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ILogger<Receiver> _logger;

        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        private IConnection _connection;
        private IModel _channel;

        public Receiver(IOptions<RabbitMqOptions> rabbitMqOptions,
                        IMediatorHandler mediatorHandler, 
                        ILogger<Receiver> logger)
        {
            _mediatorHandler = mediatorHandler;
            _logger = logger;

            _queueList = new List<QueueInfo>()
            {
                new QueueInfo(typeof(VeiculoAddedEvent)),
                new QueueInfo(typeof(VeiculoUpdatedEvent)),
                new QueueInfo(typeof(VeiculoRemovedEvent)),
                new QueueInfo(typeof(VeiculoMarcaAddedEvent)),
                new QueueInfo(typeof(VeiculoMarcaUpdatedEvent)),
                new QueueInfo(typeof(VeiculoMarcaRemovedEvent)),
                new QueueInfo(typeof(VeiculoModeloAddedEvent)),
                new QueueInfo(typeof(VeiculoModeloUpdatedEvent)),
                new QueueInfo(typeof(VeiculoModeloRemovedEvent))
            };

            _hostName = rabbitMqOptions.Value.HostName;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            foreach (var queueInfo in _queueList)
            {
                _channel.QueueDeclare(queue: queueInfo.Name, durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += OnEventReceived;

            foreach (var queueInfo in _queueList)
            {
                _channel.BasicConsume(queueInfo.Name, false, consumer);
            }

            return Task.CompletedTask;
        }

        private async Task OnEventReceived(object sender, BasicDeliverEventArgs @event)
        {
            var content = Encoding.UTF8.GetString(@event.Body.ToArray());

            foreach (var queueInfo in _queueList)
            {
                if (content.Contains(queueInfo.Name))
                {
                    await ProcessEvent(queueInfo.Type, content, @event);
                }
            }

            throw new Exception($"Queue not found for this message type: {content}");
        }

        private async Task ProcessEvent<T>(T instance, string content, BasicDeliverEventArgs @event) where T : Type
        {
            var message = JsonConvert.DeserializeObject(content, instance);

            var eventMessage = (Event)message;

            await _mediatorHandler.PublishEvent(eventMessage);

            _channel.BasicAck(@event.DeliveryTag, false);

            _logger.LogInformation($"Event consumed: {eventMessage.MessageType} - {eventMessage.AggregateId}");
        }
    }
}
