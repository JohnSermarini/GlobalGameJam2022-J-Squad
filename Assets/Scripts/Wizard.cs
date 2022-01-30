using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    protected string shotClassName = "WizardShot";
    protected Vector3 spawnPoint = Vector3.zero;
    protected Quaternion spawnRotation = Quaternion.identity;
    public GameObject WandCrystal;
    public Transform FireSpawn;
    public Transform IceSpawn;

    public PhotonView photonView;
    public GameObject head;
    public GameObject lefthand;
    public GameObject righthand;

    private GameObject xrOrigin;
    private bool aButtonHeld = false;
    private bool bButtonHeld = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //WandCrystal.SetActive(false);
        photonView = GetComponent<PhotonView>();
        xrOrigin = GameObject.Find("XR Origin");

        FireSpawn = GameObject.Find("FireWizardSpawnPoint").GetComponent<Transform>();
        IceSpawn = GameObject.Find("IceWizardSpawnPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(photonView.IsMine && Input.GetKeyDown(KeyCode.C))
        {
            CastSpell();
        }

        // Manage user input
        if(photonView.IsMine)
        {
            // A button on right hand
            bool aButtonDownThisFrame = AButtonPressed();
            if(aButtonDownThisFrame && !aButtonHeld) // A button held down first frame
            {
                Debug.Log("A-button pressed this frame!");
                aButtonHeld = true;
                CastSpell();
            }
            else if(aButtonDownThisFrame && aButtonHeld) // A button held down second frame and on
            {

            }
            else if(!aButtonDownThisFrame && aButtonHeld) // A button released
            {
                aButtonHeld = false;
            }

            // B button on right hand
            bool bButtonDownThisFrame = BButtonPressed();
            if(bButtonDownThisFrame && !bButtonHeld) // B button held down first frame
            {
                Debug.Log("B-button pressed this frame!");
                bButtonHeld = true;
                photonView.RPC("MoveToSpawn", RpcTarget.Others);
            }
            else if(bButtonDownThisFrame && bButtonHeld) // B button held down second frame and on
            {

            }
            else if(!bButtonDownThisFrame && bButtonHeld) // B button released
            {
                bButtonHeld = false;
            }
        }
    }

    protected bool AButtonPressed()
    {
        var controllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);

        foreach(var device in controllers)
        {
            bool buttonValue;
            if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out buttonValue) && buttonValue)
            {
                return true;
            }
        }

        return false;
    }

    protected bool BButtonPressed()
    {
        var controllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);

        foreach(var device in controllers)
        {
            bool buttonValue;
            if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out buttonValue) && buttonValue)
            {
                return true;
            }
        }

        return false;
    }

    protected void CastSpell()
    {
        // Shot parameters
        Vector3 shotSpawnPoint = righthand.transform.position + (righthand.transform.forward * 0.25f);
        // Create shot and direct it
        GameObject shot = PhotonNetwork.Instantiate(shotClassName, shotSpawnPoint, Quaternion.identity);
        shot.GetComponent<WizardShot>().SetLaunchParameters(shotSpawnPoint, righthand.transform.forward);
    }

    public void MoveToSpawn(bool isIce)
    {
        Debug.Log("MoveToSpawn called on " + transform.parent.name);
        if(isIce)
            xrOrigin.transform.position = IceSpawn.position;
        else
            xrOrigin.transform.position = FireSpawn.position;

        //transform.rotation = spawnRotation;
    }

    public static Wizard GetWizardUsingName(string wizardName)
    {
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
            Debug.LogError("ERROR: Wizard " + wizardName + " cannot drop Gem because it cannot be found! Shit!");
        }

        return wizard;
    }

    public static Wizard GetEnemyWizard()
    {
        Wizard wizard = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponentInChildren<PhotonView>() != null && !players[i].GetComponentInChildren<PhotonView>().IsMine)
            {
                wizard = players[i].GetComponentInChildren<Wizard>();
                return wizard;
            }
        }
        if(wizard == null)
        {
            Debug.LogError("ERROR: Enemy wizard not found!");
        }

        return null;
    }

    public static Wizard GetMyWizard()
    {
        Wizard wizard = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponentInChildren<PhotonView>() != null && players[i].GetComponentInChildren<PhotonView>().IsMine)
            {
                wizard = players[i].GetComponentInChildren<Wizard>();
                return wizard;
            }
        }
        if (wizard == null)
        {
            Debug.LogError("ERROR: My wizard not found!");
        }

        return null;
    }
}
