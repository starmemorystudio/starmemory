using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class firsttalk : MonoBehaviour
{
    public voidEventSO dialogueSO;
    public GameObject canvas;
    public GameObject blackcanva;
    private GameObject inventorycanva;
    public InputControls controls1;
    public Dictionary<string, Sprite> spritedic;
    public SpriteList spriteList;
    
        private GameObject player;
        [Multiline(20)]
    public string text1;
    // Start is called before the first frame update
    void Start()
    {
        // InventoryManager.instance.transform.gameObject.SetActive(false);
        canvas=dialogue.instance.transform.gameObject;
        blackcanva=GameObject.Find("black");
        //blackcanva.SetActive(false);
        player =GameObject.FindWithTag("Player");
        player.SetActive(false);
        talk();

    }
    void OnEnable(){
        
    }
    private void Awake()
    {
        spritedic = new Dictionary<string, Sprite>();
        spriteList=UnityEditor.AssetDatabase.LoadAssetAtPath<SpriteList>("Assets/setting/SpriteList.asset");
        spritedic=spriteList.spritedic;
        inventorycanva=InventoryManager.instance.transform.gameObject;
        inventorycanva.SetActive(false);
        // image=GameObject.Find("black").GetComponent<Image>();
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
        // timer -= Time.deltaTime;
    }

public voidEventSO sceneloadoverSO; 
    private void talkover()
    {
       

        // InventoryManager.instance.addItem(prefab);
        
        dialogueSO.onEventRaised -= talkover;
        
        sceneloadoverSO.onEventRaised+=unload;
        
        
        Scenes.instance.LoadScenes("星空岭外");
        
        // Scenes.instance.LoadScenes("星空岭外");
        //InventoryManager.instance.gameObject.SetActive(true);


    }

    public void unload(){
        StartCoroutine(UnloadCurrentSceneAndLoadNew());
    }

    private IEnumerator UnloadCurrentSceneAndLoadNew()
    {
        
        // 获取当前活动场景
        Scene currentScene = SceneManager.GetActiveScene();

        // 异步加载新场景（叠加模式）
        // AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
        // yield return loadOperation;

        // 设置新场景为活动场景
        
        //Scene newScene = SceneManager.GetSceneByName("星空岭外");
        //SceneManager.SetActiveScene(newScene);

        // 异步卸载旧场景
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
        player.SetActive(true);
        inventorycanva.SetActive(true);
        player.gameObject.transform.position=new Vector3(-10,-3,0);

        sceneloadoverSO.onEventRaised-=unload;
        yield return unloadOperation;
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
