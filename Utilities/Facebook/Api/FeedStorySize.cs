namespace Facebook.Api
{
    /// <summary>Defines the sizes of stories allowed when pubishing a Feed story.</summary>
    /// <seealso cref="Facebook.Api.Controllers.FeedController"/>
    public enum FeedStorySize : byte
    {
        /// <summary>A one-line story that contains only a title.</summary>
        OneLine = 1,

        /// <summary>A two-line story that contains a title and a body.</summary>
        TwoLine = 2
    }
}
