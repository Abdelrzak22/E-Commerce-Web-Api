using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }


        public static Error Failuer(string Code="General.Failure",string Description="General Failure Has Occured")
        {
            return new Error(Code, Description, ErrorType.Failure);
        }
        public static Error Validation(string Code="General.Validation",string Description="General Validation Has Occured")
        {
            return new Error(Code, Description, ErrorType.Validation);
        }
        public static Error NotFound(string Code= "General.NotFound", string Description= "General NotFound Has Occured")
        {
            return new Error(Code, Description, ErrorType.NotFound);
        }
        public static Error Unauthorized(string Code= "General.Unauthorized", string Description= "General Unauthorized Has Occured")
        {
            return new Error(Code, Description, ErrorType.Unauthorized);
        }
        public static Error Forbidden(string Code= "General.Forbidden", string Description= "General Forbidden Has Occured")
        {
            return new Error(Code, Description, ErrorType.Forbidden);
        }
        public static Error InvalidCredentials(string Code= "General.InvalidCredentials", string Description= "General InvalidCredentials Has Occured")
        {
            return new Error(Code, Description, ErrorType.InvalidCredentials);
        }
    }
}
