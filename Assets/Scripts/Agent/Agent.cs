using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour,Damageable,Displayable
{
    [Header("Obecny cel ruchu agenta")]
    [SerializeField]
    Vector3 currentTarget;
    [Header("Rusz na Pole")]
    [SerializeField]
    Vector2 setCustomTarget = new Vector2(-1f,-1f);
    [SerializeField]
    bool useTargetFromInspector = false;
    [Header("Wlasnosci agenta")]
    [SerializeField]
    int health;
    [SerializeField]
    float velocity;
    [Header("Timer obrazen")]
    [SerializeField]
    float damageTimer = 0f;
    AgentManager agentManager;
    public Vector3 Position
    {
        get { return transform.position; }
    }
    AgentBehaviour behaviour;
    Map map;

    Rigidbody2D rigidbodyy;

    const int damageOnCollision = 1;
    const float immunityTime = 1f;

    private void Update()
    {
        if (useTargetFromInspector)
        {
            if (setCustomTarget.x<0 || setCustomTarget.y<0 || setCustomTarget.x>= map.Size.x || setCustomTarget.y >= map.Size.y)
            {
                rigidbodyy.velocity = Vector2.zero;
            }
            else
            {
                Vector3 target = map.GetWorldPositionFromMapCoordinates(Mathf.FloorToInt(setCustomTarget.x), Mathf.FloorToInt(setCustomTarget.y));
                if (Vector3.Distance(target, Position) <= 0.1f)
                {
                    rigidbodyy.velocity = Vector2.zero;
                    transform.position = target;
                }
                else
                {
                    rigidbodyy.velocity = Vector3.Normalize(target - Position) * velocity * Time.deltaTime;
                }
            }
        }
        else
        {
            if (Vector3.Distance(currentTarget, Position) <= 0.1f)
            {
                SetNewMoveTarget();
            }
            rigidbodyy.velocity = Vector3.Normalize(currentTarget - Position) * velocity * Time.deltaTime;
        }
        if (damageTimer > 0)
        {
            damageTimer = damageTimer - Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable other;
        bool isDamageable = collision.gameObject.TryGetComponent<Damageable>(out other);
        if (isDamageable)
        {
            other.TakeDamage(damageOnCollision);
            TakeDamage(damageOnCollision);
        }
    }

    public void SetAgentProperies(int health,float velocity,bool manual = false)
    {
        this.health = health;
        this.velocity = velocity;
        useTargetFromInspector = manual;
    }
    public void AssignAgentBehaviour(AgentBehaviour agentBehaviour)
    {
        behaviour = agentBehaviour;
        SetNewMoveTarget();
    }
    public void SetNewMoveTarget()
    {
        currentTarget = behaviour.SetNextMoveTarget(this, map);
    }
    public void TakeDamage(int amount)
    {
        if (damageTimer<=0)
        {
            health = health - amount;
            SetNewMoveTarget();
            damageTimer = immunityTime;
            if (health<=0)
            {
                agentManager.DestroyAgent(this);
            }
        }
    }
    public int GetHealth()
    {
        return health;
    }

    public string GetName()
    {
        return gameObject.name;
    }
    public Vector3 GetPosition()
    {
        return Position;
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
        agent.rigidbodyy = agentObject.AddComponent<Rigidbody2D>();

        agent.rigidbodyy.gravityScale = 0f;

        return agent;
    }
}
