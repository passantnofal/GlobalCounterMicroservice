using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Counter.Resources;
using EventStore.ClientAPI;

namespace Counter.Command
{
    public class CounterCommand : CommandBase
    {
        /// <summary>
        /// CounterCommand constructor that calls the base constructor
        /// </summary>
        /// <param name="conn"></param>
        public CounterCommand(IEventStoreConnection conn) : base(conn)
        {
        }
        /// <summary>
        /// override the GetEvent to get event data to be stored in the execute method
        /// </summary>
        /// <returns></returns>
        protected override async Task<EventData> GetEvent()
        {
            var eventData = new EventData(Guid.NewGuid(), ConstantValues.EventName, false,
                Encoding.UTF8.GetBytes("1"),
                Encoding.UTF8.GetBytes("1"));

            return eventData;

        }
    }
}
