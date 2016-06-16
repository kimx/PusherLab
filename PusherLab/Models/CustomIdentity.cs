using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PusherLab.Models
{
    public class CustomIdentity : MarshalByRefObject, System.Security.Principal.IIdentity
    {
        #region Private variables

        private readonly FormsAuthenticationTicket _ticket;
        private readonly string _uniqueIdentifier;

        #endregion

        #region Constuctor

        /// <summary>
        /// Initialize the identity with the FormsAuthenticationTicket in order
        /// to read custom userdata.
        /// </summary>
        /// <param name="ticket"></param>
        public CustomIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
            _uniqueIdentifier = _ticket.UserData;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the AuthenticationType being used.
        /// </summary>
        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        /// <summary>
        /// Gets a flag indicating if the user is Authenticated or not.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the user name associated with the FormsAuthenticationTicket.
        /// </summary>
        public string Name
        {
            get { return _ticket.Name; }
        }

        /// <summary>
        /// Gets the FormsAuthenticationTicket associated with this User.
        /// </summary>
        public FormsAuthenticationTicket Ticket
        {
            get { return _ticket; }
        }

        /// <summary>
        /// Gets the user's unique identifier stored in the FormsAuthenticationTicket.
        /// </summary>
        public string UniqueIdentifier
        {
            get
            {
                return _uniqueIdentifier;
            }
        }

        #endregion
    }
}