using System.Threading.Channels;

namespace ServerSentEvents
{
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

        public async ValueTask<SimpleMessage?> WaitAndReadAsync()
        {
            var hasNextItem = await _pipe.Reader.WaitToReadAsync();

            if (!hasNextItem) return null;
            var nextItem = await _pipe.Reader.ReadAsync();
            return nextItem;
        }
    }
    
    internal class SharedStreamPipe
    {

    }
    internal class SimpleMessage
    {

    }
    internal class PrivateStreamPipe
    {
        
    }

    //internal class ClientStreamSource
    //internal class ClientStreamBuffer
    internal class ClientStreamPipe
    {
        SimpleChannelBasedStream _baseStream;

        internal ValueTask? WriteAsync(SimpleMessage message) => _baseStream.WriteAsync(message);
    }

    internal class SimpleStream
    {

    }

    internal class SimpleChannelBasedStream
    {
        private ChannelBasedStreamPipe _sharedPipe;
        private SimpeMulticastBroker _broker;
        private IEnumerable<SimpleChannelBasedStream> _clientStreams;

        public SimpleChannelBasedStream()
        {
            _sharedPipe = new ChannelBasedStreamPipe();

            // Hash set wont work, we need threadsafe iterator over client streams because other threads will Add/Remove client streams while iteration is executing.
            // Most naive implementation -> lock the whole collection while iterating. After iteration is over release lock for queue like structure that modify the collection with client streams...
            
            // If event is pushed but untill it reaches broker's iteration a new client has appearead but after the event is pushed is he supposed to receive this event ? (if we take timeline for source of truth he shoudnt...)
            
            _clientStreams = new HashSet<SimpleChannelBasedStream>();
        }

        public ValueTask? WriteAsync(SimpleMessage msg) => _sharedPipe.WriteAsync(msg);

        public void AddClientStream(SimpleChannelBasedStream stream)
        {
            
        } 

        public void RemoveClientStream(SimpleChannelBasedStream stream)
        {

        }
    }
}
