using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ip_api.Net45.AppCode
{
	public static class Common
	{
		public static int? BoolToInt(bool? value)
		{
			if (value == true)
			{
				return 1;
			}
			else if (value == false)
			{
				return 0;
			}
			else
			{
				return null;
			}
		}

		public static string GetDescription<T>(this T enumerationValue) where T : struct
		{
			var type = enumerationValue.GetType();
			if (!type.IsEnum)
			{
				throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
			}
			var memberInfo = type.GetMember(enumerationValue.ToString());
			if (memberInfo.Length > 0)
			{
				var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs.Length > 0)
				{
					return ((DescriptionAttribute)attrs[0]).Description;
				}
			}
			return enumerationValue.ToString();
		}
		
		public static string ToString<T>(this T[] enumerationValue) where T : struct
		{
			var type = enumerationValue.GetType();
			if (!type.IsEnum)
			{
				throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
			}
			List<string> values = new List<string>();
			foreach (var item in enumerationValue)
			{
				var memberInfo = type.GetMember(item.ToString());
				if (memberInfo.Length > 0)
				{
					var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

					if (attrs.Length > 0)
					{
						return ((DescriptionAttribute)attrs[0]).Description;
					}
				}
			}
			return string.Join(",", values);
		}
		public static int Push<T>(this T[] source, T value)
		{
			//WIP
			var index = Array.IndexOf(source, default(T));

			if (index != -1)
			{
				source[index] = value;
			}

			return index;
		}
		public static bool Contains<T>(this T[] source, T value)
		{
			var index = Array.IndexOf(source, value);
			return index != -1;
		}
	}
}
