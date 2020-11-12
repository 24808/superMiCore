using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// C#扩展
/// </summary>
public static class CSExtend
{
    /// <summary>
    /// 判断一个对象是否为Null
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>结果</returns>
    public static bool IsNull(this object obj)
    {
        return obj == null;
    }
    /// <summary>
    /// 判断一个字符串是否为null或空字符串
    /// </summary>
    /// <param name="str">转换值</param>
    /// <returns>结果</returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
    /// <summary>
    /// 判断一个字符串是否为 null 或空字符串或仅由空字符串组成
    /// </summary>
    /// <param name="str">转换值</param>
    /// <returns>结果</returns>
    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
    /// <summary>
    /// 转换sbyte[-128到127][有符号8位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static sbyte ToSByte(this object obj, sbyte number = 0)
    {
        sbyte result = number;
        if (obj != null)
        {
            bool ok = sbyte.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换byte[0到255][无符号8位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static byte ToByte(this object obj, byte number = 0)
    {
        byte result = number;
        if (obj != null)
        {
            bool ok = byte.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换short[-32,768到32,767][有符号16位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static short ToShort(this object obj, short number = 0)
    {
        short result = number;
        if (obj != null)
        {
            bool ok = short.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换ushort[0到65,535][无符号16位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static ushort ToUShort(this object obj, ushort number = 0)
    {
        ushort result = number;
        if (obj != null)
        {
            bool ok = ushort.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换int[-2,147,483,648到2,147,483,647][有符号32位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static int ToInt(this object obj, int number = 0)
    {
        int result = number;
        if (obj != null)
        {
            bool ok = int.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换uint[0到4,294,967,295][无符号32位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static uint ToUInt(this object obj, uint number = 0)
    {
        uint result = number;
        if (obj != null)
        {
            bool ok = uint.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换long[-9,223,372,036,854,775,808到9,223,372,036,854,775,807][有符号64位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static long ToLong(this object obj, long number = 0)
    {
        long result = number;
        if (obj != null)
        {
            bool ok = long.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换long[0到18,446,744,073,709,551,615][无符号64位整数]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static ulong ToULong(this object obj, ulong number = 0)
    {
        ulong result = number;
        if (obj != null)
        {
            bool ok = ulong.TryParse(obj.ToString(), out result);
        }
        return result;
    }
    /// <summary>
    /// 转换decimal[-7.9x10^28到7.9x10^28]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static decimal ToDecimal(this object obj, decimal number = 0)
    {
        decimal result = number;
        bool ok = decimal.TryParse(obj.ToString(), out result);
        return result;
    }
    /// <summary>
    /// 转换double[±1.5×10^-45到±3.4×10^38]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static double ToDouble(this object obj, double number = 0)
    {
        double result = number;
        bool ok = double.TryParse(obj.ToString(), out result);
        return result;
    }
    /// <summary>
    /// 转换float[±1.5×10^-45到±3.4×10^38]
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static float ToFloat(this object obj, float number = 0)
    {
        float result = number;
        bool ok = float.TryParse(obj.ToString(), out result);
        return result;
    }
    /// <summary>
    /// 转换DateTime
    /// </summary>
    /// <param name="obj">转换值</param>
    /// <param name="number">默认值</param>
    /// <returns>结果</returns>
    public static DateTime ToDateTime(this object obj)
    {
        DateTime result = DateTime.Now;
        bool ok = DateTime.TryParse(obj.ToString(), out result);
        if (ok)
        {
            return result;
        }
        else
        {
            throw new Exception("尝试转换为DateTime类型时失败");
        }
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="str">需要截取的字符串</param>
    /// <param name="length">截取的长度</param>
    /// <param name="Last">结尾追加的字符串</param>
    /// <returns>结果</returns>
    public static string SubStr(this string str, int length, string Last = "...")
    {
        if (str.Length <= length)
        {
            return str;
        }
        else
        {
            return str.Substring(0, length) + Last;
        }
    }
    /// <summary>
    /// 截取电话号码字符串显示格式为[111****1111]
    /// </summary>
    /// <param name="str">需要截取的字符串</param>
    /// <param name="placeholder">占位字符串</param>
    /// <returns>结果</returns>
    public static string SubPhoneStr(this string str, string placeholder = "*")
    {
        if (str != null)
        {
            if (str.Length == 11)
            {
                var first = str.Substring(0, 3);
                var last = str.Substring(7);
                return first + placeholder + placeholder + placeholder + placeholder + last;
            }
            else
            {
                return "不是手机号码";
            }
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 转换一个对象为Json字符串
    /// </summary>
    /// <param name="obj">需要转换的对象</param>
    /// <returns>结果</returns>
    public static string ToJson(this object obj)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }
    /// <summary>
    /// 转换一个Json字符串为对象
    /// </summary>
    /// <typeparam name="Entity">需要转换的对象类型</typeparam>
    /// <param name="str">需要转换的字符串</param>
    /// <returns>结果</returns>
    public static Entity ToObject<Entity>(this string str)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Entity>(str);
    }
    /// <summary>
    /// 字符串转Unicode
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <returns>Unicode编码后的字符串</returns>
    public static string StringToUnicode(this string source)
    {
        var bytes = Encoding.Unicode.GetBytes(source);
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < bytes.Length; i += 2)
        {
            stringBuilder.AppendFormat("\\u{0:x2}{1:x2}", bytes[i + 1], bytes[i]);
        }
        return stringBuilder.ToString();
    }
    /// <summary>  
    /// Unicode字符串转为正常字符串  
    /// </summary>  
    /// <param name="srcText"></param>  
    /// <returns></returns>  
    public static string UnicodeToString(this string srcText)
    {
        string dst = "";
        string src = srcText;
        int len = srcText.Length / 6;
        for (int i = 0; i <= len - 1; i++)
        {
            string str = "";
            str = src.Substring(0, 6).Substring(2);
            src = src.Substring(6);
            byte[] bytes = new byte[2];
            bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
            bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
            dst += Encoding.Unicode.GetString(bytes);
        }
        return dst;
    }
}
