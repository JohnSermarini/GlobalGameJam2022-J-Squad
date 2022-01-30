using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public bool gemHeld = false;
    public bool incFire = false;
    public bool incIce = false;
    public int Timer;
    public int FirePotatoTimer = 0;
    public int IcePotatoTimer = 0;
    public int victoryCondition = 2000;

    public PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(gemHeld)
        {
            if(incIce == true)
            {
                IcePotatoTimer = IcePotatoTimer + 1;
                //Debug.Log("Ice count: " + IcePotatoTimer);
            }
            else if(incFire == true)
            {
                FirePotatoTimer = FirePotatoTimer + 1;
                //Debug.Log("Fire count: " + FirePotatoTimer);
            }
        }
    }

    [PunRPC]
    private void GemGrabbed(string wizardName)
    {
        /*
        // Get wizard object from name
        Wizard wizard = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
           if(players[i].name == wizardName)
            {
                wizard = players[i].GetComponentInChildren<Wizard>();
            }
        }
        if(wizard == null)
        {
            Debug.LogError("ERROR: Wizard " + wizardName + " cannot grab Gem because it cannot be found! Shit!");
        }
        */
        Wizard wizard = Wizard.GetWizardUsingName(wizardName);

        gemHeld = true;
        incIce = false;
        incFire = false;
        if(wizard is IceWizard)
        {
            incIce = true;
        }
        else if(wizard is FireWizard)
        {
            incFire = true;
        }
    }

    [PunRPC]
    private void GemDropped()
    {
        Debug.Log("GemDropped RPC recieved");

        gemHeld = false;
        incFire = false;
        incIce = false;
    }
}
