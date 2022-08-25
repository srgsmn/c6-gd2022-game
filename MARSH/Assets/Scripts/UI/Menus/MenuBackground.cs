/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackground : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [SerializeField] private Image background;
    [SerializeField][Range(.1f, 1f)] private float lerpTime;
    [SerializeField] private Color[] colors;

    [Header("Debug values:")]
    [SerializeField][ReadOnlyInspector] private int index;
    [SerializeField][ReadOnlyInspector] private float t = 0f;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        if (background == null)
        {
            background = GetComponent<Image>();
        }
    }

    private void Start()
    {
        background.color = Color.white;

        index = Random.Range(0, colors.Length);
    }

    private void Update()
    {
        if (colors.Length > 0)
        {
            background.color = Color.Lerp(background.color, colors[index], lerpTime * Time.unscaledDeltaTime);

            OnNewBGColor?.Invoke(background.color);

            t = Mathf.Lerp(t, 1f, lerpTime * Time.unscaledDeltaTime);

            if (t > .9f)
            {
                t = 0f;

                index = Random.Range(0, colors.Length);
            }
        }
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void NewBGColorEvent(Color color);
    public static NewBGColorEvent OnNewBGColor;

    // DEBUG PRINTER ___________________________________________________________ DEBUG PRINTER

    private void Deb(string msg, DebMsgType type = DebMsgType.log)
    {
        switch (type)
        {
            case DebMsgType.log:
                Debug.Log(this.GetType().Name + " > " + msg);
                break;

            case DebMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg);
                break;

            case DebMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg);


                break;
        }
    }
}
