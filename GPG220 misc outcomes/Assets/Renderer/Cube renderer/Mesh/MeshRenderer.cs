using System.Collections.Generic;
using UnityEngine;

public class MeshRenderer : MonoBehaviour
{
    public byte[] backBuffer;
    public bool isRotating;
    public bool isScaling;
    public List<Mesh> meshs;
    public Renderer quadRenderer;
    public float rotation;
    public int scale;
    public float slowDown;
    public Texture2D texture;
    public int xSize;
    public int ySize;

    private void Start()
    {
        meshs = new List<Mesh>();
        backBuffer = new byte[xSize * ySize * 3];
        texture = new Texture2D(xSize, ySize, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Point; // This turns off blur for debugging
        quadRenderer.material.mainTexture = texture;
        for (var i = 0; i < backBuffer.Length; i++) backBuffer[i] = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //for (var i = 0; i < meshs.Count; i++) meshs[i].pos.z++;
            rotation += 0.01f;
            Draw();
        }

        if (Input.GetMouseButtonDown(1)) meshs = new List<Mesh>();
    }

    private void FixedUpdate()
    {
        //for (var i = 0; i < meshs.Count; i++) meshs[i].pos.z++;
        rotation += 0.01f;
        Draw();
    }

    public void Draw()
    {
        for (var i = 0; i < backBuffer.Length; i++) backBuffer[i] = 0;
        DrawStars();
        texture.LoadRawTextureData(backBuffer);
        texture.Apply(false);
    }

    public void DrawStars()
    {
        if (meshs.Count < 1)
        {
            var j = Random.Range(1, 1);
            for (var i = 0; i < j; i++)
            {
                var temp = new Mesh
                {
                    colour = new Vector3Int(255, 255, 255),
                    pos = new Vector3(Random.Range(0, xSize),
                        Random.Range(0, ySize), 1),
                    vertices = new Vector3[8]
                };
                temp.vertices[0] = new Vector3(1, 1, -0.5f);
                temp.vertices[1] = new Vector3(-1, 1, -0.5f);
                temp.vertices[2] = new Vector3(-1, -1, -0.5f);
                temp.vertices[3] = new Vector3(1, -1, -0.5f);
                temp.vertices[4] = new Vector3(1, 1, 0.5f);
                temp.vertices[5] = new Vector3(-1, 1, 0.5f);
                temp.vertices[6] = new Vector3(-1, -1, 0.5f);
                temp.vertices[7] = new Vector3(1, -1, 0.5f);
                meshs.Add(temp);
            }
        }

        CheckPos();
    }

    private void CheckPos()
    {
        for (var i = 0; i < meshs.Count; i++)
        {
            float viewX;
            float viewY;

            for (var j = 0; j < meshs[i].vertices.Length; j++)
            {
                if (meshs[i].vertices[j] == new Vector3(-1,-1,-1)) continue;
//                float temp = Vector3.Distance(new Vector3(meshs[i].pos.x ,0 ,0), new Vector3(xSize/2, 0, 0)) / slowDown;
//                if (meshs[i].pos.x > xSize / 2)
//                {
//                    viewX = meshs[i].vertices[j].x + meshs[i].pos.x + meshs[i].vertices[j].z/8 + meshs[i].pos.z/8* temp;
//                }
//                else
//               {
//                    viewX = meshs[i].vertices[j].x + meshs[i].pos.x - meshs[i].vertices[j].z/8 - meshs[i].pos.z/8* temp;
//                }
                viewX = meshs[i].vertices[j].x  + meshs[i].vertices[j].z;
//                temp = Vector3.Distance(new Vector3(0 ,meshs[i].pos.y ,0), new Vector3( 0, ySize/2,0)) / slowDown;
//                if (meshs[i].pos.y > ySize / 2)
//                {
//                    viewY = meshs[i].vertices[j].y + meshs[i].pos.y + meshs[i].vertices[j].z/8 + meshs[i].pos.z/8* temp;
//                }
//                else
//                {
//                    viewY = meshs[i].vertices[j].y + meshs[i].pos.y - meshs[i].vertices[j].z/8 - meshs[i].pos.z/8* temp;
//                }
                viewY = meshs[i].vertices[j].y + meshs[i].vertices[j].z;


                if (isScaling)
                {
                    Scale(meshs[i], viewX, viewY, j);
                }
                else
                {
                    if (isRotating)
                        Rotating(meshs[i], viewX, viewY, j);
                    else
                        checkValues(meshs[i], (int) viewX, (int) viewY, j);
                }
            }
        }
    }

    private void Scale(Mesh mesh, float x, float y, int j)
    {
        x = x * scale;
        y = y * scale;
        
        if (isRotating)
            Rotating(mesh, x, y, j);
        else
            checkValues(mesh, (int) x, (int) y, j);
    }

    private void Rotating(Mesh mesh, float x, float y, int j)
    {
        float newX = (Mathf.Cos(rotation) * x) + (-Mathf.Sin(rotation) * y);
        float newY = (Mathf.Sin(rotation) * x) + (Mathf.Cos(rotation) * y);
        x = newX;
        y = newY;
        checkValues(mesh, (int) x, (int) y,j);
    }

    private void checkValues(Mesh mesh, int x, int y, int j)
    {
        x += (int)mesh.pos.x;
        y += (int)mesh.pos.y;
        if (x >= xSize || x <= 0 || y >= ySize || y <= 0)
        {
        }
        else
        {
           PixelSet(mesh, x, y);
        }
    }

    private void PixelSet(Mesh currentMesh, int x, int y)
    {
        var temp = ((y - 1) * ySize + x) * 3;
        backBuffer[temp - 3] = (byte) currentMesh.colour.x;
        backBuffer[temp - 2] = (byte) currentMesh.colour.y;
        backBuffer[temp - 1] = (byte) currentMesh.colour.z;
    }
}