using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("Assets/Scene/scene1.unity",LoadSceneMode.Additive);
    }
    // public SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        
        
        UnityEngine.SceneManagement.Scene newScene = SceneManager.GetSceneByName("Scene1");
        SceneManager.SetActiveScene(newScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
