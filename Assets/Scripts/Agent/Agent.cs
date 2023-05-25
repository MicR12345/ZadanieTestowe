using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [Header("Obecny cel ruchu agenta")]
    [SerializeField]
    Vector3 currentTarget;
    [Header("Wlasnosci agenta")]
    [SerializeField]
    int health;
    [SerializeField]
    float velocity;

    AgentManager agentManager;
    public Vector3 Position
    {
        get { return transform.position; }
    }
    AgentBehaviour behaviour;
    Map map;

    Rigidbody2D rigidbody;

    private void Update()
    {
        rigidbody.velocity = Vector3.Normalize(currentTarget - Position) * velocity * Time.deltaTime;
    }

    public void SetAgentProperies(int health,float velocity)
    {
        this.health = health;
        this.velocity = velocity;
    }
    public void AssignAgentBehaviour(AgentBehaviour agentBehaviour)
    {
        behaviour = agentBehaviour;
        currentTarget = behaviour.SetNextMoveTarget(this,map);
    }
    /// <summary>
    /// Funkcja tworzy nowego agenta z potrzebnymi komponentami i go zwraca
    /// </summary>
    public static Agent CreateAgent(AgentManager agentManager,Map map,Sprite sprite)
    {
        GameObject agentObject = new GameObject("Agent");
        Agent agent = agentObject.AddComponent<Agent>();
        agent.agentManager = agentManager;
        agent.map = map;
        SpriteRenderer spriteRenderer = agentObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        agentObject.AddComponent<CircleCollider2D>();
        agent.rigidbody = agentObject.AddComponent<Rigidbody2D>();

        agent.rigidbody.gravityScale = 0f;

        return agent;
    }
}
