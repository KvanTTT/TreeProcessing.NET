using NUnit.Framework;
using System.Collections.Generic;

namespace TreesProcessing.NET.Tests
{
    [TestFixture]
    public class ListenerTests
    {
        [Test]
        public void EventListener_Dynamic()
        {
            var listener = new DynamicEventListener();
            List<string> invokeSequence = ListenerUtils.AppendEvents(listener);
            listener.InitializeEvents();
            listener.Walk(SampleTree.Init());
            ListenerUtils.CheckInvokeSequence(invokeSequence, true);
        }
    }
}
