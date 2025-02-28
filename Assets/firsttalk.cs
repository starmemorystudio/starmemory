using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firsttalk : MonoBehaviour
{
    public voidEventSO dialogueSO;
    public GameObject canvas;
    public InputControls controls1;
    public Dictionary<string, Sprite> spritedic;
    public SpriteList spriteList;
        [Multiline(20)]
    public string text1;
    // Start is called before the first frame update
    void Start()
    {
        InventoryManager.instance.transform.gameObject.SetActive(false);
        canvas=dialogue.instance.transform.gameObject;
        talk();
    }
    private void Awake()
    {
        spritedic = new Dictionary<string, Sprite>();
        spriteList=UnityEditor.AssetDatabase.LoadAssetAtPath<SpriteList>("Assets/setting/SpriteList.asset");
        spritedic=spriteList.spritedic;
        //controls1=new InputControls();
        //controls1.UI.talk.started += talk;
        //anim =npc1Sprite.GetComponent<Animator>();
        //dialogue1 = GetComponentInParent<dialogue>();
        //canPress = false;
        //npc1Sprite.SetActive(false);
        
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void talkover()
    {
       

        // InventoryManager.instance.addItem(prefab);
        
        dialogueSO.onEventRaised -= talkover;
    }

    private void talk()
    {
        
        //num += 1;
        // if (canPress)
        // {
            // canPress = false;
            //Debug.Log("talk");
            canvas.SetActive(true);
            //SetActive(true);
            dialogue.instance.SetCoversation(spritedic,text1, false);
           
            dialogueSO.onEventRaised += talkover;
 
        // }
    }

}
