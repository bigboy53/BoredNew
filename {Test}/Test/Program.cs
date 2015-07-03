using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Linq;

namespace Test
{
    class Program
    {
        static ConcurrentQueue<Person> persons=new ConcurrentQueue<Person>();
        private int i = 10000;
        static void Main(string[] args)
        {
            QueueTest();
            int ss = 1;
            var p = new Person();
            while (!persons.IsEmpty)
            {
                if (persons.TryDequeue(out p))
                {
                    Console.WriteLine("年龄:{0},姓名:{1}", p.Age, p.Name);
                    Interlocked.Add(ref ss, 1);
                }
            }
            Console.WriteLine("完了");
            Console.WriteLine(ss);
            Console.ReadKey();
        }

        private static void QueueTest()
        {
            Action action = () =>
            {
                for (int j = 0; j < 5000; j++)
                {
                    persons.Enqueue(new Person(){Age = j,Name = j.ToString()});
                }
            };
            Parallel.Invoke(action,action,action);
        }

        #region 并发集合处理阻塞和限制

        public static void AllTake()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                using (Task task1 = Task.Factory.StartNew(() =>
                {
                    bc.Add(1);
                    bc.Add(2);
                    bc.Add(3);
                    bc.CompleteAdding();
                }))
                {
                    using (Task task2 = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            while (true)
                            {
                                Console.WriteLine(bc.Take());
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Is Over");
                        }

                    }))
                    {
                        Task.WaitAll(task1, task2);
                    }
                }

            }
        }

        public static void TryBolckingCollection()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                int numbers = 10000;
                for (int i = 0; i < numbers; i++)
                {
                    bc.Add(i);
                }
                bc.CompleteAdding();
                int outSum = 0;
                Action action = () =>
                {
                    int localItem = 0;
                    int localSum = 0;
                    while (bc.TryTake(out localItem))
                    {
                        localSum += localItem;
                    }
                    var result = Interlocked.Add(ref outSum, localSum);
                };
                Parallel.Invoke(action, action, action);
                Console.WriteLine("Sum[0..{0}) = {1}, should be {2}", numbers, outSum, ((numbers * (numbers - 1)) / 2));
                Console.WriteLine("bc.IsCompleted = {0} (should be true)", bc.IsCompleted);
            }
        }
        public static void FromToAnyDemo()
        {
            var bc = new BlockingCollection<int>[2];
            bc[0] = new BlockingCollection<int>(5);
            bc[1] = new BlockingCollection<int>(5);

            int numberFailures = 0;
            for (int i = 0; i < 10; i++)
            {
                if (BlockingCollection<int>.TryAddToAny(bc, i) == -1) numberFailures++;
            }
            Console.WriteLine("TryAddToAny: {0} failures (should be 0)", numberFailures);

            int numberItem = 0;
            int item;
            while (BlockingCollection<int>.TryTakeFromAny(bc, out item) != -1)
            {
                numberItem++;
            }
            Console.WriteLine("TryTakeFromAny: 一共取出了{0}个 ", numberItem);
        }

        public static void TTest()
        {
            using (var bc = new BlockingCollection<User>())
            {
                for (int i = 0; i < 10; i++)
                {
                    bc.Add(new User { Name = "杜克迪" + i, Age = i });
                }
                bc.CompleteAdding();

                Action action = () =>
                {
                    User u;
                    while (BlockingCollection<User>.TryTakeFromAny(new[] { bc }, out u) != -1)
                    {
                        Console.WriteLine("姓名为：{0},年龄为：{1}", u.Name, u.Age);
                    }
                    //try
                    //{
                    //    while (true)
                    //    {
                    //        var item = bc.Take();
                    //        Console.WriteLine("姓名为：{0},年龄为：{1}", item.Name, item.Age);
                    //    }
                    //}
                    //catch
                    //{
                    //    Console.WriteLine("搞定");
                    //}
                };
                Parallel.Invoke(action, action, action);

            }
        }

        private static void TestConCurrentBag()
        {
            ConcurrentBag<User> cb = new ConcurrentBag<User>();
            for (int i = 0; i < 1000; i++)
            {
                cb.Add(new User { Age = i, Name = i.ToString() });
            }
            User u;
            int number = 0;
            while (!cb.IsEmpty)
            {
                if (cb.TryTake(out u))
                {
                    if (u == null || string.IsNullOrEmpty(u.Name))
                    {
                        Console.WriteLine("第{0}次为空——————————", number.ToString());
                    }
                    Console.WriteLine("姓名：{0}，年龄：{1}", u.Name, u.Age);
                    number++;
                }
                else
                    Console.WriteLine("取不出来？");
                //Interlocked.Increment(ref number);

            }
            if (cb.TryPeek(out u))
                Console.WriteLine("TryPeek succeeded for empty bag!");
            Console.WriteLine("总功执行了" + number + "次");
            //Action action = () =>
            //{
            //    while (!cb.IsEmpty)
            //    {
            //        if (cb.TryTake(out u))
            //            Console.WriteLine("姓名：{0}，年龄：{1}", u.Name, u.Age);
            //        else
            //            Console.WriteLine("取不出来？");
            //    }
            //    if (cb.TryPeek(out u))
            //        Console.WriteLine("TryPeek succeeded for empty bag!");
            //};

            //Parallel.Invoke(action, action, action);
        }

        public static void ParallelFor()
        {
            try
            {
                //Parallel.For(0, 5, (i) =>
                //{
                //    {
                //        throw new Exception("异常了：" + i);
                //    }
                //});
                Parallel.Invoke(() => { throw new Exception("1"); },
                    () => { Thread.Sleep(1500); throw new Exception("2"); },
                    () => { Thread.Sleep(3000); throw new Exception("3"); });
            }
            catch (AggregateException ae)
            {
                foreach (var exp in ae.InnerExceptions)
                    Console.WriteLine(exp.Message);
            }
        }

        public static void Queue()
        {
            int number = 62;
            int inti = 101;
            int numPoc = Environment.ProcessorCount;
            int concurrentLvl = numPoc * 2;
            ConcurrentDictionary<int, int> cd = new ConcurrentDictionary<int, int>(numPoc, inti);
            for (int i = 0; i < number; i++)
            {
                cd[i] = i * i;
            }
            var key = 0;
            var key1 = 0;
            var value = 0;
            cd.AddOrUpdate(100, t =>
            {
                key = t;
                return 100;
            }, (k, v) =>
            {
                key1 = k;
                value = v;
                return 200;
            });
            Console.WriteLine("he square of 23 is {0} (should be {1})", cd[23], 23 * 23);
            // The higher the concurrencyLevel, the higher the theoretical number of operations
            // that could be performed concurrently on the ConcurrentDictionary.  However, global
            // operations like resizing the dictionary take longer as the concurrencyLevel rises. 
            // For the purposes of this example, we'll compromise at numCores * 2.
        }
        #endregion

    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class CachTest
    {
        public T Test<T>()
        {
            return default(T);
        }

        public static string Message
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string message = context.Cache["message"] as string;
                if (string.IsNullOrEmpty(message))
                {
                    string path = @"C:\log.txt";
                    var text = File.ReadAllText(path);
                    context.Cache.Add("message", text, new CacheDependency(path),
                        Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0),
                        CacheItemPriority.AboveNormal, CallBack);
                }
                return Message;
            }
        }

        private static void CallBack(string key, object value, System.Web.Caching.CacheItemRemovedReason reson)
        {

        }
    }

    //class A
    //{
    //    public A()
    //    {
    //        PrintFields();
    //    }
    //    public virtual void PrintFields() { }
    //}
    //class B : A
    //{
    //    int x = 1;
    //    int y;
    //    public B()
    //    {
    //        y = -1;
    //    }
    //    public override void PrintFields()
    //    {
    //        Console.WriteLine("x={0},y={1}", x, y);
    //    }
    //}


    public abstract class A
    {
        public A()
        {
            Console.WriteLine("A");
        }
        public virtual void Fun()
        {
            Console.WriteLine("A.Fun()");
        }
    }

    public class B : A
    {
        public B()
        {
            Console.WriteLine("B");
        }
        public new void Fun()
        {
            Console.WriteLine("B.Fun()");
        }
    }



    #region 并发集合
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    //ALLTAKE

    public class SS<t> : IProducerConsumerCollection<t>
    {
        private ConcurrentQueue<t> queue;
        public SS()
        {
            queue = new ConcurrentQueue<t>();
        }
        #region IProducerConsumerCollection<t> 成员

        public void CopyTo(t[] array, int index)
        {
            throw new NotImplementedException();
        }

        public t[] ToArray()
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(t item)
        {
            queue.Enqueue(item);
            return true;
        }

        public bool TryTake(out t item)
        {
            return queue.TryDequeue(out item);
        }

        #endregion

        #region IEnumerable<t> 成员

        public IEnumerator<t> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICollection 成员

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return queue.Count; }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
    #endregion
}
