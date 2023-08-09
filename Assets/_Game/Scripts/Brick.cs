using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject brick;
    bool isCollect = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollect)
        {
            isCollect = true;
            other.GetComponent<Player>().AddBrick();
            brick.SetActive(false);
        }
    }
}
