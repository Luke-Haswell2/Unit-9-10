using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask playerLayer;
    void Start()
    {
        
    }

    void Update()
    {
        if (Hit())
        {
            Destroy(gameObject);
        }
    }

    bool Hit()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, playerLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
