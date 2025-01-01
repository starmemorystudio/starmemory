
using System.Collections.Generic;
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 存储点击时的鼠标坐标
            
            //UI屏幕坐标转换为世界坐标
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out tWorldPos);
            //计算偏移量   
            m_originPos = m_rt.position;
            m_offset = transform.position - tWorldPos;
        }
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            m_rt.position = Input.mousePosition + m_offset;
    }
    public int parent1;
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isAdsorbed)
        {
            m_rt.position = m_originPos;
            //m_originPos = m_rt.position;
        }else{
            if(TargetLocations[parent1].gameObject.GetComponentInChildren<Item>()){
            olditem.parent=m_rt.parent;}
            m_rt.parent =TargetLocations[parent1];
            
        }
    }
    Transform olditem;
    void Update()
    {
        for (int i = 0; i < TargetLocations.Count ; i++)
        {
            if (Mathf.Sqrt((m_rt.position - TargetLocations[i].position).magnitude) < 5)
            {
                
                if(TargetLocations[i].gameObject.GetComponentInChildren<Item>()){
                    olditem=TargetLocations[i].GetChild(1);
                    olditem.position=m_rt.parent.position;
                    

                    

                    


                }
                    parent1=i;
                    m_rt.position = TargetLocations[i].position;
                    
                    
                    

                    isAdsorbed = true;
                    
                

                

                
                break;
            }
            else
                isAdsorbed = false;
        }
    }
}