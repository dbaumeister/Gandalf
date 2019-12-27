using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Boundaries : MonoBehaviour
{
    BoxCollider2D coll;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    public Vector3 ClampPositionToCollider(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, coll.bounds.min.x, coll.bounds.max.x);
        position.y = Mathf.Clamp(position.y, coll.bounds.min.y, coll.bounds.max.y);
        return position;
    }

    public static Vector3 ClampPosition(Vector3 position)
    {
        GameObject boundaries = GameObject.FindWithTag("Boundaries");
        if(boundaries)
        {
            return boundaries.GetComponent<Boundaries>().ClampPositionToCollider(position);
        }
        return position;
    }
}
