using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator
{
    public class TestClass2
    {
        public string AA;
        public int BB;
    }

    public class TestClass
    {
        public string A;
        public TestClass2 B;
    }

    public static class Test1
    {
        public static void Test()
        {
            dynamic a = new ExpandoObject();
            a.A = "hi";
            a.B = new ExpandoObject();
            a.B.AA = "bye";
            a.B.BB = 12;

            TestClass b = new TestClass();
            b.A = "hi";
            b.B = new TestClass2();
            b.B.AA = "bye";
            b.B.BB = 12;

            Console.Write(DynamicObjectValidator.Validate<TestClass>(a, b));
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Test1.Test();
            Console.ReadLine();
        }


    }
}
