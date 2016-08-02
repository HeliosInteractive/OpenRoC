namespace testOpenRoC
{
    using liboroc;

    using System;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExecutorServiceUnitTests
    {
        [TestMethod]
        public void WaitForPending()
        {
            bool flag1 = false;
            bool flag2 = false;

            using (ExecutorService service = new ExecutorService())
            {
                service.Accept(() => { Thread.Sleep(TimeSpan.FromSeconds(1)); flag1 = true; });
                service.Accept(() => { Thread.Sleep(TimeSpan.FromSeconds(1)); flag2 = true; });
            }

            Assert.IsTrue(flag1);
            Assert.IsTrue(flag2);
        }

        [TestMethod]
        public void WaitAfterException()
        {
            bool flag1 = false;
            bool flag2 = false;

            using (ExecutorService service = new ExecutorService())
            {
                service.Accept(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    throw new Exception();

                });
                service.Accept(() => { Thread.Sleep(TimeSpan.FromSeconds(1)); flag2 = true; });
            }

            Assert.IsFalse(flag1);
            Assert.IsTrue(flag2);
        }

        [TestMethod]
        public void ExceptionPropagation()
        {
            Exception ex1 = new Exception();
            Exception ex2 = null;

            using (ExecutorService service = new ExecutorService())
            {
                service.ExceptionReceived += (ex) =>
                {
                    ex2 = ex;
                };

                service.Accept(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    throw ex1;

                });
            }

            Assert.IsTrue(ReferenceEquals(ex1, ex2));
        }
    }
}
