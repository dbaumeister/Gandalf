using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomCollectable : MonoBehaviour
{
    [SerializeField]
    public Projectile projectile;

    [SerializeField]
    Vector2[] directions;
    float defaultSpeed = 4;

    public void Split()
    {
        foreach (Vector2 dir in directions)
        {
            Projectile projec = Instantiate(projectile);
            projec.transform.position = transform.position;
            projec.MovementSpeed = defaultSpeed;
            projec.Direction = dir;
            projec.transform.localScale *= 4;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
