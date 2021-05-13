using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Tools.ErrorCodes
{
    public class IdentityErrorCodes
    {
        public const string DefaultError = "DefaultError";
        public const string ConcurrencyFailure = "ConcurrencyFailure";
        public const string PasswordMismatch = "PasswordMismatch";
        public const string InvalidToken = "InvalidToken";
        public const string LoginAlreadyAssociated = "LoginAlreadyAssociated";
        public const string InvalidUserName = "InvalidUserName";
        public const string InvalidEmail = "InvalidEmail";
        public const string DuplicateUserName = "DuplicateUserName";
        public const string DuplicateEmail = "Correo Electrónico Duplicado";
        public const string InvalidRoleName = "InvalidRoleName";
        public const string DuplicateRoleName = "DuplicateRoleName";
        public const string UserAlreadyHasPassword = "UserAlreadyHasPassword";
        public const string UserLockoutNotEnabled = "UserLockoutNotEnabled";
        public const string UserAlreadyInRole = "UserAlreadyInRole";
        public const string UserNotInRole = "UserNotInRole";
        public const string PasswordTooShort = "PasswordTooShort";
        public const string PasswordRequiresNonAlphanumeric = "PasswordRequiresNonAlphanumeric";
        public const string PasswordRequiresDigit = "PasswordRequiresDigit";
        public const string PasswordRequiresLower = "PasswordRequiresLower";
        public const string PasswordRequiresUpper = "PasswordRequiresUpper";

        public static Dictionary<string, string> All = new Dictionary<string, string>() 
        {
            { "DuplicateEmail", "Correo electrónico duplicado" },
            { "DuplicateUserName", "El usuario ya existe" },
            { "PasswordRequiresUpper", "Contraseña requiere mayusculas" },
            { "PasswordRequiresLower", "Contraseña requiere minusculas" },
            { "PasswordRequiresDigit", "Contraseña requiere digitos" },
            { "PasswordTooShort", "Contraseña es muy corta" },
            { "InvalidEmail", "Correo electrónico invalido" },
            { "InvalidUserName", "Nombre de usuario invalido" },
        };
    }
}
