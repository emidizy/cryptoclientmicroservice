using Broker.AppCallBacks.Interfaces;
using RabbitMQ.Client.Events;
using Broker.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Broker.AppCallBacks.Services
{
    public class AppCallBacks : IAppCallBacks
    {
        public void ExecuteAction(string recievedPayload, BasicDeliverEventArgs eventProperties)
        {
            var header = eventProperties.BasicProperties.Headers;
            dynamic eventIdByteValue;
            header.TryGetValue("eventId", out eventIdByteValue);
            if (eventIdByteValue == null) return;
            string eventId = Encoding.UTF8.GetString(eventIdByteValue);

            // Make your implementations for different payload actions
            switch (eventId)
            {
                case BrokerEvents.UpdateTransaction:
                    //Query Transactions
                    break;
            }
        }
    }
}
