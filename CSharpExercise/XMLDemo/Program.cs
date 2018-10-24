using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XMLDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateXmlUseLinqAndSaveToFile();
            CreateXmlUseLinqAndSaveToFile(new Student(5).Students);
            var result = from e in XDocument.Load("Student2.xml").Element("Students")
                         .Elements("Student")
                         where int.Parse(e.Attribute("Num").Value) > 1 
                         //where int.Parse(e.Element("Id").Value) > 0
                         where int.Parse(e.Element("Mark").Value) > 400
                         orderby int.Parse(e.Element("Mark").Value) descending
                         select e.Element("Name").Value;

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
        //student1.xml
        static void CreateXmlUseLinqAndSaveToFile()
        {
            var xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Create XML Using Xml.Linq"),
                new XElement("Students", new XComment("comment test"),
                    new XElement("Student", new XAttribute("Id", 1),
                        new XElement("Name", "Mark"),
                        new XElement("Gender", "Male"),
                        new XElement("TotalMarks", "800")),
                    new XElement("Student", new XAttribute("Id", 1),
                        new XElement("Name", "Rosy"),
                        new XElement("Gender", "Female"),
                        new XElement("TotalMarks", "788")),
                    new XElement("Student", new XAttribute("Id", 1),
                        new XElement("Name", "Pam"),
                        new XElement("Gender", "Male"),
                        new XElement("TotalMarks", "922"))));

                        xdoc.Save("student1.xml");
        }
        //student2.xml
        static void CreateXmlUseLinqAndSaveToFile(IEnumerable<object> objs)
        {
            var stus = objs.Cast<Student>();
            var xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("From object Students"),
                new XElement("Students", new XComment("Another Comment"),
                    from s in stus
                    select new XElement("Student", new XAttribute("Num", s.Num),
                                new XElement("Id", s.Id),
                                new XElement("Name", s.Name),
                                new XElement("Gender", s.Gender),
                                new XElement("Mark" , s.Mark)))
                );

            xdoc.Save("student2.xml");
        }
    }
}
