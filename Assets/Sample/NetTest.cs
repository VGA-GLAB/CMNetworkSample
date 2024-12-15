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

    public async void save()
    {
        //保存テスト
        var result = await CoinMasterNetwork.Save();
        Debug.Log(JsonUtility.ToJson(result));
    }

    public async void list()
    {
        //攻撃対象のリストを取得する
        var result = await CoinMasterNetwork.GetList();
        Debug.Log(JsonUtility.ToJson(result));
    }

    public async void attack()
    {
        //攻撃
        var result = await CoinMasterNetwork.Attack("acb83937-e330-4787-b064-2865ab98bb74", 100);
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
