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
        public long stolenCoin;
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

    /// <summary>
    /// セーブ時更新があった情報
    /// </summary>
    [Serializable]
    public class UpdateData
    {
        public int status;
        public long stolenCoin;
    };

    /// <summary>
    /// 攻撃時のUUID
    /// </summary>
    [Serializable]
    public class AttackSender
    {
        public string TargetUUID;
        public long BetCoin;
    };

    /// <summary>
    /// リストを取得する
    /// </summary>
    [Serializable]
    public class ListData
    {
        public int status;
        public UserData[] list;
    };

    /// <summary>
    /// 奪った情報
    /// </summary>
    [Serializable]
    public class AttackData
    {
        public int status;
        public long stealCoin;
    };
}