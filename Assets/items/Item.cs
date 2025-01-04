using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField]
    
    public SpriteRenderer spriteRenderer;
    //public UnityEvent<Item> getItem;
    public Sprite sprite;
    public ItemDetails itemDetail;
    public SO_ItemList itemList;
    private GameObject itemDescription;
    public int itemId;
    public bool isover;
    public GameObject descriptionPrefab;

    public void OnPointDown()
    {
       // Debug.Log(this.itemDetail.description);
    }

    public void OnPointEnter(){
        transform.GetChild(0).gameObject.SetActive(true);

    }
    public void OnPointExit(){
        transform.GetChild(0).gameObject.SetActive(false);
        
    }
public GameObject LoadPrefabRuntime(string prefabName)
{
    GameObject prefab = Resources.Load<GameObject>(prefabName);
    return prefab;
}


    private void Awake()
    {

        
        spriteRenderer = GetComponent<SpriteRenderer>();

        itemList = (SO_ItemList)AssetDatabase.LoadAssetAtPath<ScriptableObject>("Assets/items/so_ItemList.asset");
        //挂载描述
        
        //itemDescription.transform.localPosition=Vector3.zero;
       
        foreach (var itemdetail in itemList.itemDetails)
        {
            if (itemdetail.id == itemId)
            {
                itemDetail = itemdetail;
            }
        }
         DescriptionInit();
    }
    private void Start()
    {

    }
    private void Update()
    {
        foreach (var itemdetail in itemList.itemDetails)
        {
            if (itemdetail.id == itemId)
            {
                itemDetail = itemdetail;
            }
        }

        // if(GetComponentInChildren<RectTransform>()){
        //     RectTransform rect=transform.GetChild(0).GetComponent<RectTransform>();
        //     rect.position=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     //ect.transform.localPosition=Vector3.zero;
        // }
    }
    public void Init(int ItemIdParm)
    {
      
    }
Vector3 tWorldPos;

    private Vector3 offset;

    private void  DescriptionInit(){
        itemDescription =Instantiate(descriptionPrefab);
        List<GameObject> Description =new List<GameObject>();
        for(int i=0;i<3;i++)Description.Add(itemDescription.transform.GetChild(i).gameObject);
        Description[1].GetComponent <TextMeshProUGUI>().text=itemDetail.name;
        Description[2].GetComponent<TextMeshProUGUI>().text=itemDetail.description;
        itemDescription.transform.SetParent(this.transform);

        RectTransform rect=transform.GetChild(0).GetComponent<RectTransform>();
        rect.localPosition=new Vector3(0,-270,0);
        rect.transform.gameObject.SetActive(false);

    }
}
