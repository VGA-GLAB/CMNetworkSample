using CoinMaster;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class NetTest : MonoBehaviour
{
    [SerializeField, ReadOnly] UserData _data;
    [SerializeField] string _username;
    [SerializeField] int _facility;
    [SerializeField] long _coin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    public string username => _username;
    public int facility => _facility;
    public long coin => _coin;
#endif
}
