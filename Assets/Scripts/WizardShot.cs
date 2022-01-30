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
    private PhotonView photonView;

    private Game_Manager gameManager;
    private Gem gem;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();

        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();

        rb.AddForce(launchAngle * velocity, ForceMode.VelocityChange);
        if(photonView.IsMine)
            StartCoroutine(DelayedDestroy(duration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "XR")
        {
            Wizard wizard = Wizard.GetMyWizard();
            Debug.Log(wizard.transform.parent.name + " hit!");

            bool isIce = false;

            if (wizard.gameObject.transform.parent.name.Contains("Ice"))
                isIce = true;

            // Gem
            if(gameManager.gemHeld)
            {
                GameObject xrOrigin = GameObject.Find("XR Origin");
                if (gem == null)
                    gem = GameObject.FindGameObjectWithTag("Gem").GetComponent<Gem>();

                if(gameManager.incIce == true) // Ice has it
                {
                    if(isIce == true)
                    {
                        // Drop the gem at ice
                        //gem.GetComponent<PhotonView>().RequestOwnership();
                        gem.DropGemAtPosition(xrOrigin.transform.position + (Vector3.up * 2.0f), true);
                        Debug.Log("Iceman is dropping that mf");

                    }
                    else
                    {
                        // its ok, fireman doesnt have the gem
                    }
                }
                else // Fire has it
                {
                    if(isIce == false)
                    {
                        // Drop the gem at fire
                        //gem.GetComponent<PhotonView>().RequestOwnership();
                        gem.DropGemAtPosition(xrOrigin.transform.position + (Vector3.up * 2.0f), false);
                        Debug.Log("Fireman is dropping that mf");
                    }
                    else
                    {
                        // its ok, iceman doesnt have the gem
                    }
                }
            }

            // Respawn
            wizard.MoveToSpawn(isIce);
        }

        if(photonView.IsMine)
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
