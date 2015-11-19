using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace NameSorter
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void TestCreatePersonFromLine()
        {
            //Basic test
            var person = Person.CreateFromLine("METZROTH, DANIEL");
            Assert.AreEqual("DANIEL", person.FirstName);
            Assert.AreEqual("METZROTH", person.LastName);

            //Test case sensitivity is preserved when creating person
            person = Person.CreateFromLine("Metzroth, Daniel");
            Assert.AreNotEqual("DANIEL", person.FirstName);
            Assert.AreEqual("Daniel", person.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestExceptionThrownOnInvalidLine()
        {
            Person.CreateFromLine("METZROTH");
        }

        [TestMethod]
        public void TestComparer()
        {
            var person1 = Person.CreateFromLine("METZROTH, DANIEL");
            var person2 = Person.CreateFromLine("BOND, JAMES");
            Assert.IsTrue(person1.CompareTo(person2) > 0);

            //Test when last names match
            person1 = Person.CreateFromLine("METZROTH, DANIEL");
            person2 = Person.CreateFromLine("METZROTH, DAVE");
            Assert.IsTrue(person1.CompareTo(person2)<0);

            //Test when last names match but have different case
            person1 = Person.CreateFromLine("METZROTH, DANIEL");
            person2 = Person.CreateFromLine("Metzroth, Daniel");
            Assert.IsTrue(person1.CompareTo(person2) == 0);
        }

        [TestMethod]
        public void TestEquals()
        {
            var person1 = new Person() {FirstName = "DANIEL", LastName = "METZROTH"};
            var person2 = new Person() {FirstName = "DANIEL", LastName = "METZROTH"};
            Assert.IsTrue(person1.Equals(person2));

            person2.LastName = "BOND";
            Assert.IsFalse(person1.Equals(person2));

            person2.FirstName = "JAMES";
            person2.LastName = "METZROTH";
            Assert.IsFalse(person1.Equals(person2));
        }

        [TestMethod]
        public void TestSort()
        {
            List<Person> people = new List<Person>(){
                new Person(){FirstName = "DAVE",LastName = "METZROTH"},
                new Person(){FirstName = "JAMES",LastName = "BOND"},
                new Person(){FirstName = "AUSTIN",LastName = "POWERS"},
                new Person(){FirstName = "DANIEL",LastName = "METZROTH"}
            };
            
            people.Sort();

            Assert.AreEqual("JAMES", people[0].FirstName);
            Assert.AreEqual("DANIEL", people[1].FirstName);
            Assert.AreEqual("DAVE", people[2].FirstName);
            Assert.AreEqual("AUSTIN", people[3].FirstName);
        }


        [TestMethod]
        public void TestLineFormattingIsStandardised()
        {
            //Test standaring formatting remains
            var input = "Metzroth, Daniel";
            Assert.AreEqual(input,FromLineAndToLine(input));
            
            //Test that non standard whitespace is corrected
            Assert.AreEqual("Metzroth, DANIEL", FromLineAndToLine(" Metzroth,DANIEL"));
        }

        [TestMethod]
        public void GetFormattedLinesFromPeople()
        {
            
            List<Person> people = new List<Person>(){
                new Person(){FirstName = "DANIEL",LastName = "METZROTH"},
                new Person(){FirstName = "JAMES",LastName = "BOND"},
                new Person(){FirstName = "AUSTIN",LastName = "POWERS"}
            };

            var expectedLines = new List<string>() {"METZROTH, DANIEL","BOND, JAMES","POWERS, AUSTIN"};
            var formattedLines = Person.GetFormattedLinesFromPeople(people).ToList();

            //This method does not sort
            CollectionAssert.AreEqual(expectedLines,formattedLines);

        }

        //Create a person object from a line and convert back to a line
        private string FromLineAndToLine(string inputLine)
        {
            var person = Person.CreateFromLine(inputLine);
            return person.ToFormattedLine();
        }
    }
}
