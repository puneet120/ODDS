﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using OddsMessaging.Models;
using RabbitMQ.Client.Events;
using OddsMessaging.Hubs;
using System.Threading;


namespace OddsMessaging.Controllers
{

    public class MessagingController : ApiController
    {
        private static IConnection connection;
        private static IModel channel;
       
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Add Odds data into Messaging queue using RabbitMQ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("odd/save")]
        public IHttpActionResult PostOddDetails(OddsModel model
)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "oddsqueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                string message = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                        routingKey: "oddsqueue",
                                        basicProperties: null,
                                        body: body);
             
                return Ok(model);
            }

        }

        /// <summary>
        /// Subscriber listen to new Messages from Rabbit MQ and Broadcast using SignalR
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("odd/get")]
        public HttpResponseMessage GetAllOdds()
        {
            OddsModel data = new OddsModel();     
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "oddsqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
           
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var deserialized = JsonConvert.DeserializeObject<OddsModel>(message);

                data = new OddsModel
                {
                    Description = deserialized.Description,
                    Odd_1 = deserialized.Odd_1,
                    Odd_X = deserialized.Odd_X,
                    Odd_2 = deserialized.Odd_2,
                    Id = deserialized.Id

                };

                OddsHub.BroadcastData(data);

                // Display message
            };
            channel.BasicConsume(queue: "oddsqueue", autoAck: true, consumer: consumer); 
            return Request.CreateResponse(HttpStatusCode.OK, data);
        
        }
     

    }
}

