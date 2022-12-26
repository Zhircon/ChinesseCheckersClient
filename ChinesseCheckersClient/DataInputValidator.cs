using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesseCheckersClient
{
    public static class DataInputValidator
    {
        public static bool IsEmailValid(string _email)
        {
            bool isValid = true;
            if (_email.Contains(' ')) { isValid = false; }
            if (!_email.Contains("@")) { isValid = false; }
            
            String[] emailParts = _email.Split('@');
            if (emailParts[0].Length == 0) { isValid = false; }
            if (!emailParts[1].Contains('.')) { isValid = false; }
            
            String[] emailPartsAfterDot = emailParts[1].Split('.');
            if (emailPartsAfterDot[0].Length == 0) { isValid = false; }
            if (emailPartsAfterDot[1].Length == 0) { isValid = false; }

            return isValid;
        }
        public static bool isNicknameValid(string _nickname)
        {
            bool isValid = true;
            if (_nickname.Length > 12) { isValid = false; }
            return isValid;
        }
        public static bool isValidPassword(string _password)
        {
            bool isValid = true;
            if (!_password.Any(c => char.IsLower(c))) { isValid = false; } //if not contains Lowers is invalid
            if (!_password.Any(c => char.IsUpper(c))) { isValid = false; }  //if not contains Uppers is invalid
            if (!_password.Any(c => char.IsNumber(c))) { isValid = false; } //if not contains Numbers is invalid 
            return isValid;
        }

    }
}
