using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Entered");
    }
}
