using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/ItemEventSO")]
public class ItemEventSO : ScriptableObject
{

    public UnityAction<Item> onEventRaised;
    public void RiaseEvent(Item item)
    {
        onEventRaised?.Invoke(item);

    }



}
