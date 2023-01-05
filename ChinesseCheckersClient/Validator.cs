using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesseCheckersClient
{
    ///<summary>
    /// This class is responsable to Validate the user input for singin 
    ///<sumary>
    public static class Validator
    {
        public static bool IsEmailValid(string _email)
        {
 
            if (_email.Length == 0) { return false; }
            if (_email.Contains(' ')) { return false; }
            if (!_email.Contains("@")) { return false; }
            
            String[] emailParts = _email.Split('@');
            if (emailParts[0].Length == 0) { return false; }
            if (!emailParts[1].Contains('.')) { return false; }
            
            String[] emailPartsAfterDot = emailParts[1].Split('.');
            if (emailPartsAfterDot[0].Length == 0) { return false; }
            if (emailPartsAfterDot[1].Length == 0) { return false; }

            return true;
        }
        public static bool IsNicknameValid(string _nickname)
        {
            if (_nickname.Length == 0) { return false; }
            if (_nickname.Length > 12) { return false; }
            return true;
        }
        public static bool IsPasswordValid(string _password)
        {
            if (_password.Length < 8) { return false; }
            if (!_password.Any(c => char.IsLower(c))) { return false; } //if not contains Lowers is invalid
            if (!_password.Any(c => char.IsUpper(c))) { return false; }  //if not contains Uppers is invalid
            if (!_password.Any(c => char.IsNumber(c))) { return false; } //if not contains Numbers is invalid 
            return true;
        }
        public static bool IsDuplicatePasswordValid(string _passwordDuplicate,string _password)
        {
            if (_passwordDuplicate.Length == 0) { return false; }
            return _password.Equals(_passwordDuplicate);   
        }

    }
}
