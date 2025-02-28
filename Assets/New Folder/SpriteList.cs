using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SpriteList", menuName = "Scraptable Object/Sprite/SpriteList")]
public class SpriteList : SerializedScriptableObject
{
    // Start is called before the first frame update
    public Dictionary<string, Sprite> spritedic;
    // public List<>;

}
