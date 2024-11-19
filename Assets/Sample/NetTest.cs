using CoinMaster;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// 通信テスト用クラス
/// </summary>
public class NetTest : MonoBehaviour
{
    [SerializeField, ReadOnly] UserData _data;
    [SerializeField] string _username;
    [SerializeField] int _facility;
    [SerializeField] long _coin;

    void Start()
    {
        login();
    }

    async void login()
    {
        //ログインするとサーバの値が帰ってくる
        //NOTE: 最初に呼び出すこと。データがないと処理できないようになっている。
        var result = await CoinMasterNetwork.Login();
        Debug.Log(JsonUtility.ToJson(result));
    }


    // エディタ用
#if UNITY_EDITOR
    public string username => _username;
    public int facility => _facility;
    public long coin => _coin;

    public void LoginFromEditor() { login(); }
#endif
}
