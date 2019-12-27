using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door
{
    [SerializeField]
    DoorPosition doorPosition = DoorPosition.North; // position of the door within the "from" room

    [SerializeField]
    Room from = null;

    [SerializeField]
    Room to = null;

    public DoorPosition DoorPosition { get => doorPosition; set => doorPosition = value; }
    public Room From { get => from; set => from = value; }
    public Room To { get => to; set => to = value; }
}
