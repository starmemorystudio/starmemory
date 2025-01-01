using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
    public int itemId;
    public bool isover;

    public void OnPointDown()
    {
       // Debug.Log(this.itemDetail.description);
    }


    private void Awake()
    {

        
        spriteRenderer = GetComponent<SpriteRenderer>();

        itemList = (SO_ItemList)AssetDatabase.LoadAssetAtPath<ScriptableObject>("Assets/items/so_ItemList.asset");
        foreach (var itemdetail in itemList.itemDetails)
        {
            if (itemdetail.id == itemId)
            {
                itemDetail = itemdetail;
            }
        }

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
    }
    public void Init(int ItemIdParm)
    {
      
    }
}
