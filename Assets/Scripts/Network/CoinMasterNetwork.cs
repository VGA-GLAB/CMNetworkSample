using CoinMaster;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class CoinMasterNetwork
{
    const string BaseURI = "https://i0n9615go4.execute-api.ap-northeast-1.amazonaws.com/develop/cm/{0}/{1}";

    static CoinMasterNetwork _instance = new CoinMasterNetwork();
    private CoinMasterNetwork() { }


    [Serializable]
    class UserHashSaveData
    {
        public string UUID = null;
    }

    UserHashSaveData _save = null;
    LoginData _login = null;
    UserData _userData = null;

    private async UniTask<string> getUUID()
    {
        //���[�U��񂪏����Ă���Z�[�u�f�[�^���擾
        _save = await LocalData.LoadAsync<UserHashSaveData>("user.bin");
        if (_save == null || _save?.UUID == null)
        {
            //�V�K�쐬
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

    public static async UniTask<LoginData> Login()
    {
        string uuid = await _instance.getUUID();

        Debug.Log(string.Format(BaseURI, uuid, "login"));
        //���O�C���ʐM
        string result = await Network.WebRequest.PostRequest<string>(string.Format(BaseURI, uuid, "login"), "{}");
        LoginData data = JsonUtility.FromJson<LoginData>(result);
        _instance._userData = data.user;
        return data;
    }

    public static async UniTask<int> Save()
    {
        string uuid = await _instance.getUUID();

        //�f�[�^�ۑ�
        await Network.WebRequest.PostRequest<UserData>(string.Format(BaseURI, uuid, "save"), _instance._userData);
        return 0;
    }

    public static void UpdateName(string name)
    {
        _instance._userData.name = name;
    }

    public static void UpdateCoin(long coin)
    {
        _instance._userData.coin = coin;
    }

    public static void UpdateFacility(int level)
    {
        _instance._userData.facility = level;
    }
}
