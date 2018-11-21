using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Counter.Repository;
using Counter.Resources;
using EventStore.ClientAPI;

namespace Counter.Test.Infrastructure

{
    public class CounterRepositoryTest: RepositoryBase<DomainObjects.Counter>
    {
        /// <summary>
    /// event store
    /// </summary>
        private IEventStoreConnection _conn;
        /// <summary>
        /// in memory store
        /// </summary>
        public List<EventData> _testList = new List<EventData>();

        public CounterRepositoryTest(IEventStoreConnection conn)
        {
            // initialize the event store connection
            _conn = conn;
        }
        /// <summary>
        /// Get get all the events stored then apply the counter to incerement all
        /// </summary>
        /// <returns>Counter</returns>
        public override Task<DomainObjects.Counter> Get()
        {
            var counter = new DomainObjects.Counter();
            //get events from in memory store
            var readEvents = _testList;
            if (readEvents != null)
            {
                foreach (var evt in readEvents)
                    counter.Apply(evt.Data,evt.Type);
                
            }
            return Task.FromResult(counter);
        }
    }
}
