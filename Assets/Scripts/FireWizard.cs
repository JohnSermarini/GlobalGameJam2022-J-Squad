using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizard : Wizard
{
    protected override string ShotClassName { get => "FireShot"; set => shotClassName = value; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
