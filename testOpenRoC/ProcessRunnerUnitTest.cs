namespace testOpenRoC
{
    using liboroc;

    using System;
    using System.IO;
    using System.Threading;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProcessRunnerUnitTest
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

        [TestMethod]
        public void PrcoessWindowedExistence()
        {
            Assert.IsTrue(Directory.Exists(TestProcessesPath));
            Assert.IsTrue(File.Exists(TestProcessWindowedPath));
        }

        [TestMethod]
        public void ProcessStartupStateAssumeCrashIfNotRunning()
        {
            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = true,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath
            };

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
        public void ProcessStartupStateDoNotAssumeCrashIfNotRunning()
        {
            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = false,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath
            };

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
        public void PrcoessStateChangesAssumeCrashIfNotRunning()
        {
            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = true,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath
            };

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
        public void PrcoessStateChangesDoNotAssumeCrashIfNotRunning()
        {
            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = false,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath
            };

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
        public void StoppingUnresponsiveProcess()
        {
            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = true,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath,
                InitialStateEnumValue = ProcessRunner.Status.Running,
                CommandLineEnabled = true,
                CommandLine = "true"
            };

            options.CrashedIfUnresponsive = true;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Stop();
                Assert.IsNull(runner.Process);
            }

            options.CrashedIfUnresponsive = true;
            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();

                while (runner.Process.Responding)
                {
                    runner.Process.Refresh();
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }

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
            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = false,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath,
                InitialStateEnumValue = ProcessRunner.Status.Stopped
            };

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();
                ProcessQuitter.Instance.Shutdown(runner.Process.Id);
                // wait so Process API propagates the crash callback
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                Assert.IsNull(runner.Process);

                runner.Monitor();
                Assert.IsNotNull(runner.Process);
            }
        }

        [TestMethod]
        public void AggressiveCleanup()
        {
            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = false,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath,
                InitialStateEnumValue = ProcessRunner.Status.Stopped,
                AggressiveCleanupEnabled = true,
                CommandLine = "true",
                CommandLineEnabled = true
            };

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Start();
                runner.Stop();

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                Assert.IsNull(runner.Process);
            }
        }

        [TestMethod]
        public void GracePeriodTest()
        {
            int grace_period_seconds = 5;
            int grace_period = (int)TimeSpan.FromSeconds(grace_period_seconds).TotalMilliseconds;

            ProcessOptions options = new ProcessOptions
            {
                CrashedIfNotRunning = true,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath,
                InitialStateEnumValue = ProcessRunner.Status.Running,
                GracePeriodEnabled = true,
                GracePeriodDuration = (uint)grace_period_seconds
            };

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);

                ProcessQuitter.Instance.Shutdown(runner.Process.Id);
                runner.Monitor();
                Assert.IsNull(runner.Process);

                Thread.Sleep(grace_period / 2);
                runner.Monitor();
                Assert.IsNull(runner.Process);

                Thread.Sleep(grace_period / 2 + 100);
                runner.Monitor();
                Assert.IsNotNull(runner.Process);
            }
        }

        [TestMethod]
        public void DoubleCheckPeriodTest()
        {
            int grace_period_seconds = 5;
            int grace_period = (int)TimeSpan.FromSeconds(grace_period_seconds).TotalMilliseconds;

            ProcessOptions options = new ProcessOptions
            {
                CrashedIfUnresponsive = true,
                CrashedIfNotRunning = true,
                Path = TestProcessWindowedPath,
                WorkingDirectory = TestProcessesPath,
                InitialStateEnumValue = ProcessRunner.Status.Running,
                DoubleCheckEnabled = true,
                DoubleCheckDuration = (uint)grace_period_seconds,
                CommandLineEnabled = true,
                CommandLine = "true"
            };

            using (ProcessRunner runner = new ProcessRunner(options))
            {
                runner.Monitor();
                Assert.IsNotNull(runner.Process);

                while (runner.Process.Responding)
                {
                    runner.Process.Refresh();
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }

                Thread.Sleep(100);
                runner.Monitor();

                Thread.Sleep(grace_period / 2);
                runner.Monitor();
                Assert.IsNotNull(runner.Process);

                Thread.Sleep(grace_period / 2 + 100);
                runner.Monitor();
                Assert.IsNull(runner.Process);
            }
        }
    }
}
