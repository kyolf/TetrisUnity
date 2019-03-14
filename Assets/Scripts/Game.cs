using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNextTetraMino();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Resources folder is instantiated at runtime
    public void SpawnNextTetraMino()
    {
        GameObject nextTetraMino = (GameObject)Instantiate(Resources.Load(GetRandTetramino(), typeof(GameObject)),
                                    new Vector2(5.0f, 20.0f), Quaternion.identity);
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
