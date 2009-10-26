using System;
using System.Collections.Generic;
using System.Text;
using SAS.Entity;

namespace SAS.Plugin
{
    public interface IUser
    {
        void Create(UserInfo user);

        void Ban(int userid);

        void UnBan(int userid);

        void Delete(int userid);

        void LogIn(UserInfo user);

        void LogOut(UserInfo user);
    }
}
