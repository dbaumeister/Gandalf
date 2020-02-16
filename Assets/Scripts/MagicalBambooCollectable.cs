using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBambooCollectable : Item
{
    [SerializeField]
    float timeStep;

    float pickUpTime;
    float nextTime;

    // Start is called before the first frame update
    //void Start()
    //{
    //    timeStep = 30f;
    //    pickUpTime = 0f;
    //    nextTime = 0f;
    //}

    public override Attributes Apply(Attributes other)
    {
        //It's time to heal
        if(Time.time >= nextTime)
        {
            if (GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().getMaxHearts() > GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().getHearts())
            {
                GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().addHearts(1);
            }
            nextTime = Time.time + timeStep;
        }
        return other;
    }

    public float TimeStep { get => timeStep; set => timeStep = value; }
    public float PickUpTime { get => pickUpTime; set => pickUpTime = value; }
    public float NextTime { get => nextTime; set => nextTime = value; }
}
