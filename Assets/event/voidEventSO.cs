using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/VoidEventSO")]
public class voidEventSO : ScriptableObject
{

    public UnityAction onEventRaised;
    public void RiaseEvent() {
        onEventRaised?.Invoke();
        
    }
   
    
    
}
