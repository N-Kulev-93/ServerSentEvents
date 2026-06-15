using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace ServerSentEvents
{
    internal class SimpleMessage
    {

    }

    internal class SimpeMulticastBroker
    {

    }

    internal class ChannelBasedStreamPipe
    {
        Channel<SimpleMessage> _pipe;
        public ChannelBasedStreamPipe()
        {
            _pipe = Channel.CreateUnbounded<SimpleMessage>();
        }

        public ValueTask? WriteAsync(SimpleMessage message) => _pipe.Writer.WriteAsync(message);

        public async ValueTask<SimpleMessage> ReadAsync()
        {
            await _pipe.Reader.WaitToReadAsync();
            return await _pipe.Reader.ReadAsync();
        }
    }
    
    internal class SharedStreamPipe
    {

    }

    internal class PrivateStreamPipe
    {

    }

    internal class SimpleChannelBasedStream
    {
        private SimpleChannelBasedStream _sharedStream;
        private IEnumerable<SimpleChannelBasedStream> _clientStreams;
        private SimpeMulticastBroker _broker;

        public SimpleChannelBasedStream()
        {
            _sharedStream = new SimpleChannelBasedStream();

            // Hash set wont work, we need threadsafe iterator over client streams because other threads will Add/Remove client streams while iteration is executing.
            // Most naive implementation -> lock the whole collection while iterating. After iteration is over release lock for queue like structure that modify the collection with client streams...
            _clientStreams = new HashSet<SimpleChannelBasedStream>();
        }

        public ValueTask WriteAsync(SimpleMessage msg) => _sharedStream.WriteAsync(msg);

        public void AddClientStream(SimpleChannelBasedStream stream)
        {
            
        } 

        public void RemoveClientStream(SimpleChannelBasedStream stream)
        {

        }
    }
}
