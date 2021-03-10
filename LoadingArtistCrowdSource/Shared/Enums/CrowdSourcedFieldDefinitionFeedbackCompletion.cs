using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Enums
{
	public enum CrowdSourcedFieldDefinitionFeedbackCompletion
	{
		[Description("Action has been taken")]
		ActionTaken,
		[Description("No action has been taken")]
		NoActionTaken,
		[Description("We acknowledge your feedback, but have not taken action yet")]
		Acknowledged,
	}
}
