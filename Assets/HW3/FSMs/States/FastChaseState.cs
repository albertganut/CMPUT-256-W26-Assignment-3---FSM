using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastChaseState : State
{
    public FastChaseState() : base("FastChase") { }

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
            return new FastCutOffState();
        }

        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        // target Pacman at double speed
        agent.SetTarget(pacmanLocation);
        return this;
    }
}
