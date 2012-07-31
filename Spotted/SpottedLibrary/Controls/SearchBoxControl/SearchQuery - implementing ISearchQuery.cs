using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using System.Data.SqlClient;

namespace SpottedLibrary.Controls.SearchBoxControl
{
	[Serializable]
	public partial class SearchQuery : ISearchQuery
	{

		public enum SearchQueryPartAction { Include, Exclude }
		public IEnumerable<string> TagsQueryParts { get; private set; }
		
		public string SearchString
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				foreach (var tag in TagsQueryParts){
					if (tag.IndexOf(" ") > -1) {
						sb.Append('"');
						sb.Append(tag);
						sb.Append('"');
					}else{
						sb.Append(tag);
					}
					sb.Append(" ");
				}
				return sb.ToString().Trim();
			}
		}
		public string SearchUrl
		{
			get
			{
				string[] tags = new List<string>(TagsQueryParts).ConvertAll(k => Cambro.Web.Helpers.UrlTextSerialize(k)).ToArray();
				return UrlInfo.MakeUrl("/tags/" + String.Join("-", tags), null);
			}
		}
		public SearchQuery(IEnumerable<string> tags)
		{
			this.TagsQueryParts = tags;
		}
	}
}
