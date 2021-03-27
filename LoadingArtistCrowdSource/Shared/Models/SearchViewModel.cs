using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using LoadingArtistCrowdSource.Shared.Enums;

namespace LoadingArtistCrowdSource.Shared.Models
{
	public class SearchViewModel
	{
		[DisplayName("Number")]
		public int? Id { get; set; }
		public string? Code { get; set; }
		public string? Title { get; set; }
		public string? Tooltip { get; set; }
		public string? Description { get; set; }
		public string? Transcript { get; set; }
		public SearchEntryViewModel[] SearchEntries { get; set; } = new SearchEntryViewModel[] { };

		public string EncodeToQueryString()
		{
			List<string> queryItems = new List<string>();

			if (Id.HasValue)
			{
				queryItems.Add("id=" + Uri.EscapeDataString(Id.Value.ToString()));
			}
			if (!string.IsNullOrEmpty(Code))
			{
				queryItems.Add("code=" + Uri.EscapeDataString(Code));
			}
			if (!string.IsNullOrEmpty(Title))
			{
				queryItems.Add("title=" + Uri.EscapeDataString(Title));
			}
			if (!string.IsNullOrEmpty(Tooltip))
			{
				queryItems.Add("tool=" + Uri.EscapeDataString(Tooltip));
			}
			if (!string.IsNullOrEmpty(Description))
			{
				queryItems.Add("desc=" + Uri.EscapeDataString(Description));
			}
			if (!string.IsNullOrEmpty(Transcript))
			{
				queryItems.Add("trsc=" + Uri.EscapeDataString(Transcript));
			}

			queryItems.AddRange(SearchEntries.SelectMany(se => se.EncodeToQueryItems()));

			return string.Join("&", queryItems);
		}

		public void DecodeFromQueryString(string query)
		{
			NameValueCollection nvQuery = HttpUtility.ParseQueryString(query);

			foreach (var nv in nvQuery.AllKeys)
			{
				if (nv == null) continue;

				switch (nv)
				{
					case "id":
						Id = int.TryParse(nvQuery.GetValues("id")?.FirstOrDefault(), out int id) ? id : (int?)null;
						break;
					case "code":
						Code = nvQuery.GetValues("code")?.FirstOrDefault();
						break;
					case "title":
						Title = nvQuery.GetValues("title")?.FirstOrDefault();
						break;
					case "tool":
						Tooltip = nvQuery.GetValues("tool")?.FirstOrDefault();
						break;
					case "desc":
						Description = nvQuery.GetValues("desc")?.FirstOrDefault();
						break;
					case "trsc":
						Transcript = nvQuery.GetValues("trsc")?.FirstOrDefault();
						break;
					default:
						string[] values = nvQuery.GetValues(nv) ?? new string[] { };
						string fieldCode = nv;
						bool setOperator = false;
						if (fieldCode.EndsWith(".op"))
						{
							fieldCode = fieldCode.Substring(0, fieldCode.Length - ".op".Length);
							setOperator = true;
						}

						var searchEntry = SearchEntries.FirstOrDefault(se => se.FieldCode == fieldCode);
						if (searchEntry != null)
						{
							if (setOperator)
							{
								searchEntry.Operator = Enum.TryParse(values.FirstOrDefault(), out SearchEntryOperator op) ? op : default;
							}
							else
							{
								foreach (string value in values)
								{
									var searchEntryOption = searchEntry.FieldValues.FirstOrDefault(fv => fv.Code == value);
									if (searchEntryOption != null)
									{
										searchEntryOption.Filtered = true;
									}
									else if (searchEntry.FieldValues.Count() == 1 && searchEntry.FieldValues.First().Code == "" && searchEntry.FieldValues.First().Filtered == true)
									{
										// Special case: Integer/Textfield/Textarea input. No options with pre-set Code.
										searchEntry.FieldValues.First().Code = value;
									}
								}
							}
						}
						break;
				}
			}
		}
	}

	public class SearchEntryViewModel
	{
		public string FieldCode { get; set; } = "";
		public SearchEntryOperator Operator { get; set; } = SearchEntryOperator.Any;
		public SearchEntryOptionViewModel[] FieldValues { get; set; } = new SearchEntryOptionViewModel[] { };

		public List<string> EncodeToQueryItems()
		{
			List<string> queryItems = new List<string>();

			var filteredValues = FieldValues.Where(fv => !string.IsNullOrEmpty(fv.Code) && fv.Filtered);
			
			if (string.IsNullOrEmpty(FieldCode) || filteredValues.Count() == 0)
			{
				return queryItems;
			}

			if (Operator != default)
			{
				queryItems.Add(Uri.EscapeDataString(FieldCode) + ".op=" + Uri.EscapeDataString(Operator.ToString()));
			}

			foreach (var fieldValue in filteredValues)
			{
				queryItems.Add(Uri.EscapeDataString(FieldCode) + "=" + Uri.EscapeDataString(fieldValue.Code));
			}
			
			return queryItems;
		}
	}

	public class SearchEntryOptionViewModel
	{
		public string Code { get; set; } = "";
		public bool Filtered { get; set; }
	}
}