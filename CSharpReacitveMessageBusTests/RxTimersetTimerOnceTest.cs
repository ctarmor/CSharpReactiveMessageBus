using System;
using System.Threading;
using CSharpRxMessageBus.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpReacitveMessageBusTests
{
    [TestClass]
    public class RxTimersetTimerOnceTest
    {
        [TestMethod]
        public void RxTimer_SetTimerOnce_Test()
        {
            var times = 0;
            var ev = new ManualResetEvent(false);

            new RxTimer()
                .SetTimerOnce("t", TimeSpan.FromMilliseconds(500), () => times++);

            ev.WaitOne(2000);

            Assert.AreEqual(1, times);
        }
    }
}