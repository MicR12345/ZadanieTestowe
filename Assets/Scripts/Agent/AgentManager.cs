using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Manager obsluguje tworzenie swoich agentow
//Zakladam ze moze byc wiecej managerow do agentow
//Chociaz nie jest to napisane w zadaniu
public class AgentManager : MonoBehaviour
{
    [Header("Mapa do ktorej przypisani beda agenci")]
    [SerializeField]
    Map map = null;
    public Map Map
    {
        get { return map; }
    }
    [Header("Sprite agentow")]
    [SerializeField]
    Sprite agentSprite = null;
    [Header("Ilosci agentow")]
    [SerializeField]
    int maxAgentCount = 30;
    [SerializeField]
    [Tooltip("Obecna ilosc agengow")]
    //Zrobilem ta zmienna tylko po to by byla widoczna w inspektorze
    int agentCount = 0;
    [SerializeField]
    [Range(3,5)]
    int startAgentCount = 3;
    [Header("Pojawianie sie agentow")]
    [SerializeField]
    float minTimeToSpawn = 2f;
    [SerializeField]
    float maxTimeToSpawn = 6f;
    [SerializeField]
    float spawnTimer = 0;
    [Header("Wlasciwosci agentow")]
    [SerializeField]
    float agentVelocity = 3;
    [SerializeField]
    int agentHealth = 3;
    [SerializeField]
    [Header("Reczne sterowanie agentami")]
    bool manualControl = false;

    List<Agent> agentsUnderManagement = new List<Agent>();

    const int maxSpawnIterations = 30;

    void Start()
    {
        for (int i = 0; i < startAgentCount; i++)
        {
            CreateAgent();
        }
        spawnTimer = Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    void Update()
    {
        if (spawnTimer <= 0)
        {
            if(agentsUnderManagement.Count < maxAgentCount)
            {
                CreateAgent();
            }
            //dodaje czas zamiast ustawiac by doliczyc czas do obecnej klatki
            spawnTimer = spawnTimer + Random.Range(minTimeToSpawn, maxTimeToSpawn);
        }
        else
        {
            spawnTimer = spawnTimer - Time.deltaTime;
        }
    }
    public void DestroyAgent(Agent agent)
    {
        map.UnregisterAgentFromMap(agent);
        agentsUnderManagement.Remove(agent);
        GameObject.Destroy(agent.gameObject);
        agentCount = agentsUnderManagement.Count;
    }
    void CreateAgent()
    {
        Agent newAgent = Agent.CreateAgent(this,Map,agentSprite);
        int x = Random.Range(0, Mathf.FloorToInt(Map.Size.x));
        int y = Random.Range(0, Mathf.FloorToInt(Map.Size.y));
        int i = 0;
        while (Map.CheckIfTileOccupied(x, y) && i < maxSpawnIterations)
        {
            x = Random.Range(0, Mathf.FloorToInt(Map.Size.x));
            y = Random.Range(0, Mathf.FloorToInt(Map.Size.y));
            i++;
        }
        if (i == maxSpawnIterations)
        {
            Debug.LogError("Couldnt spawn agent in " + maxSpawnIterations + " iterations");
            GameObject.Destroy(newAgent.gameObject);
        }
        else
        {
            newAgent.transform.parent = transform;
            newAgent.transform.position = Map.GetWorldPositionFromMapCoordinates(x,y);
            newAgent.SetAgentProperies(agentHealth, agentVelocity,manualControl);
            newAgent.AssignAgentBehaviour(PickAgentBehaviour());
            agentsUnderManagement.Add(newAgent);
            map.RegisterAgentOnMap(newAgent);
            agentCount++;
        }
    }
    AgentBehaviour PickAgentBehaviour()
    {
        int coin = Random.Range(0, 2);
        if (coin==0)
        {
            return new AgentBehaviours.RandomBehaviour();
        }
        else
        {
            return new AgentBehaviours.ChaseRandomOther();
        }
    }
}
