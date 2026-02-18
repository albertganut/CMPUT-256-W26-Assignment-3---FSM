using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletChaseState : State
{
    public PelletChaseState() : base("PelletChase") { }

    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(8f);
        agent.SetSpeedModifierDouble();
    }

    public override void ExitState(FSMAgent agent)
    {
        base.ExitState(agent);
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
            return new FastChaseState();
        }

        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        // target the closest pellet to Pacman's current position
        Pellet closest = PelletHandler.Instance.GetClosestPellet(pacmanLocation);
        if (closest != null)
        {
            agent.SetTarget(closest.transform.position); // gets the position of the closest pellet and sets it as the target
        }
        else
        {
            agent.SetTarget(pacmanLocation); // if there are no pellets, target Pacman directly
        }
        return this;
    }
}
