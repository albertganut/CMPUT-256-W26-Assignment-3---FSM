using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : FSMAgent
{

    void Start()
    {
        Initialize();//remove, this is testing
    }

    public override void Initialize()
    {
        currState = new ClydeState(); // need to change this because this is Blinky's state (need to change Clyde.cs to use a different state)
        currState.EnterState(this);
    }
}
