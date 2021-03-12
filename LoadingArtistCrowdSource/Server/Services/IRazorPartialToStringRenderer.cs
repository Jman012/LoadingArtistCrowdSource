using System.Threading.Tasks;

namespace LoadingArtistCrowdSource.Server.Services
{
	public interface IRazorPartialToStringRenderer
	{
		Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
	}
}