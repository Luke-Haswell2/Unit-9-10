using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    bool touching;

    public GameObject Speech;

    void Start()
    {
        touching = false;
    }

    void Update()
    {
        if (touching == true)
        {
            Speech.SetActive(true);
        }

        else
        {
            Speech.SetActive(false);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touching = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touching = false;
        }
    }
}
