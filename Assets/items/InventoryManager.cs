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
    
    //public GameObject prefab;
    GameObject prfab1;
    void Awake() {
        index = 0;
        //itemNum = 0;
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
    public void addItem(GameObject prefab) {
        int itemNum=0;
        while(itemNum<8) {
            if (items[itemNum].GetComponentInChildren<Item>() != null) itemNum = (itemNum + 1);
            else break;
        }
        
        
        GameObject itemnew = Instantiate(prefab);
        itemnew.name=prefab.name;
        itemnew.GetComponent<Item>().itemDetail=prefab.GetComponent<Item>().itemDetail;
        itemnew.transform.SetParent(items[itemNum].transform);



        //itemnew.transform.localPosition=new Vector3(0,0,0);
        itemnew.transform.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        RectTransform rect = itemnew.GetComponent<RectTransform>();
        rect.localScale = Vector3.one;
        rect.anchorMax = new Vector2(1, 1);

        rect.anchorMin = new Vector2(0, 0);
        rect.offsetMax =Vector2.zero ;
        rect.offsetMin = Vector2.zero;

        rect.ForceUpdateRectTransforms();


        //rect(itemnew.GetComponent<RectTransform>());
        //itemNum++;




        itemnew.name = itemnew.GetComponent<Item>().itemDetail.name;
        
        //itemnew.name = "fuvk";

    }

    public void rect(RectTransform rectTransform) {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);

        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);

        rectTransform.anchorMax = new Vector2(1, 1);

        rectTransform.anchorMin = new Vector2(0, 0);

        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        rectTransform.localScale = new Vector3(1, 1, 1);

        rectTransform.localPosition = new Vector3(0, 0, 0);

        rectTransform.localEulerAngles = new Vector3(0, 0, 0);

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
        
            
            thisobject = items[index];

        
        
        for (int i = 0; i < 8; i++){
            
            items[i].transform.GetChild(0).transform.gameObject.SetActive((i == index)&&items[i].GetComponentInChildren<Item>() );
            if(items[i].transform.childCount>1&&items[index].transform.childCount==1){
                    index=i;
            }
        }
        
        
    }
}
