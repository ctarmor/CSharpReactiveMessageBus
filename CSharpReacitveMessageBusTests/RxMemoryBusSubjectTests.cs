using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CSharpRxMessageBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpReacitveMessageBusTests
{
    [TestClass]
    public class RxMemoryBusSubjectTests
    {
        [TestMethod]
        public void Subscriber_Tests()
        {
            var total = 0;

            _t.Subscribe(i => total += i);

            _b.Publish(2);
            _b.Publish(3);

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void Subscriber_WithSum()
        {
            var total = 0;

            //_t.Subscribe(i => total += i);

            _t.Sum().Subscribe(x => total = x);

            _b.Publish(2);
            _b.Publish(4);

            _b.Complete<int>();
            
            Assert.AreEqual(6, total);
        }

        [TestMethod]
        public void Subscriber_WithComplete()
        {
            var total = 0;

            _t.Subscribe(i => total += i);

            _b.Publish(2);

            _b.Complete<int>();

            _b.Publish(3);

            Assert.AreEqual(2, total);
        }



        [TestMethod]
        public void Subscriber_WithIsSubject()
        {
            Assert.IsNotNull(_t as Subject<int>);
        }

        [TestInitialize]
        public void Setup()
        {
            _b = new RxMemoryBus();
            _t = _b.Subscriber<int>();
        }

        private RxMemoryBus _b;
        private IObservable<int> _t;
    }
}
