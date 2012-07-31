using System;

namespace Facebook
{
    /// <summary>Represents non-API-specific errors that occur during the execution of logic in the Facebook client.</summary>
    public class FacebookException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="FacebookException" /> class.</summary>
        public FacebookException() : base() { }

        /// <summary>Initializes a new instance of the <see cref="FacebookException" /> class with a specified error message.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public FacebookException(String message)
            : base(message) { }

        /// <summary>Initializes a new instance of the <see cref="FacebookException" /> class with a specified error message.</summary>
        /// <param name="messageFormat">The format of the error message that explains the reason for the exception.</param>
        /// <param name="args">An <see cref="System.Object"/> array containing zero or more objects to format.</param>
        public FacebookException(String messageFormat, params Object[] args)
            : this(messageFormat, null, args) { }

        /// <summary>Initializes a new instance of the <see cref="FacebookException" /> class with a specified error message and a reference to the
        /// inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public FacebookException(String message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>Initializes a new instance of the <see cref="FacebookException" /> class with a specified error message and a reference to the
        /// inner exception that is the cause of this exception.</summary>
        /// <param name="messageFormat">The format of the error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="args">An <see cref="System.Object"/> array containing zero or more objects to format.</param>
        public FacebookException(String messageFormat, Exception innerException, params Object[] args)
            : base(String.Format(messageFormat, args), innerException) { }
    }
}
