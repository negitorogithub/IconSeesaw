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

        
    }
}
