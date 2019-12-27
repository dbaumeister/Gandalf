using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
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

    [SerializeField]
    Sprite closedSprite;

    [SerializeField]
    Sprite openedSprite;

    [SerializeField]
    bool isOpen = false;

    [SerializeField]
    bool isHidden = true;

    public Room From { get => from; set => from = value; }
    public Room To { get => to; set => to = value; }
    public DoorPosition FromPosition { get => fromPosition; set => fromPosition = value; }
    public DoorPosition ToPosition { get => toPosition; set => toPosition = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isHidden && isOpen && collision.collider.tag == "Player")
        {
            // Leave room
            GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().Transition(this);
        }
    }

    public void Show()
    {
        GetComponent<SpriteRenderer>().sprite = closedSprite;
        isHidden = false;
    }

    public void Open()
    {
        if (isHidden)
            return;

        isOpen = true;
        GetComponent<SpriteRenderer>().sprite = openedSprite;
    }

    public void Close()
    {
        if (isHidden)
            return;

        isOpen = false;
        GetComponent<SpriteRenderer>().sprite = closedSprite;
    }
}
