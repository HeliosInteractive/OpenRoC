namespace testOpenRoC
{
    using liboroc;

    using System.Linq;

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
                manager.Add(ProcessRunnerUnitTests.ResponsiveWindowedProcessOptions);

                Assert.IsTrue(manager.Contains(ProcessRunnerUnitTests.TestProcessWindowedPath));
                Assert.IsTrue(manager.Runners.Contains(manager.Get(ProcessRunnerUnitTests.TestProcessWindowedPath)));
                Assert.IsTrue(manager.Options.Select(opt => opt.Path).Contains(ProcessRunnerUnitTests.TestProcessWindowedPath));
                Assert.IsTrue(addedEventCalled);
                Assert.IsTrue(changeEventCalled);

                manager.Add(ProcessRunnerUnitTests.ResponsiveWindowedProcessOptions);
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
                manager.Add(ProcessRunnerUnitTests.ResponsiveWindowedProcessOptions);
                manager.ProcessesChanged += () => { changeEventCalled = true; };

                manager.Remove(ProcessRunnerUnitTests.TestProcessWindowedPath);
                Assert.IsTrue(removedEventCalled);
                Assert.IsTrue(changeEventCalled);

                manager.Remove(ProcessRunnerUnitTests.TestProcessWindowedPath);
                Assert.AreEqual(removedCount, 1);
            }
        }

        [TestMethod]
        public void DisposingRemovedRunners()
        {
            using (var manager = new ProcessManager())
            {
                manager.Add(ProcessRunnerUnitTests.ResponsiveWindowedProcessOptions);
                var runner = manager.Get(ProcessRunnerUnitTests.TestProcessWindowedPath);
                manager.Remove(ProcessRunnerUnitTests.TestProcessWindowedPath);

                Assert.IsTrue(runner.IsDisposed);
            }
        }

        [TestMethod]
        public void GettingRunners()
        {
            using (var manager = new ProcessManager())
            {
                Assert.IsNull(manager.Get(ProcessRunnerUnitTests.TestProcessWindowedPath));
                manager.Add(ProcessRunnerUnitTests.ResponsiveWindowedProcessOptions);
                Assert.IsNotNull(manager.Get(ProcessRunnerUnitTests.TestProcessWindowedPath));
            }
        }
    }
}
