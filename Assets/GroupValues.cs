using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupValues : MonoBehaviour
{
    [SerializeField]
    int hearts;
    [SerializeField]
    int coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hearts <= 0)
        {
            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in allPlayers)
            {
                Destroy(player);
            }
        }
    }
    public int getHearts()
    {
        return this.hearts;

    }
    public void addHearts(int hearts)
    {
        this.hearts += hearts;
    } 
    public void takeHearts(int hearts)
    {
        this.hearts -= hearts;
       
    }
}
