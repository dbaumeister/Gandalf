using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBambooCollectable : Item
{
    float timeGap;
    float pickUpTime;
    float nextTime;

    // Start is called before the first frame update
    void Start()
    {
        timeGap = 30f;
        pickUpTime = 0;
        nextTime = 0;
    }

    public override Attributes Apply(Attributes other)
    {
        //It's time to heal
        if(Time.time >= nextTime)
        {
            GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>.addHearts(1);
            nextTime = Time.time + timeGap;
        }
        return other;
    }

    public float PickUpTime { get => pickUpTime; set => pickUpTime = value; }
    public float nextTime { get => nextTime; set => nextTime = value; }
}
