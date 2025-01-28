//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//namespace BasketWeaver
//{
//    public class UnityCoreMathTest
//    {

//        private int dataCount = 4096 * 4096;
//        private int iterCount = 2097152;
//        float[] data;
//        Stopwatch sw = new Stopwatch();

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static string DateToStringHHmmssfff(DateTime dateTime)
//        {
//            unsafe
//            {

//                char* chars = stackalloc char[13];
//                chars[0] = (char)((int)(dateTime.Hour / 10) + 48);
//                chars[1] = (char)((dateTime.Hour % 10) + 48);
//                chars[2] = ':';
//                chars[3] = (char)((int)(dateTime.Minute / 10) + 48);
//                chars[4] = (char)((dateTime.Minute % 10) + 48);
//                chars[5] = ':';
//                chars[6] = (char)((dateTime.Second / 10) + 48);
//                chars[7] = (char)((dateTime.Second % 10) + 48);
//                chars[8] = '.';
//                chars[9] = (char)((dateTime.Millisecond / 100) + 48);
//                chars[10] = (char)(((dateTime.Millisecond % 100) / 10) + 48);
//                chars[11] = (char)(((dateTime.Millisecond % 10)) + 48);
//                chars[12] = ' ';
//                return new string(chars);
//            }
//        }

//        public UnityCoreMathTest()
//        {
//            System.Random random = new System.Random();

//            data = new float[dataCount];

//            for (int i = 0; i < dataCount; i++)
//            {
//                data[i] = (float)(random.NextDouble() - 0.5f) * (float.MaxValue - float.Epsilon);
//            }


//        }

//        public float TestABS()
//        {
//            float res = 0;
//            sw.Restart();
//            for (int i = 0; i < iterCount; i++)
//            {
//                res = UnityEngine.Mathf.Abs(data[i]);
//            }
//            sw.Stop();
//            return res;
//        }
        
//        public float TestCOS()
//        {
//            float res = 0;
//            sw.Restart();
//            for (int i = 0; i < iterCount; i++)
//            {
//                res = UnityEngine.Mathf.Cos(data[i]);
//            }
//            sw.Stop();
//            return res;
//        }
        

//        public float TestSIN()
//        {
//            float res = 0;
//            sw.Restart();
//            for (int i = 0; i < iterCount; i++)
//            {
//                res = UnityEngine.Mathf.Sin(data[i]);
//            }
//            sw.Stop();
//            return res;
//        }
        
//        public string TestDateTime_UtcNowString()
//        {
//            string res = "";
//            sw.Restart();
//            for (int i = 0; i < iterCount; i++)
//            {
//                res = DateTime.UtcNow.ToString();
//            }
//            sw.Stop();
//            return res;
//        }
        
//        public string TestDateTime_UtcNowFast()
//        {
//            string res = "";
//            sw.Restart();
//            for (int i = 0; i < iterCount; i++)
//            {
//                res = DateToStringHHmmssfff(DateTime.UtcNow);
//            }
//            sw.Stop();
//            return res;
//        }
//        public DateTime TestDateTime_UtcNow()
//        {
//            DateTime res = DateTime.UtcNow;
//            sw.Restart();
//            for (int i = 0; i < iterCount; i++)
//            {
//                res = DateTime.UtcNow;
//            }
//            sw.Stop();
//            return res;
//        }

        
//        public bool TestStartsWith()
//        {
//            bool res = false;
//            sw.Restart();
//            string path = Path.GetRandomFileName();
//            for (int i = 0; i < iterCount/64; i++)
//            {
                
//                string testStr= Path.GetRandomFileName();
//                sw.Start();
//                res = path.StartsWith(testStr);
//                sw.Stop();
//            }
//            return res;
//        }
//        public bool TestContains()
//        {
//            bool res = false;
//            sw.Restart();
//            string path = Path.GetRandomFileName();
//            for (int i = 0; i < iterCount/64; i++)
//            {
//                string testStr= Path.GetRandomFileName();
//                sw.Start();
//                res = path.Contains(testStr);
//                sw.Stop();
//            }
//            return res;
//        }



//        void PrintTime(string TestName, ILog Console)
//        {
//            Console.Log($"{TestName}: Total - {sw.ElapsedMilliSeconds} ms |  Iter - {sw.Elapsed.TotalMilliseconds/iterCount*1E6} ns");
//        }



//        public void RunBenchmark(ILog Console)
//        {
//            float use = TestABS(); PrintTime($"Test: Mathf.Abs() {use}", Console);
            
//            use = TestCOS(); PrintTime($"Test: Mathf.Cos() {use}", Console);
            
//            use = TestSIN(); PrintTime($"Test: Mathf.Sin() {use} ", Console);

//            string hi = TestDateTime_UtcNowString(); PrintTime($"Test: DateTime::UtcNow.ToString(){hi}", Console);
//            hi = TestDateTime_UtcNowFast(); PrintTime($"Test: DateTime::UtcNowFast.ToString(){hi}", Console);
//            DateTime b = TestDateTime_UtcNow(); PrintTime($"Test: DateTime::UtcNow{b}", Console);
//            bool low = TestStartsWith(); PrintTime($"Test: string::StartsWith(){low}", Console);
//            low = TestContains(); PrintTime($"Test: string::Contains(){low}", Console);
//        }


//    }
//}
