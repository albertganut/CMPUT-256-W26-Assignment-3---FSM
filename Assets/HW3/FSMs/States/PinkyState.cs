using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this overrides three functions: EnterState, ExitState, and Update from the base State class
public class PinkyState : State
{

    public PinkyState() : base("Pinky") { }

    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(20f);
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
            return new ScatterState(new Vector3(-1 * ObstacleHandler.Instance.XBound, ObstacleHandler.Instance.YBound), this);
        }

        //If Pacman ate a power pellet, go to Frightened State
        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }
        //If we didn't return follow Pacman
        // Each grid cell is 0.2. Thus, 4  grid cells in front of pacman is -> 0.2 x 4 = 0.8f
        agent.SetTarget(pacmanLocation + PacmanInfo.Instance.Facing * 0.8f);

        //Stay in this state
        return this;
    }
}
