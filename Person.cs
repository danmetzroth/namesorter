using System;
using System.Collections.Generic;
using System.Linq;

namespace NameSorter
{
    public class Person : IComparable<Person>, IEquatable<Person>
    {
        internal string FirstName;
        internal string LastName;

        internal static Person CreateFromLine(string line)
        {
            var split = line.Split(',');
            if (split.Length != 2)
            {
                throw new Exception(String.Format("Line is in an invalid format: {0}", line));
            }

            return new Person { LastName = split[0].Trim(), FirstName = split[1].Trim() };
        }

        internal string ToFormattedLine()
        {
            return String.Format("{0}, {1}", LastName, FirstName);
        }

        internal static IEnumerable<string> GetFormattedLinesFromPeople(List<Person> people)
        {
            return people.Select(p => p.ToFormattedLine());
        }

        public int CompareTo(Person otherPerson)
        {
            if (String.Compare(LastName, otherPerson.LastName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return String.Compare(FirstName, otherPerson.FirstName, StringComparison.OrdinalIgnoreCase);
            }

            return String.Compare(LastName, otherPerson.LastName, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Person other)
        {
            return String.Compare(LastName, other.LastName, StringComparison.OrdinalIgnoreCase)==0
                && String.Compare(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase)==0;
        }
    }
}