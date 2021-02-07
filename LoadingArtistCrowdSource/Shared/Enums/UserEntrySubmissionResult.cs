using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Enums
{
	public enum UserEntrySubmissionResult
	{
		NewEntryAdded,
		ExistingEntryEdited,
		NewEntryAddedAndVerified,
		ExistingEntryEditedAndVerified,
	}

	public static class UserEntrySubmissionResultExtensions
	{
		public static string AlertMessage(this UserEntrySubmissionResult @this)
		{
			switch (@this)
			{
				case UserEntrySubmissionResult.NewEntryAdded:
					return "Thank you!";
				case UserEntrySubmissionResult.ExistingEntryEdited:
					return "Success";
				case UserEntrySubmissionResult.NewEntryAddedAndVerified:
					return "Congrats!";
				case UserEntrySubmissionResult.ExistingEntryEditedAndVerified:
					return "Congrats!";
				default:
					return "Success";
			}
		}

		public static string AlertDescription(this UserEntrySubmissionResult @this)
		{
			switch (@this)
			{
				case UserEntrySubmissionResult.NewEntryAdded:
					return "Your entry has been saved, and you have contributed to this comic.";
				case UserEntrySubmissionResult.ExistingEntryEdited:
					return "The changes to your entry have been saved.";
				case UserEntrySubmissionResult.NewEntryAddedAndVerified:
					return "Your entry has been saved, and the field is now verified!";
				case UserEntrySubmissionResult.ExistingEntryEditedAndVerified:
					return "The changes to your entry have been saved, and the field is now verified!";
				default:
					return "The changes were successful";
			}
		}

		public static bool IsAlertVerified(this UserEntrySubmissionResult @this)
		{
			switch (@this)
			{
				case UserEntrySubmissionResult.NewEntryAdded:
				case UserEntrySubmissionResult.ExistingEntryEdited:
					return false;
				case UserEntrySubmissionResult.NewEntryAddedAndVerified:
				case UserEntrySubmissionResult.ExistingEntryEditedAndVerified:
					return true;
				default:
					return false;
			}
		}

		public static UserEntrySubmissionResult ToVerified(this UserEntrySubmissionResult @this)
		{
			switch (@this)
			{
				case UserEntrySubmissionResult.NewEntryAdded:
					return UserEntrySubmissionResult.NewEntryAddedAndVerified;
				case UserEntrySubmissionResult.ExistingEntryEdited:
					return UserEntrySubmissionResult.ExistingEntryEditedAndVerified;
				default:
					return @this;
			}
		}
	}
}
