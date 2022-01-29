using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizard : Wizard
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        shotClassName = "FireShot";
        spawnPoint = GameObject.Find("FireWizardSpawnPoint").transform.position;
        spawnRotation = GameObject.Find("FireWizardSpawnPoint").transform.rotation;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
