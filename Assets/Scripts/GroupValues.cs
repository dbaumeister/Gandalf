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
    uint heartRowLength = 60;

    public delegate void PlayersDied();
    public event PlayersDied OnPlayersDied;

    List<GameObject> heartObjects;

    Vector3 heartLocation(int heartIndex) {
        return Vector3.right * (heartIndex % heartRowLength * heartDistance) + Vector3.down * (heartIndex / heartRowLength * heartDistance);
    }

    void Awake() {
        heartObjects = new List<GameObject>();
        setHearts(hearts);
    }

    void setHearts(int hearts) {
        this.hearts = hearts;

        for (int i=hearts; i < heartObjects.Count; ++i) {
            heartObjects.RemoveAt(hearts);
        }
        for (int i=heartObjects.Count; i < hearts; ++i) {
            GameObject obj = Instantiate(heartsPrefab, transform);
            obj.transform.Translate(heartLocation(i));
            heartObjects.Add(obj);
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
}
