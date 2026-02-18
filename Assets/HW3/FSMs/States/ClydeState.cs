using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeState : State
{

    private Vector3 scatterPos;

    public ClydeState() : base("Clyde") { }

    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(20f);
        scatterPos = new Vector3(-1 * ObstacleHandler.Instance.XBound, -1 * ObstacleHandler.Instance.YBound);
    }

    public override void ExitState(FSMAgent agent)
    {
        base.ExitState(agent);
    }

    public override State Update(FSMAgent agent)

    {
        //Handle Following Pacman
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;

        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        //If timer complete, go to Scatter  State
        if (agent.TimerComplete())
        {
            return new ScatterState(scatterPos, this);
        }

        //If Pacman ate a power pellet, go to Frightened State
        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        if (Vector3.Distance(agent.GetPosition(), pacmanLocation) <= 1.6f) // 0.2 x 8 grid cells = 1.6f 
        {
            agent.SetTarget(scatterPos);
        }
        else
        {
            agent.SetTarget(pacmanLocation);
        }

        //Stay in this state
        return this;
    }
}

