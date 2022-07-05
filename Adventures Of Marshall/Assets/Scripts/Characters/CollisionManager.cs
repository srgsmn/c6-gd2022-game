using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private CollectablesManager collectablesManager;

    private void Awake()
    {
        collectablesManager = GetComponent<CollectablesManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        string debugMsg;

        switch (collider.gameObject.tag)
        {
            case "CheckPoint":
                debugMsg = "CollisionManager.cs | Collision detected [tag: " + collider.gameObject.tag + ", name: "+ collider.gameObject.transform.name + "]";
                Debug.Log(debugMsg);
                GameManager.SaveGame();

                break;

            case "SugarLump":
                debugMsg = "CollisionManager.cs | Collision detected [tag: " + collider.gameObject.tag + ", name: " + collider.gameObject.transform.name + "] (should be SL)";
                Debug.Log(debugMsg);
                collectablesManager.IncrementItem("SL", collider);

                break;

            case "ChocoChip":
                debugMsg = "CollisionManager.cs | Collision detected [tag: " + collider.gameObject.tag + ", name: " + collider.gameObject.transform.name + "] (should be CC)";
                Debug.Log(debugMsg);
                collectablesManager.IncrementItem("CC", collider);

                break;
        }
    }
}