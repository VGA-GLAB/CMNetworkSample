using System;
using UnityEditor.Rendering;

namespace CoinMaster
{
    /// <summary>
    /// ユーザーデータクラス
    /// </summary>
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

    /// <summary>
    /// ログイン時の情報
    /// </summary>
    [Serializable]
    public class LoginData
    {
        public int status;
        public UserData user;
    };
}