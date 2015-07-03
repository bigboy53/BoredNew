using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var pageData = new PageData
            //{
            //    PageIndex = 1,
            //    PageSize = 1
            //};
            //using (var db = new Entities())
            //{
            //    var query = from m in db.ManageUsers
            //                join r in db.Roles on m.RID equals r.ID
            //                select new
            //                {
            //                    m.UName,
            //                    m.AuthCode,
            //                    m.RID,
            //                    m.Email,
            //                    m.RegTime,
            //                    m.RelName,
            //                    r.RoleName
            //                };
            //    var q1 = query.OrderByDescending(t => t.RegTime).Skip(1).Take(1).Future();
            //    var q2 = query.FutureCount();
            //    pageData.Data = q1.ToList();
            //    pageData.DataCount = q2.Value;
            //}
            new B();
        }
    }

    class A
    {
        public A()
        {
            PrintFields();
        }
        public virtual void PrintFields() { }
    }
    class B : A
    {
        int x = 1;
        int y;
        public B()
        {
            y = -1;
        }
        public override void PrintFields()
        {
            Console.WriteLine("x={0},y={1}", x, y);
        }
    }
}
