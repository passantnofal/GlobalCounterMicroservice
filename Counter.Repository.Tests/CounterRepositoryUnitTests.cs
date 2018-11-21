using System;
using System.Text;
using Counter.Resources;
using Counter.Test.Infrastructure;
using EventStore.ClientAPI;
using Moq;
using NSubstitute;
using Xunit;

namespace Counter.Repository.Tests
{
    public class CounterRepositoryUnitTests
    {

        private IEventStoreConnection _conn;

        public CounterRepositoryUnitTests()
        {
            //mock EventStoreConnection connection
            _conn = Substitute.For<IEventStoreConnection>();
        }

        [Fact]
        public void TestGetCounter()
        {
            CounterRepositoryTest repo = new CounterRepositoryTest(_conn);
            var result = repo.Get();
            Assert.True(result.Result.Count == 0);
        }

        /// <summary>
        /// TestGetCounterValueOne test the counter at first increment 
        /// </summary>
        [Fact]
        public  void TestGetCounterValueOne()
        {
            // use the counter Repository test to use in memory store not the event store
            CounterRepositoryTest repo = new CounterRepositoryTest(_conn);
            var eventData = new EventData(Guid.NewGuid(), ConstantValues.EventName, false,
                Encoding.UTF8.GetBytes("1"),
                Encoding.UTF8.GetBytes("1"));
            // add the event to in memory
            repo._testList.Add(eventData);
            var result = repo.Get();
            Assert.True(result.Result.Count == 1);
        }
        /// <summary>
        /// TestGetCounterValueTwo test the counter to increment twice
        /// 
        /// </summary>
        [Fact]
        public  void TestGetCounterValueTwo()
        {

            CounterRepositoryTest repo = new CounterRepositoryTest(_conn);
            var eventData = new EventData(Guid.NewGuid(), ConstantValues.EventName, false,
                Encoding.UTF8.GetBytes("1"),
                Encoding.UTF8.GetBytes("1"));
            // increment twice
            repo._testList.Add(eventData);
            repo._testList.Add(eventData);
            var result = repo.Get();
            Assert.True(result.Result.Count == 2);
        }
    }
}