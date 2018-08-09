# nats-streaming-pub-sub-console
Here I am using NATS.io streaming in a pub/sub setup with small console apps just to prove the point. The publish one I ran in a VSCode window in debug mode. The other I built and ran 'dotnet path-to-dll' to run it and make it listen for messages. Remember NATS streaming has to subscribe to the specific subject, no wildcards as of yet with version 1.0.0. Hopefully soon!

I am using the STAN.Client https://github.com/nats-io/csharp-nats-streaming implementation and using the official Docker image for nats-streaming.

## Docker setup for NATS Streaming
- "docker run --rm --name natstest -p 4222:4222 -p 8222:8222 nats-streaming -store file -dir ./data --max_msgs 0 --max_bytes 0 --cluster_id mynatscluster -m 8222"

## Subscription Console Application

This is just a client that connects and subscribes indefinitely. There is no timer or loop and it simply splits out the message to the console for now to prove the subscription. You would really do something useful with this. :) 

- run with dotnet bin\Debug\netcoreapp2.0\natsclient-sub.dll

## Publishing Console Application

This is fairly simple. You connect to the docker setup with the proper name and then get going every xx seconds based on the sleep parameter. This is only meant to test the publishing. You can publish from anything.

## Reading Management and Metrics

Go to http://localhost:8222/streaming/clientsz and http://localhost:8222/streaming/serverz or http://localhost:8222/streaming to see all the metrics and management type of information.