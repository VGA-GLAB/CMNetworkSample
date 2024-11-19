using UnityEngine;
using UnityEditor;
using PlasticGui.Configuration.CloudEdition.Welcome;
using CoinMaster;

/// <summary>
/// シーン依存系設定のエディタ拡張
/// </summary>
[CustomEditor(typeof(NetTest), true, isFallback = true)]
public class EventSystemViewerEditor : Editor
{
    /// <summary>
    /// インスペクタ上で設定
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NetTest view = target as NetTest;
        if (GUILayout.Button(@"ログイン"))
        {
            login();
        }
        if (GUILayout.Button(@"セーブ"))
        {
            save();
        }
    }

    async void login()
    {
        var result = await CoinMasterNetwork.Login();
        Debug.Log(JsonUtility.ToJson(result));
    }

    async void save()
    {
        NetTest view = target as NetTest;
        CoinMasterNetwork.UpdateName(view.username);
        CoinMasterNetwork.UpdateCoin(view.coin);
        CoinMasterNetwork.UpdateFacility(view.facility);
        await CoinMasterNetwork.Save();
    }
}