using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JvmSharp.UnitTests.Core.Test
{
    public class Runner_Test
    {
        [Theory]
        // relative path from ./bin/Debug/net7.0/
        [InlineData("../../../data/classFile/HelloWorld.class", "Hello, World!\n")]
        [InlineData("../../../data/classFile/_42_plus_42.class", "84\n")]
        public void Test_Run(string classFilePath, string expected)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);

            JvmSharp.Core.Runner.Run(classFilePath);
            
            var actual = ReplaceNewLine(sw.ToString());
            Assert.Equal(expected, actual);
        }

        private static string ReplaceNewLine(string str) =>
            Environment.NewLine != "\n" ? str.Replace(Environment.NewLine, "\n") : str;
    }
}
