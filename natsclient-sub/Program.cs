using System;
using STAN.Client;
using System.Threading;

namespace natsclient_sub
{
    class Program
    {
        static void Main(string[] args)
        {
            // run docker run --rm --name natstest -p 4222:4222 -p 8222:8222 nats-streaming -store file -dir ./data --max_msgs 0 --max_bytes 0 --cluster_id scarsnatscluster
            EventHandler<StanMsgHandlerArgs> ackHandler = (obj, natsargs) =>
            {
                Console.WriteLine("Message: {0} - {1} - {2}", natsargs.Message.Subject, 
                        System.Text.Encoding.UTF8.GetString(natsargs.Message.Data), natsargs.Message.TimeStamp.ToString());
            };

            var opts = StanSubscriptionOptions.GetDefaultOptions();
            opts.DeliverAllAvailable();
            //opts.StartAt(22);
            var cf = new StanConnectionFactory();
            var c = cf.CreateConnection("mynatscluster", "my-reader");
            var s = c.Subscribe("my.log.error", opts, ackHandler);
        }
    }
}
