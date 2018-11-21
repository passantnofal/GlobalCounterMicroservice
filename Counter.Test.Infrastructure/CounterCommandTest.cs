using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Counter.Command;
using Counter.Resources;
using EventStore.ClientAPI;

namespace Counter.Test.Infrastructure
{
    public class CounterCommandTest : CommandBase
    {
        /// <summary>
        /// the in memory store
        /// </summary>
        List<EventData> _testList = new List<EventData>();

        public CounterCommandTest(IEventStoreConnection conn) : base(conn)
        {
        }
        /// <summary>
        /// override the GetEvent to get event data to be stored in the execute method
        /// </summary>
        /// <returns></returns>
        protected override async Task<EventData> GetEvent()
        {
            var myEvent = new EventData(Guid.NewGuid(), ConstantValues.EventName, false,
                Encoding.UTF8.GetBytes("1"),
                Encoding.UTF8.GetBytes("1"));

            return myEvent;

        }

        public override async Task<EventData> Execute()
        {
            var @event = await GetEvent();
            _testList.Add(@event);
            return @event;
        }

    }
}
