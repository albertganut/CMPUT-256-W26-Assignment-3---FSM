using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastCutOffState : State
{
    public FastCutOffState() : base("FastCutOff") { }

    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(8f);
        agent.SetSpeedModifierDouble();
    }

    public override void ExitState(FSMAgent agent)
    {
        agent.SetSpeedModifierNormal();
    }

    public override State Update(FSMAgent agent)
    {
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;

        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        if (agent.TimerComplete())
        {
            return new PelletChaseState();
        }

        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        agent.SetTarget(pacmanLocation + PacmanInfo.Instance.Facing * 0.8f);

        return this;
    }
}
