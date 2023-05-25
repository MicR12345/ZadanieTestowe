using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    Vector3 position;
    public Tile(Vector3 position)
    {
        this.position = position;
    }

    ///<summary>Funkcja generujaca wszystko co potrzebne jest do zrenderowania jednego pola. <c>vertOffset</c> sluzy do przesunięcia wartosci w tablicy </summary>
    //Są to stałe dla każdego pola
    //W przypadku potrzeby większej ilości tekstur niż jedna wystarczy tu zmodyfikować
    //generowanie uv żeby brały z jakiegoś atlasu 
    public (Vector3[], int[], Vector2[], Vector3[]) CreateTileMesh(int vertOffset)
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = position + new Vector3(0, 0);
        vertices[1] = position + new Vector3(1f, 0);
        vertices[2] = position + new Vector3(0, 1f);
        vertices[3] = position + new Vector3(1f, 1f);
        int[] triangles = { 0 + vertOffset, 2 + vertOffset, 1 + vertOffset, 1 + vertOffset, 2 + vertOffset, 3 + vertOffset };
        Vector2[] uvs =
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0,1),
            new Vector2(1, 1),
        };
        Vector3[] normals =
        {
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back
    };
        return (vertices, triangles, uvs, normals);
    }
}
