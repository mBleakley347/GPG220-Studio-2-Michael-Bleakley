using UnityEngine;

public class RenderScript : MonoBehaviour
{
    public byte[] backBuffer;
    private bool first = true;
    private int j;
    public int numberStars;
    public Renderer quadRenderer;
    public Texture2D texture;
    public Star[] twinkle;
    private int xSelect;
    public int xSize;
    private int ySelect;
    public int ySize;


    private void Start()
    {
        backBuffer = new byte[xSize * ySize * 3];
        twinkle = new Star[numberStars];
        texture = new Texture2D(xSize, ySize, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Point; // This turns off blur for debugging
        quadRenderer.material.mainTexture = texture;

        for (var i = 0; i < backBuffer.Length; i++)
        {
            if (j == 0)
                backBuffer[i] = 70;
            else if (j == -1)
                backBuffer[i] = 130;
            else
                j = -2;

            j++;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Draw();
    }

    private void FixedUpdate()
    {
        Draw();
    }

    public void ButtonPressed(int num)
    {
        if (first)
        {
            ySelect = num;
            first = !first;
        }
        else
        {
            xSelect = num;
            first = !first;
        }
    }

    [ContextMenu("Draw")]
    public void Draw()
    {
        texture.LoadRawTextureData(backBuffer);
        DrawStars();
        texture.Apply(false);
    }

    public void DrawStars()
    {
        for (var i = 0; i < numberStars; i++)
        {
            if (twinkle[i] == null)
            {
                var temp = new Star
                {
                    colour = new Vector3Int(255, 255, 255),
                    position = new Vector3Int((int) Random.Range(xSize * 0.3f, xSize * 0.7f),
                        (int) Random.Range(ySize * 0.3f, ySize * 0.7f), 0)

                };
                twinkle[i] = temp;
                texture.SetPixel((int)twinkle[i].position.x, (int)twinkle[i].position.y,
                    new Color(twinkle[i].colour.x, twinkle[i].colour.y, twinkle[i].colour.z));
            }
            else
            {
                if (twinkle[i].position.x > xSize / 2)
                {
                    if (twinkle[i].position.x + 2 > xSize)
                    {
                        twinkle[i] = null;
                    }
                    else
                    {
                        twinkle[i].position.x++;
                        CheckY(i);
                    }
                }
                else
                {
                    if (twinkle[i].position.x - 1 < 0)
                    {
                        twinkle[i] = null;
                    }
                    else
                    {
                        twinkle[i].position.x--;
                        CheckY(i);
                    }
                }
            }
        }
    }

    private void CheckY(int i)
    {
        if (twinkle[i].position.y > ySize / 2)
        {
            if (twinkle[i].position.y + 2 > ySize)
            {
                twinkle[i] = null;
            }
            else
            {
                twinkle[i].position.y++;
                texture.SetPixel((int)twinkle[i].position.x, (int)twinkle[i].position.y,
                    new Color(twinkle[i].colour.x, twinkle[i].colour.y, twinkle[i].colour.z));
            }
        }
        else
        {
            if (twinkle[i].position.y - 1 < 0)
            {
                twinkle[i] = null;
            }
            else
            {
                twinkle[i].position.y--;
                texture.SetPixel((int)twinkle[i].position.x, (int)twinkle[i].position.y,
                    new Color(twinkle[i].colour.x, twinkle[i].colour.y, twinkle[i].colour.z));
            }
        }
    }


    //int temp = ((twinkle.position.y - 1) * 4 + twinkle.position.x) * 3;

    //backBuffer[temp-2] = (byte) twinkle.colour.y;
    //backBuffer[temp-1] = (byte) twinkle.colour.z;

    public void ChangeColour(int colour)
    {
        switch (colour)
        {
            case 1:
                //white
                SetColour(255, 255, 255);
                break;
            case 2:
                //purple
                SetColour(70, 0, 130);
                break;
            case 3:
                //black
                SetColour(0, 0, 0);
                break;
        }
    }

    private void SetColour(byte set1, byte set2, byte set3)
    {
        var temp = ((ySelect - 1) * 4 + xSelect) * 3;
        backBuffer[temp - 3] = set1;
        backBuffer[temp - 2] = set2;
        backBuffer[temp - 1] = set3;
    }
}