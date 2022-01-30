using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public Text WinnerText;
    public bool gemHeld = false;
    public bool incFire = false;
    public bool incIce = false;
    public int Timer;
    public int FirePotatoTimer = 0;
    public int IcePotatoTimer = 0;
    public int victoryCondition = 3600;

    public Image IceBillboard;
    public Image FireBillboard;
    public GameObject Fireworks;

    public PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Fireworks = GameObject.Find("Fireworks");
        Fireworks.SetActive(false);
    }

    private void Update()
    {
        if (gemHeld && string.IsNullOrEmpty(WinnerText.text))
        {
            if (incIce == true)
            {
                IcePotatoTimer = IcePotatoTimer + 1;
                IceBillboard.fillAmount = (float)IcePotatoTimer / (float)victoryCondition;
                if (Mathf.Approximately(IceBillboard.fillAmount, 1f))
                {
                    WinnerText.text = "Ice Wizard Wins!";
                    WinnerText.gameObject.SetActive(true);
                    Fireworks.SetActive(true);
                    StartCoroutine(ApplicationQuit());
                }
            }
            else if (incFire == true)
            {
                FirePotatoTimer = FirePotatoTimer + 1;
                FireBillboard.fillAmount = (float)FirePotatoTimer / (float)victoryCondition;
                if (Mathf.Approximately(FireBillboard.fillAmount, 1f))
                {
                    WinnerText.text = "Fire Wizard Wins!";
                    WinnerText.gameObject.SetActive(true);
                    Fireworks.SetActive(true);
                    StartCoroutine(ApplicationQuit());
                }
            }
        }
    }

    public int QuitTimer = 15;
    public IEnumerator ApplicationQuit() 
    {
        while (QuitTimer != 0)
        {
            yield return new WaitForSeconds(1f);
            QuitTimer--;
        }
        Application.Quit();
    }

    [PunRPC]
    private void GemGrabbed(string wizardName)
    {
        Wizard wizard = Wizard.GetWizardUsingName(wizardName);
        wizard.WandCrystal.SetActive(true);
        gemHeld = true;
        incIce = false;
        incFire = false;
        if (wizard is IceWizard)
        {
            incIce = true;
        }
        else if (wizard is FireWizard)
        {
            incFire = true;
        }
    }

    [PunRPC]
    private void GemDropped(bool isIce)
    {
        Debug.Log("GemDropped RPC recieved");
        Wizard wizard;
        if (isIce)
            wizard = GameObject.FindObjectOfType<IceWizard>();
        else
            wizard = GameObject.FindObjectOfType<FireWizard>();

        wizard.WandCrystal.SetActive(false);
        gemHeld = false;
        incFire = false;
        incIce = false;
    }
}
