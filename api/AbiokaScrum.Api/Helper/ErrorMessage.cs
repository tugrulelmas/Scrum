using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Helper
{
    public class ErrorMessage
    {
        public const string UserNotFound = "UserNotFound";
        public const string InvalidToken = "InvalidToken";
        public const string InvalidPassword = "InvalidPassword";
        public const string InvalidProvider = "InvalidProvider";
        public const string YouCannotRemoveYourselfFromBoard = "You cannot remove yourself from a board";
    }
}