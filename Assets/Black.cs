using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
public class Black : MonoBehaviour
{
    float timer;
     public AnimationCurve curve;
     public Image image;
     [Range(0.5f, 2f)]public float speed = 1f;
    void Awake()
    {
        image=GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        StartCoroutine(BlackStart());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    Color tmpColor;
    public IEnumerator BlackStart()
    {
        float timer = 0f;
        tmpColor = image.color;
        do
        {
            timer += Time.deltaTime;
            SetColorAlpha(curve.Evaluate(timer * speed));
            yield return null;
 
        } while (tmpColor.a > 0);
        //gameObject.SetActive(false);
    }

 void SetColorAlpha(float a)
    {
        tmpColor.a = a;
        image.color = tmpColor;
    }
}
