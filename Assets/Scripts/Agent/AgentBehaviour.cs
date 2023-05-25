using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AgentBehaviour
{
    public Vector3 SetNextMoveTarget(Agent agent,Map map);
}

