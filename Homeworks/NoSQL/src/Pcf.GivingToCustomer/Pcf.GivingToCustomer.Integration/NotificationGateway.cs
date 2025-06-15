using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using Pcf.GivingToCustomer.Core.Abstractions.Gateways;

namespace Pcf.GivingToCustomer.Integration
{
    public class NotificationGateway
        : INotificationGateway
    {
        public Task SendNotificationToPartnerAsync(ObjectId partnerId, string message)
        {
            //Код, который вызывает сервис отправки уведомлений партнеру
            
            return Task.CompletedTask;
        }
    }
}