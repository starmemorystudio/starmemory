using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public interface ISaveable
{
    DataDefination GetDataID();
    void RegisterSaveData() =>SaveManager.instance.RegisterSaveData(this);
    
    
    void UnregisterSaveData()=>SaveManager.instance.UnRegisterSaveData(this);

    void GetSaveData(Data data);
    void LoadSaveData(Data data);
}
