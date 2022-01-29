using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShot : MonoBehaviour
{
    //public NetworkPlayer Wizard;
    public float velocity = 5.0f;
    public float duration = 5.0f;
    private Vector3 launchAngle = Vector3.zero;

    private SphereCollider collider;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();

        rb.AddForce(launchAngle * velocity, ForceMode.VelocityChange);
        StartCoroutine(DelayedDestroy(duration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " hit!! Pain!! Ow!!");
        PhotonNetwork.Destroy(this.gameObject);
    }

    public void SetLaunchParameters(Vector3 launchPoint, Vector3 launchRotation)
    {
        transform.position = launchPoint;
        launchAngle = launchRotation;
    }

    IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PhotonNetwork.Destroy(this.gameObject);
    }
}