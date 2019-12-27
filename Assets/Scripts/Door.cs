using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [SerializeField]
    DoorPosition fromPosition = DoorPosition.North; // position of the door within the "from" room

    [SerializeField]
    DoorPosition toPosition = DoorPosition.North; // position of the door within the "to" room

    [SerializeField]
    Room from = null;

    [SerializeField]
    Room to = null;

    public Room From { get => from; set => from = value; }
    public Room To { get => to; set => to = value; }
    public DoorPosition FromPosition { get => fromPosition; set => fromPosition = value; }
    public DoorPosition ToPosition { get => toPosition; set => toPosition = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            // Leave room
            GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().Transition(this);
        }
    }
}
