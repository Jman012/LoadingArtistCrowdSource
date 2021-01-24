﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LoadingArtistCrowdSource.Shared.Utilities
{
	public static class Utilities
	{
		public static string GetMemberName<T>(Expression<Func<T>> expression)
		{
			var expressionBody = (MemberExpression)expression.Body;
			var value = expressionBody.Member.Name;
			return value;
		}

		public static string GetDisplayName<T>(Expression<Func<T>> expression)
		{
			var expressionBody = (MemberExpression)expression!.Body;
			var value = expressionBody.Member.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
			return value?.DisplayName ?? expressionBody.Member.Name ?? "";
		}

		public static string GetDescription<T>(Expression<Func<T>> expression)
		{
			var expressionBody = (MemberExpression)expression!.Body;
			var value = expressionBody.Member.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
			return value?.Description ?? expressionBody.Member.Name ?? "";
		}

		public static string GetDescriptionAttributeValue<T>(Expression<Func<T>> expression)
		{
			var expressionBody = (MemberExpression)expression!.Body;
			var value = expressionBody.Member.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
			return value?.Description ?? "";
		}

		public static string GetEnumDescription<TEnum>(TEnum value)
		{
			FieldInfo fi = value!.GetType().GetField(value.ToString()!)!;

			DescriptionAttribute[] attributes = (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[])!;

			if (attributes != null && attributes.Any())
			{
				return attributes.First().Description;
			}

			return value.ToString()!;
		}

	}
}
