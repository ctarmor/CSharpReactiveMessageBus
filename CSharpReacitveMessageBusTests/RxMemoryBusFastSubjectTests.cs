using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CSharpRxMessageBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpReacitveMessageBusTests
{
    [TestClass]
    public class RxMemoryBusFastSubjectTests
    {
        [TestMethod]
        public void Subscriber_Tests()
        {
            var total = 0;

            _subs.Subscribe(i => total += i);

            _bus.Publish(2);
            _bus.Publish(3);

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void Subscriber_WithSum()
        {
            var total = 0;

            _subs.Sum().Subscribe(x => total = x);

            _bus.Publish(2);
            _bus.Publish(4);

            _bus.Complete<int>();

            Assert.AreEqual(6, total);
        }

        [TestMethod]
        public void Subscriber_WithComplete()
        {
            var total = 0;

            _subs.Subscribe(i => total += i);

            _bus.Publish(2);

            _bus.Complete<int>();

            _bus.Publish(3);

            Assert.AreEqual(2, total);
        }

        [TestMethod]
        public void Subscriber_WithIsSubject()
        {
            Assert.IsNull(_subs as Subject<int>);
        }

        [TestInitialize]
        public void Setup()
        {
            _bus = new RxMemoryBusFast();
            _subs = _bus.Subscriber<int>();
        }

        private RxMemoryBusFast _bus;
        private IObservable<int> _subs;
    }
}