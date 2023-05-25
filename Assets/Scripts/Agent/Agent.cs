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
    [Header("Obecny cel ruchu agenta")]
    [SerializeField]
    Vector3 currentTarget;
    [Header("Wlasnosci agenta")]
    [SerializeField]
    int health;
    [SerializeField]
    float velocity;
    public void SetAgentProperies(int health,float velocity)
    {
        this.health = health;
        this.velocity = velocity;
    }
    /// <summary>
    /// Funkcja tworzy nowego agenta z potrzebnymi komponentami i go zwraca
    /// </summary>
    public static Agent CreateAgent(AgentManager agentManager,Sprite sprite)
    {
        GameObject agentObject = new GameObject("Agent");
        Agent agent = agentObject.AddComponent<Agent>();
        agent.agentManager = agentManager;

        SpriteRenderer spriteRenderer = agentObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        agentObject.AddComponent<BoxCollider2D>();
        return agent;
    }
}
