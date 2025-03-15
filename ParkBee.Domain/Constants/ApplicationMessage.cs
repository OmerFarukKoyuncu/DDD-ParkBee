using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Domain.Constants
{
    public static class ApplicationMessage
    {
        public static string AppCode = "PRB";

        public static readonly string Failed = "-1".WithCode();
        public static readonly string Success = "0".WithCode();
        public static readonly string DoorIsReachable = "1".WithCode();
        public static readonly string EmptyPlaceInGarage = "2".WithCode();
        public static readonly string NoPlaceInGarage = "3".WithCode();
        public static readonly string GarageNotFound = "4".WithCode();
        public static readonly string FailedToAccessEntranceDoor = "5".WithCode();
        public static readonly string UnFinishedSession = "6".WithCode();
        public static readonly string UserAlreadyExists = "7".WithCode();
        public static readonly string UserNotFound = "8".WithCode();
        public static readonly string FailedToAccessDoor = "9".WithCode();

        private static readonly Dictionary<string, string> UserMessages =
            new()
            {
                { "-1","Failed" },
                { "0", "Success" },
                { "1", "The door is reachable" },
                { "2", "There is a place in the garage" },
                { "3", "There is no place in the garage" },
                { "4", "No garage found with given criteria" },
                { "5", "Failed to access entrance door" },
                { "6", "User have unfinished session" },
                { "7", "User already available" },
                { "8", "User not found" },
                { "9", "Failed to access door" },
            };
        private static string WithCode(this string code)
        {
            return $"{AppCode}{code}";
        }
        public static string Message(this string code, params object[] messageParams)
        {
            var originCode = code.ReplaceFirst(AppCode, "");
            var valueExists = UserMessages.TryGetValue(originCode, out var errorMessage);
            if (!valueExists)
            {
                return code;
            }
            return string.Format(errorMessage, messageParams);
        }
        public static string UserMessage(this string code, params object[] messageParams)
        {
            var originCode = code.ReplaceFirst(AppCode, "");
            var exists = UserMessages.TryGetValue(originCode, out var errorMessage);
            if (!exists)
            {
                return code;
            }
            return string.Format(errorMessage, messageParams);
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text[(pos + search.Length)..];
        }
    }
}