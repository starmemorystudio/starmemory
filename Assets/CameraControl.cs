using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindWithTag("Player");
        // GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }

    void OnEnable()
{
    
    
}

void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    var player = GameObject.FindWithTag("Player");
    GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
}

    // Update is called once per frame
    void Update()
    {
        GetComponent<CinemachineConfiner2D>().m_BoundingShape2D=GameObject.FindWithTag("bounds").GetComponent<PolygonCollider2D>();
        
    }
}
