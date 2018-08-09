using System;
using STAN.Client;

namespace natsclient_pub
{
    class Program
    {
        static void Main(string[] args)
        {
            // run docker run --rm --name natstest -p 4222:4222 -p 8222:8222 nats-streaming -store file -dir ./data --max_msgs 0 --max_bytes 0 --cluster_id scarsnatscluster
            var cf = new StanConnectionFactory();
            var c = cf.CreateConnection("mynatscluster", "my-publish");

            // when the server responds with an acknowledgement, this
            // handler will be invoked.
            EventHandler<StanAckHandlerArgs> ackHandler = (obj, natsargs) =>
            {
                if (!string.IsNullOrEmpty(natsargs.Error))
                {
                    Console.WriteLine("Published Msg {0} failed: {1}",
                        natsargs.GUID, natsargs.Error);
                }

                // handle success - correlate the send with the guid..
                //Console.WriteLine("Published msg {0} was stored on the server.");
            };

            int i = 0;
            // returns immediately
            while (true) {
                string guid = c.Publish("scars.log.error", System.Text.Encoding.UTF8.GetBytes(i.ToString() + " - This error sucks!"), ackHandler);
                Console.WriteLine("{1} - Published msg {0} was stored on the server.", guid, i.ToString());
                i++;
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 5));
            }
        }
    }
}
