using System;
using System.Collections.Generic;
using System.Linq;

namespace XMLDemo
{
    class Student
    {
        private Student()
        {}

        public Student(int num = 1)
        {
            if (num < 1)
            {
                GenStudentsRandom(1);
            }
            GenStudentsRandom(num);
        }

        public int Num { get; private set; } = -1;
        public int Id { get; private set; } = -1;
        public string Name { get; private set; } = "None";
        public string Gender { get; private set; } = "None";
        public int Mark { get; private set; } = -1;
        public List<Student> Students { get; private set; } = new List<Student>();

        private void GenStudentsRandom(int num)
        {
            var count = 0;
            for (int i = 0; i < num; i++)
            {
                count++;
                Students.Add(new Student()
                {
                    Num = count,
                    Id = GenRandomId(),
                    Name = GenRandomName(),
                    Gender = GenRandomGender(),
                    Mark = GenRandomMark(),
                });
            }
        }

        private int GenRandomId()
        {
            return Guid.NewGuid().GetHashCode();
        }

        private string GenRandomName()
        {
            lock (syncLock)
            {
                var length = random.Next(7, 14);
                return new string(Enumerable.Repeat(character, length).Select(i =>
                i[random.Next(character.Length)]).ToArray()); 
            }
        }

        private string GenRandomGender()
        {
            lock (syncLock)
            {
                var r = random.Next(2);
                if (r == 0)
                {
                    return "Male";
                }
                if (r == 1)
                {
                    return "Female";
                }
                return "Unkown"; 
            }
        }

        private int GenRandomMark()
        {
            return random.Next(600, 800);
        }

        private const string character = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
        private static readonly Random random = new Random();//if random is
        //a partial variable , everytime the random.next() will return the same
        private static readonly object syncLock = new object();
    }
}
