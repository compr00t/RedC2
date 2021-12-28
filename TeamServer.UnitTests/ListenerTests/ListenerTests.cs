using System;
using TeamServer.Models;
using TeamServer.Services;
using Xunit;

namespace TeamServer.UnitTests
{
    public class ListenerTests
    {
        private readonly IListenerService _listeners;

        public ListenerTests(IListenerService listeners)
        {
            _listeners = listeners;
        }

        [Fact]
        public void TestCreateGetListener()
        {
            var origListener = new HttpListener("TestListener", 4444);
            _listeners.AddListener(origListener);

            var newListener = (HttpListener)_listeners.GetListener(origListener.Name);

            Assert.NotNull(newListener);
            Assert.Equal(origListener.Name, newListener.Name);
            Assert.Equal(origListener.BindPort, newListener.BindPort);
        }
    }
}
