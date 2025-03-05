using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public voidEventSO sceneloadoverSO; 
    public static Scenes instance;
    public GameObject blackcanva;
    public GameObject player;
 
    private Black black;
    
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(instance);
        }
        
    //      blackcanva.SetActive(true);
        black=blackcanva.gameObject.GetComponent<Black>(); 
        
    }
    // public SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        
      
       LoadScenes("scene1");

        player.SetActive(false);
    }

    public void LoadScenes(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        black.StartFadeIn();
        yield return new WaitForSeconds(2);
        // 异步加载场景（使用场景名称而非路径）
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // 等待加载完成
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            
            yield return null;
        }
        sceneloadoverSO.RiaseEvent();
        // 获取并激活场景
         UnityEngine.SceneManagement.Scene newScene = SceneManager.GetSceneByName(sceneName);
        if (newScene.IsValid())
        {
            SceneManager.SetActiveScene(newScene);
            Debug.Log($"Active scene set to: {newScene.name}");
            black.StartFadeOut();

        }
        else
        {
            Debug.LogError($"Failed to load scene: {sceneName}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
