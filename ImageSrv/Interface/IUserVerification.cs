using System;
using System.Collections.Generic;
using System.Text;

namespace IFSServ.Interface
{
    public interface IUserVerification
    {
        bool Verification(string username, string password);
    }

    public class UserVerification : IUserVerification
    {
        public bool Verification(string username, string password)
        {
            return true;
        }
    }
}
