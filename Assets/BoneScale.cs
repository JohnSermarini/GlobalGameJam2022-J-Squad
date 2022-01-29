using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScale : MonoBehaviour
{

    public Transform beginning;
    public Transform mid;
    public Transform end;

    float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(beginning.localPosition, end.localPosition);
        distance = distance / 9;
        mid.localPosition = new Vector3(mid.localPosition.x, distance, mid.localPosition.z);
        beginning.localPosition = new Vector3(mid.localPosition.x, distance, mid.localPosition.z);

    }
}
