using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookGraphToolkit {
    /// <summary>
    /// The Exception that is thrown by Facebook Graph Toolkit
    /// </summary>
    public class FacebookException : Exception {
        private string _Message="";
        /// <summary>
        /// The message of the Exception
        /// </summary>
        public override string Message {
            get {
                return _Message;
            }
        }
        internal FacebookException(string Message) {
            _Message = Message;
        }
    }
    /// <summary>
    /// The Exception that is thrown when making a Graph Api call to Facebook that requires the user to authorize the application but the user has not done so.
    /// </summary>
    public sealed class RequiedLoginException : FacebookException {
        internal RequiedLoginException() : base("User Authorization is required to perform this action."){
        }
    }
}
