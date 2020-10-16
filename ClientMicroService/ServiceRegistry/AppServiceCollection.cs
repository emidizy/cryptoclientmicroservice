using AppCore.Application.Interfaces;
using AppCore.Application.Services;
using AppCore.Shared.Interfaces;
using AppCore.Shared.Services;
using Broker.AppCallBacks.Interfaces;
using Broker.AppCallBacks.Services;
using Broker.Clients.Interfaces;
using Broker.Clients.Services;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository.Interfaces;
using Persistence.Repository.Services;
using Persistence.UnitOfWork.Interfaces;
using Persistence.UnitOfWork.Services;
using Reciever;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace TransactionMicroService.ServiceRegistry
{
    public static class AppServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<Logger>();
            
            //Register Db services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            //Register Core services
            services.AddTransient<IRestResponse, RestResponse>();
            services.AddTransient<IHttpClient, HttpClient>();
            services.AddTransient<IResponseHandler, ResponseHandler>();
            services.AddTransient<IClientTransactions, ClientTransactions>();

            //Register Broker services
            services.AddScoped<IAppCallBacks, AppCallBacks>();
            services.AddSingleton<IBroadcaster, Broadcaster>();
            services.AddSingleton<IReceiver, Receiver>();
            services.AddHostedService<BrokerDaemon>();

            //Register Background service to send 'update transaction' message
            services.AddTransient<IBackgroundJobSvc, BackgroundJobSvc>();
            RecurringJob.AddOrUpdate<IBackgroundJobSvc>("UpdateTransaction", job => job.CheckForTransactionUpdate(), "*/5 * * * *");

            return services;
        }
    }
}
