using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class shopbase : MonoBehaviour,ISaveable

{

    public UnityEvent<shopbase> touchEvent;
    // Start is called before the first frame update
    public GameObject tips;
    //private InputControl streetcontrol;
    public int presstimes=0;
    private int goodWill=0;

    private void OnEnable()
    {
        //streetcontrol.Enable();
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        //streetcontrol.Disable();
        ISaveable saveable = this;
        saveable.UnregisterSaveData();
    }

    // Update is called once per frame
    private void Awake()
    {
        tips.SetActive(false);
        //streetcontrol = new Streetcontrol();
        //streetcontrol.street.Tips.started += touch;
    }
    private void Update()
    {
        presstimes = goodWill;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("enter shop");
            tips.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("exit lottery");
            tips.SetActive(false);

        }
    }
    public virtual void  talk()
    {
           
     
    }
    private void touch(InputAction.CallbackContext context)
    {
        if (tips.activeSelf)
        {
            talk();
            goodWill++;
            touchEvent?.Invoke(this);
        }
        
        
        
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.IntData.ContainsKey(GetDataID().ID + "goodWill"))
        {
            data.IntData[GetDataID().ID + "goodWill"] = goodWill;


        }
        else { 
            data.IntData.Add(GetDataID().ID+"goodWill", goodWill);
        }
    }

    public void LoadSaveData(Data data)
    {
        if (data.IntData.ContainsKey(GetDataID().ID + "goodWill")) { 
            goodWill= data.IntData[GetDataID().ID + "goodWill"];
        }
    }
}
