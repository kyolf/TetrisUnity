using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetramino : MonoBehaviour
{
    float fall = 0;
    public float fallSpeed = 1;

    public bool allowRotation = true;
    public bool limitRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            //Debug.Log("right");
            if (CheckIsValidPosition())
            {
            }else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            //Debug.Log("left");
            if (CheckIsValidPosition())
            {
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (allowRotation)
            {
                if (limitRotation)
                {
                    if(transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }

                //Checking if it's a valid position in grid
                if (CheckIsValidPosition())
                {
                }
                else
                {
                    if (limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            transform.position += new Vector3(0, -1, 0);
            //Debug.Log("down");
           if (CheckIsValidPosition())
            {
                //Debug.Log("down1" + transform.position);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                enabled = false;

                FindObjectOfType<Game>().SpawnNextTetramino();
                //Debug.Log("down2" + transform.position);
            }

            fall = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            //pos variable contains a rounded value of the mino's current position in the iteration
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            //Debug.Log(mino.position);
            if(FindObjectOfType<Game>().CheckIsInsideGrid(pos) == false)
            {
                return false;
            }

        }
        return true;
    }
}
