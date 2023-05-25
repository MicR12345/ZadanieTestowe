using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Manager obsluguje tworzenie swoich agentow
//Zakladam ze moze byc wiecej managerow do agentow
//Chociaz nie jest to napisane w zadaniu
public class AgentManager : MonoBehaviour
{
    [Header("Mapa do ktorej przypisani beda agenci")]
    [SerializeReference]
    Map map;
    public Map Map
    {
        get { return map; }
    }
    [Header("Sprite agentow")]
    [SerializeField]
    Sprite agentSprite;
    [Header("Ilosci agentow")]
    [SerializeField]
    int maxAgentCount = 30;
    [SerializeField]
    [Tooltip("Obecna ilosc agengow")]
    int agentCount = 0;
    [SerializeField]
    [Range(3,5)]
    int startAgentCount = 3;
    [Header("Pojawianie sie agentow")]
    [SerializeField]
    float minTimeToSpawn = 2f;
    [SerializeField]
    float maxTimeToSpawn = 6f;
    [Header("Wlasciwosci agentow")]
    [SerializeField]
    float velocity = 3;
    [SerializeField]
    float health = 3;

    List<Agent> agentsUnderManagement = new List<Agent>();

    const int maxSpawnIterations = 30;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startAgentCount; i++)
        {
            CreateAgent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateAgent()
    {
        Agent newAgent = Agent.CreateAgent(this,agentSprite);
        newAgent.transform.parent = transform;
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
            newAgent.transform.position = Map.GetWorldPositionFromMapCoordinates(x,y);
            agentsUnderManagement.Add(newAgent);
            agentCount++;
        }
    }
}
