/* PlayerManager.cs
 * Attach it to player, it'll automatically add all the required components that manage the player
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Perfezionare movimenti in accordo con la camera
 *  
 * Ref:
 *  - https://www.youtube.com/watch?v=4HpC--2iowE
 *  - (Checkpoints) https://www.youtube.com/watch?v=ofCLJsSUom0
 */
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealthController))]
[RequireComponent(typeof(CollectablesManager))]
[RequireComponent(typeof(CollisionManager))]
public class PlayerManager : MonoBehaviour
{
    [Header("Debug purpose readonly fields")]
    [SerializeField][ReadOnlyInspector] private PlayerController playerController;
    [SerializeField][ReadOnlyInspector] private PlayerHealthController playerHealthController;
    [SerializeField][ReadOnlyInspector] private CollectablesManager collectablesManager;
    [SerializeField][ReadOnlyInspector] private CollisionManager collisionManager;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerHealthController = GetComponent<PlayerHealthController>();
        collectablesManager = GetComponent<CollectablesManager>();
        collisionManager = GetComponent<CollisionManager>();
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (GameManager.loadedGame != null)
        {
            transform.position = GameManager.loadedGame.position;
            transform.rotation = GameManager.loadedGame.rotation;
            collectablesManager.SetSL(GameManager.loadedGame.sl);
            collectablesManager.SetCC(GameManager.loadedGame.cc);
            playerHealthController.SetHeatlth(GameManager.loadedGame.health);
            playerHealthController.SetArmor(GameManager.loadedGame.armor);
            playerHealthController.SetMaxHeatlth(GameManager.loadedGame.maxHealth);
            playerHealthController.SetMaxArmor(GameManager.loadedGame.maxArmor);
        }
        else
        {
            Debug.Log("PlayerManager.cs | No data to load");
        }
    }
}