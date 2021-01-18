using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Shared.Enums
{
	public enum CrowdSourcedFieldDefinitionFeedbackCompletion
	{
		ActionTaken,
		NoActionTaken,
		Acknowledged,
	}
}
