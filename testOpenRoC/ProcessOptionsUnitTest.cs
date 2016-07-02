namespace testOpenRoC
{
    using liboroc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProcessOptionsUnitTest
    {
        [TestMethod]
        public void EnvironmentVariableValidFormat()
        {
            ProcessOptions options = new ProcessOptions();

            const string var1_name = "var1";
            const string var2_name = "var2";
            const string var1_value = "val1";
            const string var2_value = "val2";
            string variables = string.Format("{0}={1};{2}={3}",
                var1_name, var1_value, var2_name, var2_value);

            options.EnvironmentVariables = variables;

            Assert.AreEqual(options.EnvironmentVariablesDictionary.Count, 2);
            Assert.AreEqual(options.EnvironmentVariablesDictionary[var1_name], var1_value);
            Assert.AreEqual(options.EnvironmentVariablesDictionary[var2_name], var2_value);
        }

        [TestMethod]
        public void EnvironmentVariableFromDictionary()
        {
            ProcessOptions options = new ProcessOptions();

            const string var1_name = "var1";
            const string var2_name = "var2";
            const string var1_value = "val1";
            const string var2_value = "val2";
            string variables = string.Format("{0}={1};{2}={3}",
                var1_name, var1_value, var2_name, var2_value);

            options.EnvironmentVariablesDictionary.Add(var1_name, var1_value);
            options.EnvironmentVariablesDictionary.Add(var2_name, var2_value);

            Assert.AreEqual(options.EnvironmentVariables, variables);
        }
    }
}
