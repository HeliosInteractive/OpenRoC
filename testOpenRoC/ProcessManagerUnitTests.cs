using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testOpenRoC
{
    using liboroc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProcessManagerUnitTests
    {
        [TestMethod]
        public void AddingRunners()
        {
            using (var manager = new ProcessManager())
            {
                int addedCount = 0;
                bool addedEventCalled = false;
                bool changeEventCalled = false;

                manager.ProcessesChanged += () => { changeEventCalled = true; };
                manager.RunnerAdded += (runner) => { addedEventCalled = true; ++addedCount; };
                manager.Add(ProcessRunnerUnitTest.ResponsiveWindowedProcessOptions);

                Assert.IsTrue(manager.Contains(ProcessRunnerUnitTest.TestProcessWindowedPath));
                Assert.IsTrue(addedEventCalled);
                Assert.IsTrue(changeEventCalled);

                manager.Add(ProcessRunnerUnitTest.ResponsiveWindowedProcessOptions);
                Assert.AreEqual(addedCount, 1);

                Assert.IsFalse(manager.Contains(null));
                Assert.IsFalse(manager.Contains(""));
            }
        }

        [TestMethod]
        public void RemovingRunners()
        {
            using (var manager = new ProcessManager())
            {
                int removedCount = 0;
                bool removedEventCalled = false;
                bool changeEventCalled = false;

                manager.RunnerRemoved += (runner) => { removedEventCalled = true; ++removedCount; };
                manager.Add(ProcessRunnerUnitTest.ResponsiveWindowedProcessOptions);
                manager.ProcessesChanged += () => { changeEventCalled = true; };

                manager.Remove(ProcessRunnerUnitTest.TestProcessWindowedPath);
                Assert.IsTrue(removedEventCalled);
                Assert.IsTrue(changeEventCalled);

                manager.Remove(ProcessRunnerUnitTest.TestProcessWindowedPath);
                Assert.AreEqual(removedCount, 1);
            }
        }

        [TestMethod]
        public void GettingRunners()
        {
            using (var manager = new ProcessManager())
            {
                Assert.IsNull(manager.Get(ProcessRunnerUnitTest.TestProcessWindowedPath));
                manager.Add(ProcessRunnerUnitTest.ResponsiveWindowedProcessOptions);
                Assert.IsNotNull(manager.Get(ProcessRunnerUnitTest.TestProcessWindowedPath));
            }
        }
    }
}
