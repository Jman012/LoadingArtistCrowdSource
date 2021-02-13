using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Enums
{
	public enum FilterOperator
	{
		Equals,
		Contains,
	}

	public static class FilterOperatorExtension
	{
		public static readonly FilterOperator[] AllOperators = (FilterOperator[])Enum.GetValues(typeof(FilterOperator));
		public static string Description(this FilterOperator @this)
		{
			switch (@this)
			{
				case FilterOperator.Equals: return "=";
				case FilterOperator.Contains: return "contains";
				default: return @this.ToString();
			}
		}
	}
}
