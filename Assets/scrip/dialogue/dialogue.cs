using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEditor;

public class dialogue : MonoBehaviour
{
    public static dialogue instance;
    public string[] list;

    public voidEventSO dialogueSO;

    public Image image;
    private InputControls inputControl;
    private PlayerController playerController;
    public GameObject player;
    public TextMeshProUGUI npcNameTMP;
    private string nameId;
    private bool autoClose;
    public string[] sentences;
    public int page=-1;
    public TextMeshProUGUI  conversationTMP;
    private SpriteRenderer keyIcon;
    public UnityAction<GameObject> onDialohue;
    public Dictionary<string, Sprite>  sprites;


    public bool Isonabled1;
    public void RaiseEvent(GameObject go) {
        
     }
    private void OnEnable()
    {
        inputControl.Enable();
        

    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(instance);
        }



        playerController =  player.GetComponent<PlayerController>();
        inputControl = new InputControls();
        
        
        //image = GetComponentInChildren<Image>();
        //npcNameTMP = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        npcNameTMP.text = "1";
        //conversationTMP = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        inputControl.UI.Fire.started += NextPage;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void SetCoversation(Dictionary<string, Sprite> npcSprite, string stringId, bool autoClose) {

        playerController.pause();

        sprites = npcSprite;
        string conversation = stringId.Replace(" ", "\n");
        sentences = conversation.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        page = 0;
        list = (sentences[page]).Split(new[] { ":", "：" }, StringSplitOptions.None);
        Debug.Log(list);
        conversationTMP.text = list[1];
        string nametext = list[0];
        if(nametext.Contains("[")){
            nametext=nametext.Remove(list[0].IndexOf("["));
        }
        npcNameTMP.text=nametext;
        Debug.Log(nametext);
        if (list[0] != "") {
        
        image.sprite = sprites[list[0]];
    }else {
                npcNameTMP.text =" ";
                //image.sprite=null;
                 image.transform.gameObject.SetActive(false);
                 
            }
        this.autoClose = autoClose;

        //if (sentences.Length > 1 || autoClose)
            //keyIcon.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    // Update is called once per frame
    public void SetCoversation( string stringId, bool autoClose)
    {

        playerController.pause();
        sprites  = ((SpriteList)AssetDatabase.LoadAssetAtPath<ScriptableObject>("Assets/setting/SpriteList.asset")).spritedic;
        
        string conversation = stringId.Replace(" ", "\n");
        sentences = conversation.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        page = 0;
        list = (sentences[page]).Split(new[] { ":", "：" }, StringSplitOptions.None);
        conversationTMP.text = list[1];
        npcNameTMP.text = list[0];
        string nametext = list[0];
        Debug.Log(1);
        if(nametext.Contains("[")){
            nametext=nametext.Remove(list[0].IndexOf("["));
        }
        npcNameTMP.text=nametext;

        if (list[0] != "")
        {

            image.sprite = sprites[list[0]];
        }else {
                npcNameTMP.text =" ";
                //image.sprite=null;
                 image.transform.gameObject.SetActive(false);
                 
            }
        this.autoClose = autoClose;

        //if (sentences.Length > 1 || autoClose)
        //keyIcon.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    public void NextPage(InputAction.CallbackContext context) {
        
        if (page < sentences.Length - 1)
        {
            page++;

            if (sentences[page].IndexOf(":")==-1 && sentences[page].IndexOf("：")==-1)
                sentences[page] =" :"+ sentences[page];
            list = (sentences[page]).Split(new[] { ":", "："}, StringSplitOptions.None);
            conversationTMP.text = list[1];

            if (list[0] != ""&&list[0] !=" ")
            {
                
                string nametext = list[0];
                if(nametext.Contains("[")){
                    nametext=nametext.Remove(list[0].IndexOf("["));
                    }
                 npcNameTMP.text=nametext;
                
                image.sprite = sprites[list[0]];
                image.transform.gameObject.SetActive(true);
            }
            else {
                npcNameTMP.text =" ";
                //image.sprite=null;
                 image.transform.gameObject.SetActive(false);
                 
            }
        }
        else
        {
            gameObject.SetActive(false);
            dialogueSO.onEventRaised?.Invoke();
            playerController.go();
        }
    }
    public void talk() {
        if (page < sentences.Length - 1||page==-1)
        {
            

        }
        
    }
    void Update()
    {
        //inputControl.UI.dialogue.started += NextPage;
        
    }
}
