using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPianoCondition : Conditional
{
    ControlPiano piano;

    public override void OnAwake()
    {
        piano = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().piano.GetComponent<ControlPiano>();
    }

    public override TaskStatus OnUpdate()
    {
        if (!piano.roto)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
