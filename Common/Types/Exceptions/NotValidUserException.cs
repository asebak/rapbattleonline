#region Using

using System;

#endregion

namespace Common.Types.Exceptions
{
    public class NotValidUserException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotValidUserException" /> class.
        /// </summary>
        public NotValidUserException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotValidUserException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotValidUserException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotValidUserException" /> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public NotValidUserException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotValidUserException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception, or a null reference (Nothing in
        ///     Visual Basic) if no inner exception is specified.
        /// </param>
        public NotValidUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotValidUserException" /> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="args">The arguments.</param>
        public NotValidUserException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}