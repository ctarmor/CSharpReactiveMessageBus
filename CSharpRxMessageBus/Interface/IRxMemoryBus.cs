using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRxMessageBus.Interface
{
    public interface IRxMemoryBus<T>
    {
        /// <summary>
        /// Get reactive Subscriber
        /// </summary>
        /// <returns></returns>
        IObservable<T> Subscriber { get; }

        /// <summary>
        /// PublishCount message into bus
        /// </summary>
        /// <returns></returns>
        void Publish(T message);


        // ISubject
        void OnCompleted();
        void OnError(Exception error);
        void OnNext(T value);
    }
}
