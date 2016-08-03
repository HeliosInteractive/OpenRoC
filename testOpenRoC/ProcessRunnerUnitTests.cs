namespace testOpenRoC
{
    using liboroc;

    using System;
    using System.IO;
    using System.Threading;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProcessRunnerUnitTests
    {
        internal static string AssemblyPath
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }

        internal static string TestProcessesPath
        {
            get { return Path.Combine(AssemblyPath, "..", "..", "processes"); }
        }

        internal static string TestProcessWindowedPath
        {
            get { return Path.Combine(TestProcessesPath, "testProcessWindowed.exe"); }
        }

        internal static TimeSpan EpsilonTime
        {
            get { return TimeSpan.FromMilliseconds(100); }
        }

        internal static ProcessOptions ResponsiveWindowedProcessOptions
        {
            get
            {
                return new ProcessOptions
                {
                    Path = TestProcessWindowedPath,
                    WorkingDirectory = TestProcessesPath
                };
            }
        }

        internal static ProcessOptions UnresponsiveWindowedProcessOptions
        {
            get
            {
                return new ProcessOptions
                {
                    Path = TestProcessWindowedPath,
                    WorkingDirectory = TestProcessesPath,
                    CommandLineEnabled = true,
                    CommandLine = "unresponsive"
                };
            }
        }

        [TestMethod]
        public void WindowedPrcoessExistence()
        {
            Assert.IsTrue(Directory.Exists(TestProcessesPath));
            Assert.IsTrue(File.Exists(TestProcessWindowedPath));
        }

        [TestMethod]
        public void StartupStateAssumeCrashIfNotRunning()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = true;

            options.InitialStateEnumValue = ProcessRunner.Status.Invalid;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.InitialStateEnumValue = ProcessRunner.Status.Disabled;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.InitialStateEnumValue = ProcessRunner.Status.Running;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNotNull(runner.Process);
            }
        }

        [TestMethod]
        public void StartupStateDoNotAssumeCrashIfNotRunning()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = false;

            options.InitialStateEnumValue = ProcessRunner.Status.Invalid;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.InitialStateEnumValue = ProcessRunner.Status.Disabled;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.InitialStateEnumValue = ProcessRunner.Status.Running;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNotNull(runner.Process);
            }
        }

        [TestMethod]
        public void StateChangesAssumeCrashIfNotRunning()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = true;

            // running to stopped
            options.InitialStateEnumValue = ProcessRunner.Status.Running;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);

                runner.State = ProcessRunner.Status.Stopped;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
            }

            // running to disabled
            options.InitialStateEnumValue = ProcessRunner.Status.Running;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);

                runner.State = ProcessRunner.Status.Disabled;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);
            }

            // stopped to running
            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                runner.State = ProcessRunner.Status.Running;
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);
            }

            // stopped to disabled
            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                runner.State = ProcessRunner.Status.Disabled;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);
            }

            // disabled to running
            options.InitialStateEnumValue = ProcessRunner.Status.Disabled;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);

                runner.State = ProcessRunner.Status.Running;
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);
            }

            // disabled to stopped
            options.InitialStateEnumValue = ProcessRunner.Status.Disabled;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);

                runner.State = ProcessRunner.Status.Stopped;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
            }
        }

        [TestMethod]
        public void StateChangesDoNotAssumeCrashIfNotRunning()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = false;

            // running to stopped
            options.InitialStateEnumValue = ProcessRunner.Status.Running;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);

                runner.State = ProcessRunner.Status.Stopped;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
            }

            // running to disabled
            options.InitialStateEnumValue = ProcessRunner.Status.Running;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);

                runner.State = ProcessRunner.Status.Disabled;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);
            }

            // stopped to running
            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                runner.State = ProcessRunner.Status.Running;
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);
            }

            // stopped to disabled
            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                runner.State = ProcessRunner.Status.Disabled;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);
            }

            // disabled to running
            options.InitialStateEnumValue = ProcessRunner.Status.Disabled;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);

                runner.State = ProcessRunner.Status.Running;
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);
            }

            // disabled to stopped
            options.InitialStateEnumValue = ProcessRunner.Status.Disabled;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Disabled);

                runner.State = ProcessRunner.Status.Stopped;
                runner.Monitor();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
            }
        }

        [TestMethod]
        public void UnresponsiveProcess()
        {
            ProcessOptions options = UnresponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = true;
            options.CrashedIfUnresponsive = true;
            options.InitialStateEnumValue = ProcessRunner.Status.Running;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Stop();
                Assert.IsNull(runner.Process);
            }

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();
                runner.WaitUntilUnresponsive();

                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.CrashedIfUnresponsive = false;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();

                runner.Monitor();
                Assert.IsNotNull(runner.Process);

                runner.Stop();
                Assert.IsNull(runner.Process);
            }
        }

        [TestMethod]
        public void StopCallback()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = false;
            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();
                ProcessHelper.Shutdown(runner.Process.Id);

                Thread.Sleep(EpsilonTime);
                Assert.IsNotNull(runner.Process);

                runner.Monitor();
                Assert.IsNotNull(runner.Process);
            }
        }

        [TestMethod]
        public void AggressiveCleanup()
        {
            ProcessOptions options = UnresponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = false;
            options.AggressiveCleanupEnabled = true;
            options.InitialStateEnumValue = ProcessRunner.Status.Stopped;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();
                runner.Stop();

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                Assert.IsNull(runner.Process);
            }
        }

        [TestMethod]
        public void GracePeriod()
        {
            var grace_timespan = TimeSpan.FromSeconds(5);
            var grace_timespan_half = TimeSpan.FromSeconds(grace_timespan.TotalSeconds / 2);

            ProcessOptions options = ResponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = true;
            options.GracePeriodEnabled = true;
            options.GracePeriodDuration = (uint)grace_timespan.TotalSeconds;
            options.InitialStateEnumValue = ProcessRunner.Status.Running;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);

                ProcessHelper.Shutdown(runner.Process.Id);
                Thread.Sleep(EpsilonTime);
                runner.Monitor();
                Assert.IsNull(runner.Process);

                Thread.Sleep(grace_timespan_half);
                runner.Monitor();
                Assert.IsNull(runner.Process);

                Thread.Sleep(grace_timespan_half + EpsilonTime);
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
            }
        }

        [TestMethod]
        public void DoubleCheckPeriod()
        {
            var check_timespan = TimeSpan.FromSeconds(2);
            var check_timespan_half = TimeSpan.FromSeconds(check_timespan.TotalSeconds / 2);

            ProcessOptions options = UnresponsiveWindowedProcessOptions;
            options.CrashedIfNotRunning = true;
            options.CrashedIfUnresponsive = true;
            options.DoubleCheckEnabled = true;
            options.DoubleCheckDuration = (uint)check_timespan.TotalSeconds;
            options.InitialStateEnumValue = ProcessRunner.Status.Running;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
                runner.WaitUntilUnresponsive();

                Thread.Sleep(EpsilonTime);
                runner.Monitor();

                Thread.Sleep(check_timespan_half);
                runner.Monitor();
                Assert.IsNotNull(runner.Process);

                Thread.Sleep(check_timespan_half + EpsilonTime);
                runner.Monitor();
                Assert.IsNull(runner.Process);
            }

            options.CommandLine = string.Format("{0} {1}",
                options.CommandLine,
                (int)check_timespan_half.TotalSeconds);

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();

                Thread.Sleep(check_timespan_half);
                runner.Monitor();
                Assert.IsNotNull(runner.Process);

                Thread.Sleep(check_timespan + EpsilonTime);
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
            }
        }

        [TestMethod]
        public void StateRestore()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.RestoreState();
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                runner.State = ProcessRunner.Status.Disabled;
                runner.RestoreState();
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                runner.State = ProcessRunner.Status.Disabled;
                
                // this should be a no-op
                runner.State = ProcessRunner.Status.Disabled;
                runner.RestoreState();
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);
            }
        }

        [TestMethod]
        public void SwappingOptions()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Stop();
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                runner.Start();
                Assert.IsNotNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Running);

                runner.ProcessOptions = options;
                Assert.IsNull(runner.Process);
                Assert.AreEqual(runner.State, ProcessRunner.Status.Stopped);

                Assert.IsFalse(ReferenceEquals(runner.ProcessOptions, options));
            }
        }

        [TestMethod]
        public void DoubleStartingProcess()
        {
            ProcessOptions options = ResponsiveWindowedProcessOptions;

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();
                var attempt1 = runner.Process;

                runner.Start();
                var attempt2 = runner.Process;

                Assert.IsFalse(ReferenceEquals(attempt1, attempt2));
            }
        }
    }
}
