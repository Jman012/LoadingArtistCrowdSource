using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Logic
{
	public static class FieldLogic
	{
		public static bool FieldTypeHasOptions(Enums.CrowdSourcedFieldType fieldType)
		{
			switch (fieldType)
			{
				case Enums.CrowdSourcedFieldType.Checkboxes:
				case Enums.CrowdSourcedFieldType.Dropdown:
				case Enums.CrowdSourcedFieldType.MultiDropdown:
				case Enums.CrowdSourcedFieldType.RadioButtons:
					return true;
				case Enums.CrowdSourcedFieldType.FreeformTextarea:
				case Enums.CrowdSourcedFieldType.FreeformTextfield:
				case Enums.CrowdSourcedFieldType.IntegerNumber:
				case Enums.CrowdSourcedFieldType.Section:
				default:
					return false;
			}
		}
	}
}
