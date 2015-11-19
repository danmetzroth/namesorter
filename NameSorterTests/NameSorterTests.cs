using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NameSorter
{
    /// <summary>
    /// Summary description for NameSorterTests
    /// </summary>
    [TestClass]
    public class NameSorterTests
    {
        [TestMethod]
        public void TestMethod()
        {
            var testOutputFile = "TestFiles\\names-sorted.txt";
            DeleteTestOutputIfExists(testOutputFile);

            Assert.IsTrue(!File.Exists("TestFiles\\names-sorted.txt"));
            
            var nameSorter = new NameSorter(new string[]{ "TestFiles\\names.txt" });
            nameSorter.Run();

            Assert.IsTrue(File.Exists("TestFiles\\names-sorted.txt"));

            Assert.AreEqual("BAKER, THEODORE\r\nKENT, MADISON\r\nSMITH, ANDREW\r\nSMITH, FREDRICK\r\n", File.ReadAllText(testOutputFile));

            DeleteTestOutputIfExists(testOutputFile);
        }

        private static void DeleteTestOutputIfExists(string testOutputFile)
        {
            if (File.Exists(testOutputFile))
            {
                File.Delete(testOutputFile);
            }
        }

        [TestMethod]
        public void TestAppendToFileName()
        {
            //Test full path
            Assert.AreEqual("C:\\names-sorted.txt",NameSorter.AppendToFileName("C:\\names.txt", "-sorted"));
            
            //Test relative path
            Assert.AreEqual("names-sorted.txt", NameSorter.AppendToFileName("names.txt", "-sorted"));

            //Test with no filename extension
            Assert.AreEqual("names-sorted", NameSorter.AppendToFileName("names", "-sorted"));
        }
    }
}
