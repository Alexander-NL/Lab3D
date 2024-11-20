using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableTableType {Enemy, Item}

public class Interactable : MonoBehaviour
{
    public Stat myActor {get; private set;}

    public InteractableTableType interactionType;
    public Mana M;

    void Awake(){
        if(interactionType == InteractableTableType.Enemy){
            myActor = GetComponent<Stat>();
        }
    }

    public void InteractWithItem(){
        M.currentMana = M.currentMana + 20;
        Destroy(gameObject);
    }
}
