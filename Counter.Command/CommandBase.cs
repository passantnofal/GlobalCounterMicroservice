using System;
using System.Threading.Tasks;
using Counter.Resources;
using EventStore.ClientAPI;

namespace Counter.Command
{/// <summary>
/// command base so any commond can implement the commom features
/// </summary>
    public abstract class CommandBase
    {
        private readonly IEventStoreConnection _conn;

        protected CommandBase(IEventStoreConnection conn)
        {
            // set the event store connection
            _conn = conn;
        }
        /// <summary>
        /// each command will implement Get Event 
        /// </summary>
        /// <returns></returns>
        protected abstract Task<EventData> GetEvent();
        /// <summary>
        /// the base execute method that append the event to the event store
        /// </summary>
        /// <returns></returns>
        public virtual async  Task<EventData> Execute()
        {
            var @event = await GetEvent();
            await _conn.AppendToStreamAsync(ConstantValues.StreamName,
                ExpectedVersion.Any, @event);
            return @event;
        }


    }
}
