using System;
using System.Threading.Tasks;
using Counter.Resources;
using EventStore.ClientAPI;

namespace Counter.Repository
{
    public class CounterRepository : RepositoryBase<DomainObjects.Counter>
    {
        private IEventStoreConnection _conn;

        public CounterRepository(IEventStoreConnection conn)
        {
            _conn = conn;
        }
        /// <summary>
        /// Get read all the events stored in the stream
        /// then apply the counter to incerement all
        /// </summary>
        /// <returns>Counter</returns>
        public override Task<DomainObjects.Counter> Get()
        {
            var counter = new DomainObjects.Counter();
            var readEvents =  _conn.ReadStreamEventsForwardAsync(ConstantValues.StreamName, 0, 10, true).Result;

            if (readEvents != null)
            {
                // iterate on the stored events to increment 
                foreach (var evt in readEvents.Events)
                    counter.Apply(evt.Event.Data,evt.Event.EventType);
                
            }
            return Task.FromResult<DomainObjects.Counter>(counter); ;
        }

      
    }
}
