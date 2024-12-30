
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefination : MonoBehaviour
{
    public enum Persistenttype { 
        ReadWrite,DoNotPerst       
    }
    public Persistenttype persistenttype;
    
    public string ID;
    private void OnValidate()
    {
        if (persistenttype == Persistenttype.ReadWrite)
        { if (ID == string.Empty)
              ID = System.Guid.NewGuid().ToString();
              }
            else { 
            ID=string.Empty;
            }
    }
}
