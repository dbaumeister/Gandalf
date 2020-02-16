using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLook : MonoBehaviour
{
    public Vector2 oldPos = Vector2.zero;

    public bool isFlipped = false;

    // Update is called once per frame
    public void LookInMoveDirection()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        Vector2 pos = transform.position;
        Vector2 lookDirection = pos - oldPos;
        if (lookDirection.x < 0 && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(lookDirection.x > 0 && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

        oldPos = pos;
    }
}
