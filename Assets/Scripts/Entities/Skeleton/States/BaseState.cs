using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    abstract public void EnterState(EnemyStateController stateController);

    abstract public void ExitState(EnemyStateController stateController);

    abstract public void UpdateState(EnemyStateController stateController);
}
