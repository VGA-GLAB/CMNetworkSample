using System;
using UnityEditor.Rendering;

namespace CoinMaster
{
    [Serializable]
    public class UserData
    {
        public string uuid;
        public string name;
        public long coin;
        public int facility;
        public DateTime lastAccess;
        public DateTime createdAt;
    };

    [Serializable]
    public class LoginData
    {
        public int status;
        public UserData user;
    };
}