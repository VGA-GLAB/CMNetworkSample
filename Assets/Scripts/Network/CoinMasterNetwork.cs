using CoinMaster;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

/// <summary>
/// コインマスターライクの通信クラス
/// NOTE: このクラスで通信処理は完結する
/// </summary>
public class CoinMasterNetwork
{
    //URL
    const string BaseURI = "https://i0n9615go4.execute-api.ap-northeast-1.amazonaws.com/develop/cm/{0}/{1}";

    //シングルトン
    static CoinMasterNetwork _instance = new CoinMasterNetwork();
    private CoinMasterNetwork() { }

    /// <summary>
    /// ユーザID用のセーブデータ
    /// </summary>
    [Serializable]
    class UserHashSaveData
    {
        public string UUID = null;  // ID
    }

    UserHashSaveData _save = null;  // ユーザIDのセーブ
    LoginData _login = null;        // ログイン時のパケット(一応持っておく)
    UserData _userData = null;      // ユーザーデータ

    /// <summary>
    /// UUIDの取得
    /// NOTE: UUIDとは、そのユーザがユニークであることを示すIDのこと
    /// </summary>
    /// <returns>UUIDの文字列を返却</returns>
    private async UniTask<string> getUUID()
    {
        //ユーザ情報が書いてあるセーブデータを取得
        _save = await LocalData.LoadAsync<UserHashSaveData>("user.bin");
        if (_save == null || _save?.UUID == null)
        {
            //新規作成
            _save = new UserHashSaveData() { UUID = Guid.NewGuid().ToString() };
            await LocalData.SaveAsync<UserHashSaveData>("user.bin", _save);
            Debug.Log($"New User:{_save.UUID}");
        }
        else
        {
            Debug.Log($"Current User:{_save.UUID}");
        }

        return _save.UUID;
    }

    /// <summary>
    /// ログイン通信
    /// </summary>
    /// <returns>ログイン時のデータ</returns>
    public static async UniTask<LoginData> Login()
    {
        string uuid = await _instance.getUUID();

        Debug.Log(string.Format(BaseURI, uuid, "login"));
        //ログイン通信
        string result = await Network.WebRequest.PostRequest<string>(string.Format(BaseURI, uuid, "login"), "{}");
        LoginData data = JsonUtility.FromJson<LoginData>(result);
        _instance._userData = data.user;
        return data;
    }

    /// <summary>
    /// ユーザデータの保存通信
    /// </summary>
    /// <returns>更新があったデータ</returns>
    public static async UniTask<UpdateData> Save()
    {
        string uuid = await _instance.getUUID();

        //データ保存
        string result = await Network.WebRequest.PostRequest<UserData>(string.Format(BaseURI, uuid, "save"), _instance._userData);
        UpdateData data = JsonUtility.FromJson<UpdateData>(result);
        return data;
    }

    /// <summary>
    /// 攻撃相手のリストを取得する
    /// </summary>
    /// <returns>リストデータ</returns>
    public static async UniTask<ListData> GetList()
    {
        string uuid = await _instance.getUUID();

        Debug.Log(string.Format(BaseURI, uuid, "list"));

        //ログイン通信
        string result = await Network.WebRequest.PostRequest<string>(string.Format(BaseURI, uuid, "list"), "{}");
        ListData data = JsonUtility.FromJson<ListData>(result);
        return data;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <returns>ログイン時のデータ</returns>
    public static async UniTask<AttackData> Attack(string targetUserUUID, long betCoin)
    {
        string uuid = await _instance.getUUID();

        Debug.Log(string.Format(BaseURI, uuid, "attack"));

        var atk = new AttackSender();
        atk.TargetUUID = targetUserUUID;
        atk.BetCoin = betCoin;

        //攻撃をする通信
        string result = await Network.WebRequest.PostRequest<AttackSender>(string.Format(BaseURI, uuid, "attack"), atk);
        AttackData data = JsonUtility.FromJson<AttackData>(result);
        return data;
    }

    /// <summary>
    /// ユーザー名を更新する
    /// </summary>
    /// <param name="name">変更後の値</param>
    public static void UpdateName(string name)
    {
        _instance._userData.name = name;
    }

    /// <summary>
    /// コインの枚数を更新する
    /// NOTE: 通信頻度は少なくすること
    /// </summary>
    /// <param name="coin">変更後の値</param>
    public static void UpdateCoin(long coin)
    {
        _instance._userData.coin = coin;
    }

    /// <summary>
    /// 施設レベルを更新する
    /// </summary>
    /// <param name="level">変更後の値</param>
    public static void UpdateFacility(int level)
    {
        _instance._userData.facility = level;
    }
}
