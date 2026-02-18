using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : FSMAgent
{

    void Start()
    {
        Initialize();//remove, this is testing
    }

    public override void Initialize()
    {
        currState = new PinkyState(); // instructions say that he doesn't give this, but he does 
        currState.EnterState(this);
    }
}