using System;

namespace Facebook.Api.Controllers
{
    public partial class VideoController
    {
        public FacebookResponse<Video> Upload()
        {
            throw new NotSupportedException("Sorry, video uploads are not supported in this release of the API client.");
        }
    }
}
