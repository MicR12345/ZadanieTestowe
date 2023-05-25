using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Tile[,] tiles;
    ///<summary>Funkcja generujaca mesh na podstawie tablicy pol</summary>
    public Mesh GenerateMapMesh(int xSize, int ySize,Vector3 position)
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
