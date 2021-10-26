using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatsChart : MonoBehaviour
{
    [SerializeField]private Material radarMaterial;
    [SerializeField]private CanvasRenderer radarCanvasRender;

    [SerializeField]private float size;

    private float counter;

    private Mesh mesh;

    private Vector3[] vertices = new Vector3[6];
    private Vector2[] uv = new Vector2[6];
    private int[] triangles = new int[3 * 5];

    private float angle = 360 / 5;

    private void Awake()
    {
        mesh = new Mesh();
        counter = 0;
        //StartCoroutine(UpdateChart(1000,1000,100,100,10));
    }

    public IEnumerator UpdateChart(int ATK, int MAG, int DEF, int RES, float SPD) {
        if (counter == 0) {
            while (counter <= PauseMenu.buttonCD)
            {
                Vector3 atkVertex = Vector3.Lerp(vertices[1], Quaternion.Euler(0, 0, -angle * 0) * Vector3.up * size * (float)(ATK / 1000f), Mathf.Clamp(20f * Time.deltaTime, 0, 1));
                Vector3 defVertex = Vector3.Lerp(vertices[2], Quaternion.Euler(0, 0, -angle * 1) * Vector3.up * size * (float)(DEF / 500f), Mathf.Clamp(20f * Time.deltaTime, 0, 1));
                Vector3 resVertex = Vector3.Lerp(vertices[3], Quaternion.Euler(0, 0, -angle * 2) * Vector3.up * size * (float)(RES / 500f), Mathf.Clamp(20f * Time.deltaTime, 0, 1));
                Vector3 spdVertex = Vector3.Lerp(vertices[4], Quaternion.Euler(0, 0, -angle * 3) * Vector3.up * size * (float)(SPD / 100f), Mathf.Clamp(20f * Time.deltaTime, 0, 1));
                Vector3 magVertex = Vector3.Lerp(vertices[5], Quaternion.Euler(0, 0, -angle * 4) * Vector3.up * size * (float)(MAG / 1000f), Mathf.Clamp(20f * Time.deltaTime, 0, 1));

                vertices[0] = Vector3.zero;
                vertices[1] = atkVertex;
                vertices[2] = defVertex;
                vertices[3] = resVertex;
                vertices[4] = spdVertex;
                vertices[5] = magVertex;

                uv[0] = Vector2.zero;
                uv[1] = Vector2.one;
                uv[2] = Vector2.one;
                uv[3] = Vector2.one;
                uv[4] = Vector2.one;
                uv[5] = Vector2.one;

                triangles[0] = 0;
                triangles[1] = 1;
                triangles[2] = 2;

                triangles[3] = 0;
                triangles[4] = 2;
                triangles[5] = 3;

                triangles[6] = 0;
                triangles[7] = 3;
                triangles[8] = 4;

                triangles[9] = 0;
                triangles[10] = 4;
                triangles[11] = 5;

                triangles[12] = 0;
                triangles[13] = 5;
                triangles[14] = 1;

                mesh.vertices = vertices;
                mesh.uv = uv;
                mesh.triangles = triangles;

                radarCanvasRender.SetMesh(mesh);
                radarCanvasRender.SetMaterial(radarMaterial, null);
                counter += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            } 
            counter = 0;
        }
        yield break;
    }

}
