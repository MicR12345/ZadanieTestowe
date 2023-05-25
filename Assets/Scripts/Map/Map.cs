using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map : MonoBehaviour
{
    private Tile[,] tiles;
    [Header("Rozmiar planszy")]
    [SerializeField]
    int xSize;
    [SerializeField]
    int ySize;
    //Lista agentow na mapie
    //Jest po to by agenci bedacy na mapie "widzieli" innych
    //w celu np ruchu czy pojawiania sie
    List<Agent> agentsOnMap = new List<Agent>();

    const float tileOccupiedDistance = 1f;

    public Vector2 Size
    {
        get { return new Vector2(xSize, ySize); }
    }

    private void Awake()
    {
        GenerateMap();
    }

    ///<summary>Zebrane funkcje do utworzenia mapy </summary>
    private void GenerateMap()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        
        GenerateMapTiles(xSize, ySize);
        meshFilter.mesh = GenerateMapMesh(xSize, ySize);
        
        Camera.main.orthographicSize = (xSize * 0.5f) + 1;
    }

    ///<summary>Funkcja generujaca pola na ksztalt prostokata o wymiarach <param>xSize</param> na <param>ySize</param> </summary>
    private void GenerateMapTiles(int xSize, int ySize)
    {
        tiles = new Tile[xSize, ySize];
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                tiles[i, j] = new Tile(new Vector3(i, j) - new Vector3(xSize * 0.5f,ySize * 0.5f));
            }
        }
    }
    ///<summary>Funkcja generujaca mesh na podstawie tablicy pol</summary>
    private Mesh GenerateMapMesh(int xSize, int ySize)
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                (Vector3[], int[], Vector2[], Vector3[]) newMeshComps = tiles[i, j].CreateTileMesh(verts.Count);
                verts.AddRange(newMeshComps.Item1);
                tris.AddRange(newMeshComps.Item2);
                uvs.AddRange(newMeshComps.Item3);
                normals.AddRange(newMeshComps.Item4);
            }
        }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        return mesh;
    }
    public void RegisterAgentOnMap(Agent agent)
    {
        agentsOnMap.Add(agent);
    }
    public void UnregisterAgentFromMap(Agent agent)
    {
        agentsOnMap.Remove(agent);
    }
    public Vector3 GetWorldPositionFromMapCoordinates(int x,int y)
    {
        return tiles[x, y].Position + new Vector3(0.5f,0.5f);
    }
    public bool CheckIfTileOccupied(int x,int y)
    {
        Vector3 tilePosition = GetWorldPositionFromMapCoordinates(x,y);
        foreach (Agent agent in agentsOnMap)
        {
            if (Vector3.Distance(agent.Position,tilePosition)<=tileOccupiedDistance)
            {
                return true;
            }
        }
        return false;
    }
}
