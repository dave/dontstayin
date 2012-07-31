using System;
namespace Facebook.Web.UI
{
    public class Else : BooleanControl
    {
        public override Boolean IsRendered
        {
            get
            {
                if (this.Parent is BooleanControl)
                {
                    return !((BooleanControl)this.Parent).IsRendered;
                }
                else return false;
            }
        }
    }
}
