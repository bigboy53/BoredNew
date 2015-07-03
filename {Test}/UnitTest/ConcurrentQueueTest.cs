using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ConcurrentQueueTest
    {
        ConcurrentQueue<Person> persons=new ConcurrentQueue<Person>();

        [TestMethod]
        public void Test()
        {
            int count = 1000;
            for (var i = 0; i < count; i++)
            {
                persons.Enqueue(new Person() {Age = i, Name = "Name" + i});
            }
            var p=new Person();
            while (!persons.IsEmpty)
            {
                if (persons.TryDequeue(out p))
                    Console.WriteLine("年龄:{0},姓名:{1}", p.Age,p.Name);
            }
            Console.ReadKey();
        }

    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
