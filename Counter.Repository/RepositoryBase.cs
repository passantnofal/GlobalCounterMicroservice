using System;
using System.Threading.Tasks;

namespace Counter.Repository
{
    public abstract class RepositoryBase<T>
    {
        /// <summary>
        /// Get abstract method to get the event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract  Task<T> Get();
    }
}
