using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("ÊÂ¼þ¼àÌý")]
    public voidEventSO saveDataEvent;

    private List<ISaveable> saveablelist = new List<ISaveable>();
    public Data savedata;
    public Dictionary<string, int> goodwill;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        savedata = new Data();
        

    }
    private void Start()
    {
        Load();
    }

    private void laterUpdate()
    {
        
            
            if(savedata.IntData.Count!=0)
            goodwill = savedata.IntData;
        
    }
    private void OnEnable()
    {
        saveDataEvent.onEventRaised += Save;
    }

    private void OnDisable()
    {
        saveDataEvent.onEventRaised -= Save;
    }

    public void RegisterSaveData(ISaveable saveable) {
        if (!saveablelist.Contains(saveable)) {
            saveablelist.Add(saveable);
        
        }
    
    }


    public void UnRegisterSaveData(ISaveable saveable) { 
    
        saveablelist.Remove(saveable);
    
    }

    private void Save()
    {
        foreach(var saveable in saveablelist)
        {
            saveable.GetSaveData(savedata);
        }


        foreach (var data in savedata.IntData) { 
        
        Debug.Log(data.Key+"  "+data.Value);
        }
        //Write(savedata);
    }

    private void Load()
    {
        Debug.Log("Load!");
       // if (findData()== null|| savedata.IntData.Count == 0) {
       //     return;
      //  }
        if(findData()!=null)
            savedata = findData();

        foreach (var saveable in saveablelist)
        {
            
            saveable.LoadSaveData(savedata);
            
        }
        



    }
    public static char[] KeyChars={'g','e','a','r','g','a','m','e'};
    private int datatime=0;

    public static string Encrypt(string data) { 
        char[] datachars = data.ToCharArray();
        for (int i = 0; i < datachars.Length; i++) { 
             char dataChar = datachars[i];
            char keyChar = KeyChars[i % KeyChars.Length];
            char newChar =(char)( dataChar ^ keyChar);

            datachars[i] = newChar;
        }
        return new string(datachars);
    }

    private Data findData()
    {
        
        string path = Application.persistentDataPath + string.Format("/users/player{0}.json", datatime);
        if (File.Exists(path))
        {
            string jsonData = Encrypt(File.ReadAllText(path));

            Data data = JsonConvert.DeserializeObject<Data>(jsonData);
            return data;
        }
        else return null;
    }

    public void Write()
    {
        if (!File.Exists(Application.persistentDataPath + "/users")) {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/users");
        
        }
        string dataJson = Encrypt( JsonConvert.SerializeObject(savedata)); 
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/player{0}.json",datatime),dataJson);
        Debug.Log(dataJson) ;
    }
        
}
