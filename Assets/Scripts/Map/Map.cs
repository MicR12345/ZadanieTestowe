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
    private void Start()
    {
        GenerateMap();
    }
    private void GenerateMap()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        
        GenerateMapTiles(xSize, ySize);
        meshFilter.mesh = GenerateMapMesh(xSize, ySize);
    }

    ///<summary>Funkcja generujaca pola na ksztalt prostokata o wymiarach <c>xSize</c> na <c>ySize</c> </summary>
    private void GenerateMapTiles(int xSize, int ySize)
    {
        tiles = new Tile[xSize, ySize];
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                tiles[i, j] = new Tile(new Vector3(i, j) - new Vector3(xSize/2,ySize/2));
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
}
