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
    [SerializeField]
    int heartsPerPlayer = 10;

    [SerializeField]
    int maxHearts = 10;

    public delegate void PlayersDied();
    public event PlayersDied OnPlayersDied;

    Vector3 heartLocation(int heartIndex) {
        return Vector3.right * (heartIndex * heartDistance);
    }

    void Awake() {
        setHearts(hearts);
    }

    //void Start()
    //{
    //    GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
    //    int numPlayer = allPlayers.Length;
    //    maxHearts = numPlayer * heartsPerPlayer;
    //}

    void setHearts(int hearts) {
        this.hearts = hearts;
        int count = transform.childCount;
        for (int i = 0; i < count; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < hearts; ++i) {
            GameObject obj = Instantiate(heartsPrefab, transform);
            obj.transform.Translate(heartLocation(i));
        }

        if (hearts <= 0) {
            if(OnPlayersDied != null) {
                OnPlayersDied();
            }

            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in allPlayers) {
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
        setHearts(this.hearts + hearts);
    } 
    public void takeHearts(int hearts)
    {
        setHearts(this.hearts - hearts);
    }
    public int getHeartsPerPlayer()
    {
        return this.heartsPerPlayer;
    }
    public int getMaxHearts()
    {
        return this.maxHearts;
    }
}
