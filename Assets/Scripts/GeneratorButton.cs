using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneratorButton : Button
{
    public static GeneratorButton instance;
    public event Action GeneratorButtonEvent;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        SetButtonInteracted();
        if (GeneratorButtonEvent != null)
        {
            GeneratorButtonEvent();
            if (_isObjectiveType == true)
            {
                ObjectiveCompleted();
            }
        }
    }
}
