using System.Text;
using System.Threading.Tasks;
using Counter.Test.Infrastructure;
using EventStore.ClientAPI;
using NSubstitute;
using Xunit;


namespace Counter.Command.Tests
{
    public class CounterCommandUnitTest
    {
        private IEventStoreConnection conn;
       public CounterCommandUnitTest()
       {
            //Mock event store connection
             conn = Substitute.For<IEventStoreConnection>();

        }
        /// <summary>
        /// TestExecute Event unit test to test the increment functionality
        /// </summary>
        [Fact]
        public void TestExecuteEvent()
        {
            //user the Counter test class to use in memory data
            CounterCommandTest cmd = new CounterCommandTest(conn);
            Task task = (cmd.Execute())
                .ContinueWith(innerTask =>
                {
                    var @event = innerTask.Result;
                    // the excecute method should add 1 event with value 1
                    Assert.True(int.Parse(Encoding.UTF8.GetString(@event.Data)) == 1);

                });
            task.Wait();
        }
    }
}
