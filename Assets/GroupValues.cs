using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupValues : MonoBehaviour
{
    [SerializeField]
    int hearts;
    [SerializeField]
    int coins;

    [SerializeField]
    GameObject heartsPrefab;

    [SerializeField]
    float heartDistance = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int count = transform.childCount;
        for(int i = 0; i < count; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < hearts; ++i)
        {
            GameObject obj = Instantiate(heartsPrefab, transform);
            obj.transform.Translate(Vector3.right * heartDistance * i);
        }

        if (hearts <= 0)
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
