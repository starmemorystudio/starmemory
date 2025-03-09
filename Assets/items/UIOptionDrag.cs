
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class UIOptionDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("目标位置")]
    public List<Transform> TargetLocations;
 
    //存储当前拖拽图片的RectTransform组件
    private RectTransform m_rt;
    private Vector3 m_originPos;
    //存储图片中心点与鼠标点击点的偏移量
    private Vector3 m_offset;
    //当前是否吸附
    private bool isAdsorbed = false;
    
    void Awake(){
        foreach(var Transform in InventoryManager.instance.items)
        TargetLocations.Add(Transform.GetComponent<RectTransform>());
    }
    void Start()
    {
        m_rt = transform.GetComponent<RectTransform>();
        m_originPos = m_rt.position;
    }
    Vector3 tWorldPos;
    int sortcache;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // if (eventData.button == PointerEventData.InputButton.Left)
        // {
            // 存储点击时的鼠标坐标
            isdown=false;
            //UI屏幕坐标转换为世界坐标
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out tWorldPos);
            //计算偏移量   
            m_originPos = m_rt.position;
            m_offset = transform.position - tWorldPos;
            lastparent=m_rt.parent;
        // }
    }
    public Transform lastparent;
    public void OnDrag(PointerEventData eventData)
    {
            // sortcache=m_rt.GetComponent<Canvas>().sortingOrder;
            // m_rt.GetComponent<Canvas>().sortingOrder=100;
            
            m_rt.SetParent(transform.parent.parent.GetChild(8));
            //m_rt.gameObject.layer=LayerMask.NameToLayer("ground");
        // if (eventData.button == PointerEventData.InputButton.Left)
            m_rt.position = Input.mousePosition + m_offset;
    }
    public int parent1;
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isAdsorbed)
        {
            m_rt.position = m_originPos;
            m_rt.SetParent(lastparent);
            //m_originPos = m_rt.position;
        }else{
            
            //如果原位有物品
            if(TargetLocations[parent1].gameObject.GetComponentInChildren<Item>()){
                //如果是左键拖拽，替换
                
                if (eventData.button == PointerEventData.InputButton.Left){
                
                    olditem.position=lastparent.position;
                     olditem.SetParent(lastparent);
                    
                    }
                else if (eventData.button == PointerEventData.InputButton.Right){
                        //右键拖拽合成
                        olditem.SetParent(lastparent);
                        if(olditem.GetComponent<Item>().itemDetail.itemType==m_rt.GetComponent<Item>().itemDetail.itemType&&m_rt.GetComponent<Item>().itemDetail.itemType==Enums.ItemType.star)
                        {
                            Compound(olditem,m_rt);
                            Debug.Log("合成成功!");
                        }else{
                            Debug.Log("包含无法合成物品！");
                        }

                    }
            }
            m_rt.position = TargetLocations[parent1].position;
            m_rt.SetParent(TargetLocations[parent1]);
            
            }
            isdown=true;
            // m_rt.GetComponent<Canvas>().sortingOrder=sortcache;
            m_rt.gameObject.layer=LayerMask.NameToLayer("UI");
            olditem=null;
    }

    private void Compound(Transform olditem1, RectTransform olditem2)
    {
        int newid=olditem1.GetComponent<Item>().itemDetail.id*olditem2.GetComponent<Item>().itemDetail.id;
        ItemDetails newItemDetail=new ItemDetails();
        foreach (var itemdetail in olditem1.GetComponent<Item>().itemList.itemDetails)
        {
            if (itemdetail.id == newid)
            {
                newItemDetail = itemdetail;
            }
        }
        
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath<GameObject>("Assets/items/Item/"+newItemDetail.name+".prefab");
        Debug.Log("Assets/items/Item/"+newItemDetail.name+".prefab");
        Destroy(olditem.gameObject);Destroy(olditem2.gameObject);
        InventoryManager.instance.addItem(prefab);
    }
    

    Transform olditem;
    bool isdown=false;
    void Update()
    {
        for (int i = 0; i < TargetLocations.Count ; i++)
        {
            if (Mathf.Sqrt((m_rt.position - TargetLocations[i].position).magnitude) < 3)
            {
                
                if(TargetLocations[i].gameObject.GetComponentInChildren<Item>()){
                    olditem=TargetLocations[i].GetChild(1);


                }
                    parent1=i;
                    
                    
                    
                    

                    isAdsorbed = true;
                    //isdown=false;
                
                break;
            }
            else{
                isAdsorbed = false;
                
            }
        }
    }
}