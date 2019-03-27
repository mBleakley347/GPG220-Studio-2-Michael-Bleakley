using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class StarRenderer : MonoBehaviour
{
    public byte[] backBuffer;
    public Renderer quadRenderer;
    public List<Star> stars;
    public Texture2D texture;
    public int xSize;
    public int ySize;

    private void Start()
    {
        stars = new List<Star>();
        backBuffer = new byte[xSize * ySize * 3];
        texture = new Texture2D(xSize, ySize, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Point; // This turns off blur for debugging
        quadRenderer.material.mainTexture = texture;
        for (var i = 0; i < backBuffer.Length; i++) backBuffer[i] = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Draw();
    }

    private void FixedUpdate()
    {
        Draw();
    }

    public void Draw()
    {
        foreach (var VARIABLE in backBuffer)
        {
            backBuffer[VARIABLE] = 0;
        }
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
                Debug.Log(stars.Count);
            var temp = new Star
            {
                colour = new Vector3Int(255, 255, 255),
                position = new Vector3(Random.Range(0, xSize),
                    Random.Range(0, ySize), 0)
            };
            stars.Add(temp);
        }

        for (var i = 0; i < stars.Count; i++)
        {
            stars[i].position.z++;
            if (stars[i].position.x > xSize)
                stars.Remove(stars[i]);
            if (stars[i].position.x < 0)
                stars.Remove(stars[i]);
            CheckY(i);
            /* archived code incorrect positioning
            if (stars[i].position.x > xSize / 2)
            {
                stars[i].position.x += stars[i].position.z / 6;
                //stars[i].position.x += (int)(Vector2.Distance(new Vector2(stars[i].position.x,stars[i].position.y), new Vector2(xSize/2,ySize/2)) / 30);
                //stars[i].position.x++;
                if (stars[i].position.x > xSize)
                    stars.Remove(stars[i]);
                else
                    CheckY(i);
            }
            else
            {
                stars[i].position.x -= stars[i].position.z / 6;
                //stars[i].position.x -= (int)(Vector2.Distance(new Vector2(stars[i].position.x,stars[i].position.y), new Vector2(xSize/2,ySize/2)) / 30);
                //stars[i].position.x--;
                if (stars[i].position.x < 0)
                    stars.Remove(stars[i]);
                else
                    CheckY(i);
            }
            */
        }
    }

    private void CheckY(int i)
    {
        if (stars[i].position.y > ySize)
        {
            stars.Remove(stars[i]);
        }
        else if (stars[i].position.y < 0)
        {
            stars.Remove(stars[i]);
        }
        else
        {
            PixelSet(stars[i]);
        }      
        
        
        
        /*archived code incorrect positioning
        if (stars[i].position.y > ySize / 2)
        {
            stars[i].position.y += stars[i].position.z / 8;
            //stars[i].position.y += (int)(Vector2.Distance(new Vector2(stars[i].position.x,stars[i].position.y), new Vector2(xSize/2,ySize/2)) / 40);
            //stars[i].position.y++;
            if (stars[i].position.y > ySize)
                stars.Remove(stars[i]);
            else
                texture.SetPixel((int) stars[i].position.x, (int) stars[i].position.y,
                    new Color(stars[i].colour.x, stars[i].colour.y, stars[i].colour.z));
        }
        else
        {
            stars[i].position.y -= stars[i].position.z / 8;
            //stars[i].position.y -= (int)(Vector2.Distance(new Vector2(stars[i].position.x,stars[i].position.y), new Vector2(xSize/2,ySize/2)) / 40);
            //stars[i].position.y--;
            if (stars[i].position.y < 0)
                stars.Remove(stars[i]);
            else
                texture.SetPixel((int) stars[i].position.x, (int) stars[i].position.y,
                    new Color(stars[i].colour.x, stars[i].colour.y, stars[i].colour.z));
        }
        */
    }

    private void PixelSet(Star currentStar)
    {
        int temp = (int)((currentStar.position.y - 1) * ySize + currentStar.position.x) * 3;
        backBuffer[temp - 3] = (byte)currentStar.colour.x;
        backBuffer[temp - 2] = (byte)currentStar.colour.y;
        backBuffer[temp - 1] = (byte)currentStar.colour.z;
    }
}