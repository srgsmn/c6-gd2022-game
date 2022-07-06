//CoinBehaviour.cs
/* Manages the behaviour and animation of coin-like objects such as spinning and translating bypassing anim animations.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 spinSpeed = Vector3.one;
    [SerializeField] Vector3 translationSpeed = Vector3.zero;

    private Vector3 positionOnStart;

    private void Start()
    {
        positionOnStart = transform.localPosition;
        
        //transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-90f, +90f), Random.Range(-90f, +90f), Random.Range(-90f, +90f)));
    }

    private void Update()
    {
        transform.Rotate(360 * spinSpeed.x * Time.deltaTime, 360 * spinSpeed.y * Time.deltaTime, 360 * spinSpeed.z * Time.deltaTime);
        /*transform.position = new Vector3(positionOnStart.x + .125f * Mathf.Sin(translationSpeed.x * Time.time),
                                         positionOnStart.y + .125f * Mathf.Sin(translationSpeed.y * Time.time),
                                         positionOnStart.z + .125f * Mathf.Sin(translationSpeed.z * Time.time));
        */
        transform.localPosition = new Vector3(positionOnStart.x + .125f * Mathf.Sin(translationSpeed.x * Time.time),
                                         positionOnStart.y + .125f * Mathf.Sin(translationSpeed.y * Time.time),
                                         positionOnStart.z + .125f * Mathf.Sin(translationSpeed.z * Time.time));
    }
}
