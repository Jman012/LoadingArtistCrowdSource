using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Models
{
	/// <summary>
	/// Defines possible options for some types of <see cref="CrowdSourcedFieldDefinition"/>s.
	/// </summary>
	[Table(nameof(CrowdSourcedFieldDefinitionOption))]
	public class CrowdSourcedFieldDefinitionOption
	{
		public Guid CrowdSourcedFieldDefinitionId { get; set; }
		public string Code { get; set; } = "";
		public string Text { get; set; } = "";
		public string Description { get; set; } = "";
		public string? URL { get; set; }
		public int DisplayOrder { get; set; }

		public CrowdSourcedFieldDefinition CrowdSourcedFieldDefinition { get; set; } = null!;
	}
}
