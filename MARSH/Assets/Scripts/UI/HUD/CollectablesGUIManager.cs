/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;
using TMPro;
using System;

public class CollectablesGUIManager : MonoBehaviour
{
    private class Indicator
    {
        public CollectableType collectableType { private set; get; }
        public float timer;

        public Indicator(CollectableType collectableType, float timer = Consts.SHOW_TIME)
        {
            this.collectableType = collectableType;
            ResetTimer(timer);
        }

        public void ResetTimer(float newTimer = Consts.SHOW_TIME)
        {
            timer = newTimer;
        }

        public void DeleteTimer()
        {
            timer = 0f;
        }
    }

    private class UIElement
    {
        public GameObject gameObject { private set; get; }
        public Transform transform { private set; get; }
        public TextMeshProUGUI count;
        public Animator icon { private set; get; }

        public UIElement(GameObject gameObject, TextMeshProUGUI count, Animator icon)
        //public UIElement(Transform transform, TextMeshProUGUI text)
        {
            this.gameObject = gameObject;
            this.transform = gameObject.GetComponent<Transform>();
            this.count = count;
            this.icon = icon;
        }
    }

    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [Header("Coins:")]
    [SerializeField] private GameObject SLCounter;
    [SerializeField] private TextMeshProUGUI SLText;
    [SerializeField] private Animator SLIcon;
    [SerializeField] private GameObject CCCounter;
    [SerializeField] private TextMeshProUGUI CCText;
    [SerializeField] private Animator CCIcon;
    [Header("Objects:")]
    [SerializeField]
    [ReadOnlyInspector] private bool hasKey;
    [SerializeField] private GameObject KeyContainer;
    [SerializeField] private Animator KeyIcon;
    [SerializeField]
    [ReadOnlyInspector] private bool hasWheel;
    [SerializeField] private GameObject WheelContainer;
    [SerializeField] private Animator WheelIcon;

    [SerializeField]
    [ReadOnlyInspector] private bool keep = false;


    private Dictionary<CollectableType, UIElement> items = new Dictionary<CollectableType, UIElement>();

    private List<Indicator> indicators = new List<Indicator>();


    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        items.Add(CollectableType.SL, new UIElement(SLCounter, SLText, SLIcon));
        items.Add(CollectableType.CC, new UIElement(CCCounter, CCText, CCIcon));
        items.Add(CollectableType.Key, new UIElement(KeyContainer, null, KeyIcon));
        items.Add(CollectableType.Wheel, new UIElement(WheelContainer, null, WheelIcon));

        EventSubscriber();
    }

    private void Start()
    {
        OnGUIStartup?.Invoke();
        ShowAll();
    }

    private void Update()
    {
        foreach (Indicator indicator in indicators)
        {
            if (indicator.timer > 0 && !keep)
            {
                indicator.timer -= Time.deltaTime;
            }
            else if (!keep)
            {
                Deb("Update(): Time out, hiding text...");
                HideText(indicator.collectableType);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS

    private void ShowText(CollectableType collectableType)
    {
        int index;

        if (indicators.Exists(i => i.collectableType == collectableType))
        {
            index = indicators.IndexOf(indicators.Find(i => i.collectableType == collectableType));
            Deb("ShowText(): Indicator already existing at index \" + index + \". Resetting the timer...");

            indicators[index].ResetTimer();
            Deb("ShowText(): Timer reset");
        }
        else
        {
            indicators.Add(new Indicator(collectableType));
            index = indicators.IndexOf(indicators.Find(i => i.collectableType == collectableType));

            UIElement element = items[collectableType];

            element.gameObject.SetActive(true);
            element.icon.enabled = true;

            Deb("ShowText(): Indicator non existing. Adding at index " + index);
        }

        UpdateList();
    }

    private void HideText(CollectableType collectableType)
    {
        UIElement element = items[collectableType];

        element.gameObject.SetActive(false);
        element.icon.enabled = false;
        element.transform.localPosition = new Vector3(0, 0, 0);

        Deb("HideText(): Hiding text by moving it to " + items[collectableType].transform.localPosition);

        if (indicators.Exists(i => i.collectableType == collectableType))
        {
            indicators.Remove(indicators.Find(i => i.collectableType == collectableType));

            Deb("HideText(): Removing from list. Now list is: " + indicators.ToString());

            UpdateList();
        }
    }

    private void UpdateList()
    {
        foreach (Indicator indicator in indicators)
        {
            int index = indicators.IndexOf(indicator);

            items[indicator.collectableType].transform.localPosition = new Vector3(0, index * -60f, 0);
        }
    }

    private void ShowAllAndKeep(bool flag)
    {
        keep = flag;

        ShowAll();
    }

    private void ShowAll()
    {
        ShowText(CollectableType.SL);
        ShowText(CollectableType.CC);
        if (hasKey) ShowText(CollectableType.Key);
        if (hasWheel) ShowText(CollectableType.Wheel);
    }

    private void HideAll()
    {
        HideText(CollectableType.SL);
        HideText(CollectableType.CC);

        if (hasKey) HideText(CollectableType.Key);
        if (hasWheel) HideText(CollectableType.Wheel);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS

    public delegate void GUIDataCheck();
    public static GUIDataCheck OnGUIStartup;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCCollectionManager.OnInventoryChanged += UpdateValue;
            InputManager.OnInfo += ShowAll;

            GameManager.OnNewScreen += ShowInfoOnPause;
        }
        else
        {
            MCCollectionManager.OnInventoryChanged -= UpdateValue;
            InputManager.OnInfo -= ShowAll;

            GameManager.OnNewScreen -= ShowInfoOnPause;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void ShowInfoOnPause(GameScreen screen)
    {
        if(screen == GameScreen.PauseMenu && screen == GameScreen.StoreMenu)
        {
            ShowAllAndKeep(true);
        }
        else
        {
            ShowAllAndKeep(false);
        }
    }

    private void UpdateValue(CollectableType param, object value)
    {
        CollectableType? type = null;

        switch (param)
        {
            case CollectableType.SL:
                type = CollectableType.SL;

                break;

            case CollectableType.CC:
                type = CollectableType.CC;
                break;

            case CollectableType.Key:
                hasKey = true;
                //items[CollectableType.Key].

                break;

            case CollectableType.Wheel:
                hasWheel = true;

                break;
        }

        if (type != null)
        {
            ShowText((CollectableType)type);
            items[(CollectableType)type].count.text = value.ToString();
        }
    }

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
