/* Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 *  
 * Ref:
 *  - (Checkpoints) https://www.youtube.com/watch?v=ofCLJsSUom0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    private Vector3 _lastPosition;
    public Vector3 lastPosition
    {
        get { return _lastPosition; }
        set { _lastPosition = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
    public float[] lastPositionToFloats()
    {
        float[] pos = new float[3];

        pos[0] = lastPosition.x;
        pos[1] = lastPosition.y;
        pos[2] = lastPosition.z;

        return pos;
    }
    */
}