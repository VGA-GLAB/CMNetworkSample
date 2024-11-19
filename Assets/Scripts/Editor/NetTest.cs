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
            view.LoginFromEditor();
        }
        if (GUILayout.Button(@"セーブ"))
        {
            save();
        }
    }

    //保存テスト
    async void save()
    {
        //値を変更して保存
        NetTest view = target as NetTest;
        CoinMasterNetwork.UpdateName(view.username);
        CoinMasterNetwork.UpdateCoin(view.coin);
        CoinMasterNetwork.UpdateFacility(view.facility);
        await CoinMasterNetwork.Save();
    }
}