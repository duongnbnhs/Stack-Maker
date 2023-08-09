using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Vector3.Distance(other.transform.position, transform.position) < 0.1f)
                LevelManager.Instance.OnFinish();
        }
    }
}
