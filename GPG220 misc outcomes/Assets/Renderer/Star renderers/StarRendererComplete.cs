using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class StarRendererComplete : MonoBehaviour
{
    
    public byte[] backBuffer;
    public Renderer quadRenderer;
    public List<Star> stars;
    public Texture2D texture;
    public int xSize;
    public int ySize;
    public float slowDown;

    private void Start()
    {
        stars = new List<Star>();
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
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].position.z++;
            }
            Draw();
        }
        if (Input.GetMouseButtonDown(1)) stars = new List<Star>();
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].position.z++;
        }
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
        if (stars.Count < 80)
        {
            var j = Random.Range(5, 20);
            for (var i = 0; i < j; i++)
            {
                var temp = new Star
                {
                    colour = new Vector3Int(255, 255, 255),
                    position = new Vector3(Random.Range(0, xSize),
                        Random.Range(0, ySize), 1)
                };
                stars.Add(temp);
            }
        }
        
        CheckX();
    }

    private void CheckX()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            float viewX;
            //effect movement based on distance from centre and from camera (z)
            float temp = Vector3.Distance(new Vector3(stars[i].position.x ,0 ,0), new Vector3(xSize/2, 0, 0)) / slowDown;
            if (stars[i].position.x > xSize / 2)
            {
                viewX = stars[i].position.x + stars[i].position.z/8 * temp;
                
            }
            else
            {
                viewX = stars[i].position.x - stars[i].position.z/8 * temp;
            }

            float viewY;
            temp = Vector3.Distance(new Vector3(0, stars[i].position.y  ,0), new Vector3(0, ySize/2, 0)) / slowDown;
            if (stars[i].position.y > ySize / 2)
            {
                viewY = stars[i].position.y + stars[i].position.z/8 * temp;
            }
            else
            {
                viewY = stars[i].position.y - stars[i].position.z/8 * temp;
            }

            if (viewX >= xSize || viewX <= 0)
            {
                stars.Remove(stars[i]);
            }
            else
            {
                if (CheckY(i,(int)viewY)) PixelSet(stars[i],(int)viewX, (int)viewY);
            }
        }
    }

    private bool CheckY(int i,int viewY)
    {
        if (viewY >= ySize || viewY <= 0)
        {
            stars.Remove(stars[i]);
            return false;    
        }
        return true;
    }
    
    private void PixelSet(Star currentStar, int x, int y)
    {
        int temp = ((y - 1) * ySize + x) * 3;
        backBuffer[temp - 3] = (byte)currentStar.colour.x;
        backBuffer[temp - 2] = (byte)currentStar.colour.y;
        backBuffer[temp - 1] = (byte)currentStar.colour.z;
    }
}
