using System;

namespace Facebook.Web
{
    /// <summary>Represents the method that will handle an event that happens during processing of an event in a Facebook web application.</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">A <see cref="FacebookWebEventArgs" /> object that contains data about the event.</param>
    public delegate void FacebookWebEventHandler(Object sender, FacebookWebEventArgs e);
}
