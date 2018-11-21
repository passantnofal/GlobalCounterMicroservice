using System;
using System.Text;
using Counter.Resources;
using EventStore.ClientAPI;

namespace Counter.DomainObjects
{
    public class Counter
    {
        public int Count { get; set; }

        /// <summary>
        /// Apply to increment the count by the data stored in the event
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="EventType"></param>
        public void Apply(byte[] Data, string EventType)
        {
            // check if the sam event type
            if (EventType == ConstantValues.EventName)
            {
                // Increment the count 
                Count += int.Parse(Encoding.UTF8.GetString(Data));
            }
        }
    }
}
