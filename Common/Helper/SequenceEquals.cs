using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    /// <summary>
    /// 元素比较
    /// </summary>
    public static class SequenceEquals
    {
        public static bool ByteSequenceEquals(byte[] a, byte[] b)
        {
            if(a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
