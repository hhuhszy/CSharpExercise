namespace Chapter1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }
    }

    public class Salary : IComparable<Salary>
    {
        public Salary(string name , int @base , int bonus)
        {
            Name = name; BaseSalary = @base; Bonus = bonus;
        }

        public int BaseSalary { get; set; }
        public int Bonus { get; set; }
        public string Name { get; set; } = "None";

        public int CompareTo(Salary other)
        {
            return (BaseSalary + Bonus).CompareTo((other.BaseSalary +
                other.Bonus));
        }
    }

    public class SalaryComparer : IComparer<Salary>
    {
        public SalaryComparer()
        {}

        public SalaryComparer(ComparerType ct)
        {
            _type = ct;
        }

        public int Compare(Salary x, Salary y)
        {
            switch (_type)
            {
                case ComparerType.Bonus:
                    return x.Bonus.CompareTo(y.Bonus);
                case ComparerType.BaseSalary:
                    return x.BaseSalary.CompareTo(y.BaseSalary);
                case ComparerType.BonusAndBaseSalary:
                    return x.CompareTo(y);
                default:
                    return 0;
            }
        }

        private ComparerType _type = ComparerType.BonusAndBaseSalary;
    }

    public enum ComparerType
    {
        Bonus,
        BaseSalary,
        BonusAndBaseSalary,
    }

    public class Person : IEquatable<Person>
    {
        public Person(int code)
        {
            IdCode = code;
        }

        public int IdCode { get; private set; }

        public override bool Equals(object obj)
        {
            return IdCode.Equals((obj as Person).IdCode);    
        }

        public override int GetHashCode()
        {
            var prefix = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            return $"{prefix}#{IdCode}".GetHashCode();
        }

        public bool Equals(Person other)
        {
            return IdCode.Equals(other.IdCode);
        }
    }

    [Serializable]
    class Employee : ICloneable
    {
        public Employee()
        {}
        public Employee(string id , int age , Department department)
        {
            IDCode = id; Age = age; Department = department;
        }

        public string IDCode { get; set; } = "None";
        public int Age { get; set; } = -1;
        public Department Department { get; set; } = new Department(new DepInfo
        { Name = "None"});

        #region ICloneable
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
        public override string ToString()
        {
            return $"ID: {IDCode}\nAge: {Age}\nDepart: {Department.Info.Name}" +
                $"\n--------"; 
        }

        public Employee ShallowClone()
        {
            return this.Clone() as Employee;
        }

        public Employee DeepClone()
        {
            using (var mstream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(mstream, this);
                mstream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(mstream) as Employee;
            }
        }
    }

    [Serializable]
    class Department
    {
        public Department(DepInfo info)
        {
            Info = info;
        }

        public DepInfo Info { get;set; }


    }
    [Serializable]
    public class DepInfo
    {
        public string Name { get; set; }
    }
}
