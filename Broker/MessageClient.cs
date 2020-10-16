using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Binders;
using Broker.AppCallBacks.Interfaces;
using Broker.Clients.Interfaces;

namespace Reciever
{
    public class MessageClient
    {
        private IModel _clientChannel;
        private readonly Logger _logger;
        private IConnection _connection;
        private readonly IServiceProvider _brokerSvcProvider;
        private readonly IReceiver _eventReciever;
        private readonly IBroadcaster _eventPublisher;
        private readonly BrokerConfig _brokerConfig;

        public MessageClient(ILogger<BrokerDaemon> loggerFactory, 
            IServiceProvider serviceProvider,
            IOptions<BrokerConfig> brokerConfig,
            IBroadcaster eventPublisher,
            IReceiver eventReciever,
            Logger logger)
        {
            _logger = logger;
            _brokerSvcProvider = serviceProvider;
            _brokerConfig = brokerConfig.Value;
            _eventPublisher = eventPublisher;
            _eventReciever = eventReciever;

            var connection = CreateConnectionToRabbitMQ();
            RegisterEventPublisher(connection);
            RegisterEventReceiver(connection);
        }

       
        private IConnection CreateConnectionToRabbitMQ()
        {
            var server = _brokerConfig.Server;
            var factory = new ConnectionFactory();
            if (server.isLocal)
            {
                factory = new ConnectionFactory { HostName = server.IP };
            }
            else
            {
                factory = new ConnectionFactory { HostName = server.IP, UserName = server.Username, Password = server.Password };
            }
            // create connection to Rabbitmq server
            var connection = factory.CreateConnection();

            return connection;
        }

        private void RegisterEventReceiver(IConnection connection)
        {
            _eventReciever.InitRabbitMQEventReciever(connection);
        }

        private void RegisterEventPublisher(IConnection connection)
        {
            _eventPublisher.InitRabbitMQEventPublisher(connection);
        }

    }
}
