using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    // Start is called before the first frame update
    void Start()
    {
        SpawnNextTetramino();
    }

    //y = the row we are going to check
    public bool IsFullRowAt (int y)
    {
        for (int x = 0; x < gridWidth; ++x )
        {
            if(grid[x,y] == null)
            {
                return false;
            }
        }

        return true;
    }


    public void DeleteMinoAt (int y)
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x,y].gameObject);

            grid[x, y] = null;
        }
    }

    public void MoveRowDown(int y)
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            if(grid[x,y] != null)
            {
                
                grid[x, y-1] = grid[x, y];

                grid[x, y] = null;

                grid[x, y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowDown (int y)
    {
        for(int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

   public void DeleteRow()
    {
        for(int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowDown(y + 1);
                --y;
            }
        }
    }

    public void UpdateGrid(Tetramino tetramino)
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                if (grid[x, y] != null)
                {
                    //Check if parent transform is the tetramino transform
                    //that we pass to our UpdateGrid
                    if (grid[x, y].parent == tetramino.transform)
                    {
                        //for when it is moving down 1
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetramino.transform)
        {
            Vector2 pos = Round(mino.position);
            //Don't want to assign values to grid array the 
            //value that is above the grid (index out of bounds errors)
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPos(Vector2 pos)
    {
        if(pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    //Resources folder is instantiated at runtime
    public void SpawnNextTetramino()
    {
        GameObject nextTetraMino = (GameObject)Instantiate(Resources.Load(GetRandTetramino(), typeof(GameObject)),
                                    new Vector2(5.0f, 20.0f), Quaternion.identity);
        //Quaternion.identity = current rotation
    }

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandTetramino()
    {
        int randTetramino = Random.Range(1, 8);
        string randomTetraminoName = "Tetramino_T";

        switch (randTetramino)
        {
            case 1:
                randomTetraminoName = "Prefabs/Tetramino_T";
                break;
            case 2:
                randomTetraminoName = "Prefabs/Tetramino_Long";
                break;
            case 3:
                randomTetraminoName = "Prefabs/Tetramino_Square";
                break;
            case 4:
                randomTetraminoName = "Prefabs/Tetramino_L";
                break;
            case 5:
                randomTetraminoName = "Prefabs/Tetramino_J";
                break;
            case 6:
                randomTetraminoName = "Prefabs/Tetramino_S";
                break;
            case 7:
                randomTetraminoName = "Prefabs/Tetramino_Z";
                break;
        }
        return randomTetraminoName;
    }
}
