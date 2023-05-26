using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AgentBehaviour
{
    public Vector3 SetNextMoveTarget(Agent agent,Map map);
}
namespace AgentBehaviours
{
    public class RandomBehaviour : AgentBehaviour
    {
        Vector3 AgentBehaviour.SetNextMoveTarget(Agent agent, Map map)
        {
            int x = Random.Range(0, Mathf.FloorToInt(map.Size.x));
            int y = Random.Range(0, Mathf.FloorToInt(map.Size.y));
            return map.GetWorldPositionFromMapCoordinates(x, y);
        }
    }
    public class ChaseRandomOther : AgentBehaviour
    {
        Vector3 AgentBehaviour.SetNextMoveTarget(Agent agent, Map map)
        {
            Agent target = map.GetRandomOtherAgent(agent);
            if (target!=null)
            {
                return target.Position;
            }
            else
            {
                int x = Random.Range(0, Mathf.FloorToInt(map.Size.x));
                int y = Random.Range(0, Mathf.FloorToInt(map.Size.y));
                return map.GetWorldPositionFromMapCoordinates(x, y);
            }
        }
    }
}
