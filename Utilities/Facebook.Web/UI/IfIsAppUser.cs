using System;
using System.Web.UI;
using Facebook.Web.UI;

namespace Facebook.Web.UI
{
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class IfIsAppUser : BooleanControl
    {
        public override Boolean IsRendered
        {
            get { return FacebookHttpSession.Current.Added; }
        }
    }
}
