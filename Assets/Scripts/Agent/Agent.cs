using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    AgentManager agentManager;
    public Vector3 Position
    {
        get { return transform.position; }
    }
    AgentBehaviour behaviour;

    Vector3 currentTarget;

    public static Agent CreateAgent(AgentManager agentManager,Sprite sprite)
    {
        GameObject agentObject = new GameObject("Agent");
        Agent agent = agentObject.AddComponent<Agent>();
        agent.agentManager = agentManager;

        SpriteRenderer spriteRenderer = agentObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        return agent;
    }
}
