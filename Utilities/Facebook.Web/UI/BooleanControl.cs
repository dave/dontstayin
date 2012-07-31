using System;
using System.Linq;
using System.Web.UI;

namespace Facebook.Web.UI
{
    [ParseChildren(false)]
    [PersistChildren(true)]
    public abstract class BooleanControl : Control
    {
        public abstract Boolean IsRendered { get; }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (this.IsRendered)
            {
                base.RenderChildren(writer);
            }
            else
            {
                var elseControl = (
                    from control in this.Controls.OfType<Control>()
                    where control is Else
                    select control).ToList();

                if (elseControl.Count > 0)
                {
                    elseControl.First().RenderControl(writer);
                }
            }
        }
    }
}
