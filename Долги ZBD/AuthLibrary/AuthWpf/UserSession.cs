using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthLibrary.Models;

namespace AuthWpf
{
    public class UserSession
    {
        private static readonly UserSession _instance = new();
        private UserSession() { }
        public static UserSession Instance => _instance;

        public CinemaUser? CurrentUser { get; private set; }

        public void SetCurrentUser(CinemaUser user)
            => CurrentUser = user;

        public void Clear()
            => CurrentUser = null;
    }
}
