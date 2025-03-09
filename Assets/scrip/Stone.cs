using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Sirenix.OdinInspector.Editor.Validation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Stone : MonoBehaviour
{
    public bool canPress;
    private InputControls controls1;
    public GameObject prefab;
    public ItemDetails detail;
    public SO_ItemList itemlist;
    public bool onground;

    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/items/Item/{detail.name}_UI.prefab");
        prefab.GetComponent<Item>().itemDetail = detail;
        prefab.name = name;
    }
    void OnEnable()
    {
        controls1.Enable();
        // transform.localPosition = Vector3.zero;
    }
    void OnDisable()
    {
        controls1.Disable();
    }
    void Awake()
    {
        controls1 = new InputControls();
        controls1.UI.talk.started += take;
        // if(detail==null)
        // Debug.Log(detail.name);
        if (detail.name == "")
            detail = itemlist.itemDetails.Find(c => c.name == name);


        // prefab.GetComponent<Image>().sprite=;
    }

    private void take(InputAction.CallbackContext context)
    {
        // Debug.Log("take");
        if (onground)
        {
            if (canPress)
            {

                InventoryManager.instance.addItem(prefab);
                Destroy(gameObject);
            }

        }
        else
        {
             if (canPress)
            {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPress = true;

        }
        if (collision.CompareTag("tilemap"))
        {
            onground = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPress = false;

        }
    }
}
