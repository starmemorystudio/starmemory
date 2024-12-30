using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="so_ItemList",menuName ="Scraptable Object/Item/ItemList")]
public class SO_ItemList : ScriptableObject {
    // Start is called before the first frame update
    [SerializeField]
    public List<ItemDetails> itemDetails; 
    
}
