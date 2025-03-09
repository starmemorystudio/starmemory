using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
     public bool canPress ;
    private InputControls controls1;
    public SO_ItemList itemlist;
    void Awake()
    {
        controls1 = new InputControls();
        controls1.UI.talk.started += take;
    }

void OnEnable()
    {
        controls1.Enable();
        
    }
    void OnDisable()
    {
        controls1.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void take(InputAction.CallbackContext context)
    {
        
        
            if(canPress&&transform.childCount==0&&InventoryManager.instance.thisobject.transform.childCount>1){
                string itemname=InventoryManager.instance.thisobject.transform.GetChild(1).name;
                // Debug.Log(itemname);
            GameObject prefab=(GameObject)AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/items/item_object/{itemname}.prefab");
            // InventoryManager.instance.addItem(prefab);
            GameObject newitem=Instantiate(prefab);
            newitem.GetComponent<Stone>().detail = itemlist.itemDetails.Find(c=>c.name==itemname);
            newitem.name=prefab.name;
            newitem.transform.SetParent(this.transform);
            newitem.transform.localPosition=Vector3.zero;

                // InventoryManager.instance.items[InventoryManager.instance.index]=null;
             Destroy(InventoryManager.instance.thisobject.transform.GetChild(1).gameObject);
            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            canPress = true;
            
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            canPress = false;
            
        }
    }
}
