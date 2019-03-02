using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Input q to EXIT program or type any message to send message");
                    string input = "Hello World!";
                    while (input != "q")
                    {
                        channel.QueueDeclare("hello", false, false, false, null);

                        var message = input;
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(String.Empty, "hello", null, body);
                        Console.WriteLine(" [x] Sent {0}", message);
                        input = Console.ReadLine();
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
