using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NpcInteraction : MonoBehaviour
{
    bool active;
    NpcInteractor initiator;
    Npc npc;

    [SerializeField]
    float interact_radius = 200.0f;

    [SerializeField]
    LayerMask npc_mask;

    public bool Active { get => active; }

    public Npc findNpc(Vector3 location) {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(location, interact_radius, npc_mask);
        for (int i = 0; i < nearby.Length; ++i) {
            Npc npc = nearby[i].gameObject.GetComponent<Npc>();
            if (npc != null) return npc;
        }
        return null;
    }

    public void StartInteraction(NpcInteractor initiator) {
        StartInteraction(initiator, findNpc(initiator.gameObject.transform.position));
    }

    public void StartInteraction(NpcInteractor initiator, Npc npc) {
        if (npc == null) return;
        if (!active) {
            active = true;
            initiator = initiator;
            npc = npc;
        }

        npc.ShowSpeechBubble();

        StopInteraction();
    }

    public void StopInteraction() {
        if (active) {
            active = false;
            initiator = null;
            npc = null;
        }
    }
}
