
using Bobs;
namespace Spotted.Templates.Comments
{
	public interface ICommentsPage
	{
		Thread CurrentThread { get; }
		GroupUsr CurrentThreadGroupUsr { get; }
	}
}
