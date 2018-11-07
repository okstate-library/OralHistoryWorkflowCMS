using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public byte UserType { get; set; }

        public string Username { get; set; }

        public SecureString Password { get; set; }

        public string UserTypeName { get; set; }

    }
}
