using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    static class MyUtil
    {

        public static String plusClone(String origin)
        {
            return origin + "(Clone)";
        }
        public static void InvokeUtil<T>(this T obj, Action action) => action();

        public static void InvokeUtil<T>(this T obj, Action<T> action) => action(obj);

        public static int GetLastIndex(this Array thisObj) => thisObj.Length - 1;

        public static bool ReturnBoolByPercent(float rate)
        {
            bool bool2Return;
            if (rate > 100f)
            {
                rate = 100f;
            }
            if (rate < 0f)
            {
                rate = 0f;
            }

            float random = (float)new System.Random().NextDouble() * 100;
            if (random <= rate)
            {
                bool2Return = true;
            }
            else
            {
                bool2Return = false;
            }
            return bool2Return;
        }
    }
}
