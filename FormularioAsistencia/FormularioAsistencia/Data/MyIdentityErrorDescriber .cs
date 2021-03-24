using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Data
{//DuplicateUserName.
    public class MyIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = "La contraseña debe tener más de 6 caracteres."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "La contraseña debe tener un caracter especial."
            };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "La contraseña debe tener una letra minúscula."
            };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "La contraseña debe tener una letra mayúscula."
            };
        }
        public override IdentityError DuplicateUserName(string user)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = "Ese correo ya pertenece a otra cuenta."
            };
        }
    }
}
