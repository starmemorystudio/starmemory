using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private Dictionary<int, Item> itemDetailsDictionary;
    public SO_ItemList itemList;
    public List<GameObject> items;
    public List<Item> inventoryItems ;
    public ItemEventSO itemso;
    //物品栏坐标
    public int index;
    public GameObject thisobject;
    public GameObject prfab;
    GameObject prfab1;
    void Awake() {
        index = 0;

        if (instance == null)
            instance = this;
        else
        {
            Destroy(instance);
        }


        //itemso.onEventRaised += refresh(item);
    }

    private void refresh()
    {
        foreach (var item in inventoryItems) { 
            
        }
    }
    public void addItem(Item item) {
        int i = inventoryItems.Count;

        
        inventoryItems.Add(item);
        GameObject itemnew = new GameObject();
        itemnew.transform.parent= items[i].transform;
        
        
        itemnew.AddComponent<UnityEngine.UI.Image>().sprite = item.sprite;
        itemnew.transform.localPosition=new Vector3(0,0,0);
        itemnew.transform.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        //itemnew.transform.localScale=item.transform.localScale;


        itemnew.AddComponent<Item>().itemId = item.itemId;


        Debug.Log(item.itemDetail.name);
        itemnew.name = item.itemDetail.name;
        
        //itemnew.name = "fuvk";

    }

    // Start is called before the first frame update
    
    
    void Start()
    {
        

        //获取子物体
        for (int i = 0; i < 8; i++)
        {
            try
            {
                inventoryItems.Add(GetComponentsInChildren<Item>()[i]);
                // 这里写可能引起异常的语句
            }
            //
            catch (Exception e)
            {
                // 当try中的语句发生异常时，错误处理代码（相当于备用方案）
                //Debug.Log(e.Message);//这边啥错误打印出来，
            }

            items.Add(transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (index < 0) index += 8;
        index = index % 8;
        
        thisobject = items[index];
        for (int i = 0; i < 8; i++)
            items[i].transform.GetChild(0).transform.gameObject.SetActive((i == index) );

        
        
        
    }
}
