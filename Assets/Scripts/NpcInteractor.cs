using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NpcInteractor : MonoBehaviour
{
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;  // handle key-up
        NpcInteraction npci = GameObject.FindGameObjectWithTag("NpcInteraction").GetComponent<NpcInteraction>();
        if (!npci.Active) {
            npci.StartInteraction(this);
        }
        else {
            npci.StopInteraction();
        }
    }
}
