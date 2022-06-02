/*  CollectablesManagerGUI.cs
 * GUI manager for collectables
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Put something in globals.
 *  
 * REF:
 *  - 
 *  
 *  Debug.Log("CollectablesManagerGUI | ");
 */

using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectablesManagerGUI : MonoBehaviour
{
    private const int showTime = 3; //TODO in Globals

    public enum IndicatorType       //TODO in Globals
    {
        SL, CC
    }


    class Indicator
    {
        private IndicatorType _indicatorType;
        public IndicatorType indicatorType
        {
            get { return _indicatorType; }
        }

        /*
        private Text _text;
        public Text text
        {
            get { return _text; }
            set { _text = value; }
        }
        */

        private float _timer;           // FIELD
        public float timer              // PROPERTY
        {
            get { return _timer; }
            set { _timer = value; }
        }

        /*
        public Indicator(IndicatorType indicatorType, Text text, float timer = showTime)
        {
            this._indicatorType = indicatorType;
            this.text.transform.position = new Vector3(0, 0, 0);
            this.timer = timer;
        }
        */

        public Indicator(IndicatorType indicatorType, float timer = showTime)
        {
            this._indicatorType = indicatorType;
            ResetTimer(timer);
        }

        /*
        public void UpdatePosition(int index)
        {
            Debug.Log("CollectablesManagerGUI | Updating indicator position");
            Vector3 temp = this.text.transform.position;
            temp.y = 0;

            text.transform.position = temp;
            text.transform.position += new Vector3(0, index * -20f, 0);
            Debug.Log("CollectablesManagerGUI | New text position is: " + text.transform.position);
        }
        */

        public void ResetTimer(float showTime = showTime)
        {
            timer = showTime;
        }

        public void DeleteTimer()
        {
            timer = 0f;
        }

        /*
        public bool TextCheck(Text text)
        {
            if (this.text == text)
                return true;
            else
                return false;
        }
        */
    }

    [SerializeField] private Text SLText;
    [SerializeField] private Text CCText;
    private Dictionary<IndicatorType, Text> items = new Dictionary<IndicatorType, Text>();

    List<Indicator> indicators = new List<Indicator>();
    //Dictionary<Text, Vector3> textPositionsOnStart = new Dictionary<Text, Vector3>();
    //Dictionary<Text, float> timers = new Dictionary<Text, float>();

    //METHODS
    private void Awake()
    {
        items.Add(IndicatorType.SL, SLText);
        items.Add(IndicatorType.CC, CCText);
    }

    private void Start()
    {
        foreach(IndicatorType indicatorType in Enum.GetValues(typeof(IndicatorType)))
        {
            HideText(indicatorType);
        }

        //HideText(IndicatorType.SL);
        //HideText(IndicatorType.CC);
    }

    private void Update()
    {
        foreach(Indicator indicator in indicators)
        {
            if (indicator.timer > 0)
            {
                indicator.timer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("CollectablesManagerGUI | Time out, hiding text...");
                HideText(indicator.indicatorType);
                break;
            }
        }
    }

    public void UpdateSL(int value)
    {
        UpdateCounter(IndicatorType.SL, value);

        
        Debug.Log("CollectablesManagerGUI | Updating SLText in " + items[IndicatorType.SL].text);
    }

    public void UpdateCC(int value)
    {
        UpdateCounter(IndicatorType.CC, value);

        
        Debug.Log("CollectablesManagerGUI | Updating CCText in " + items[IndicatorType.CC].text);
    }
    
    public void UpdateCounter(IndicatorType indicatorType, int value)
    {
        switch (indicatorType)
        {
            case IndicatorType.SL:
                ShowText(IndicatorType.SL);
                items[IndicatorType.SL].text = "SL: " + value;

                Debug.Log("CollectablesManagerGUI | Calling start timer function");

                break;

            case IndicatorType.CC:
                ShowText(IndicatorType.CC);
                items[IndicatorType.CC].text = "CC: " + value;

                Debug.Log("CollectablesManagerGUI | Calling start timer function");

                break;
        }
    }
    
    private void ShowText(IndicatorType indicatorType)
    {
        int index;

        if(indicators.Exists(i => i.indicatorType == indicatorType))
        {
            index = indicators.IndexOf(indicators.Find(i => i.indicatorType == indicatorType));
            Debug.Log("CollectablesManagerGUI | Indicator already existing at index "+index+". Resetting the timer...");

            indicators[index].ResetTimer();
            Debug.Log("CollectablesManagerGUI | Timer reset");
        }
        else
        {
            indicators.Add(new Indicator(indicatorType));
            index = indicators.IndexOf(indicators.Find(i => i.indicatorType == indicatorType));

            Debug.Log("CollectablesManagerGUI | Indicator non existing. Adding at index "+index);
        }

        //items[indicatorType].rectTransform.position = new Vector3(0, index*-20f, 0);

        UpdateList();

        
    }

    private void HideText(IndicatorType indicatorType)
    {
        items[indicatorType].rectTransform.localPosition = new Vector3(-100, 0, 0);                                                                //POSIZIONE

        Debug.Log("CollectablesManagerGUI | Hiding text by moving it to "+ items[indicatorType].rectTransform.localPosition);

        if (indicators.Exists(i => i.indicatorType==indicatorType))
        {
            indicators.Remove(indicators.Find(i => i.indicatorType == indicatorType));

            Debug.Log("CollectablesManagerGUI | Removing from list. Now list is: "+indicators.ToString());

            UpdateList();
        }
    }

    

    private void UpdateList()
    {
        foreach(Indicator indicator in indicators)
        {
            int index = indicators.IndexOf(indicator);
            
            items[indicator.indicatorType].rectTransform.localPosition = new Vector3(0, index * -25f,0);                       //POSIZIONE
            //indicator.UpdatePosition(indicators.IndexOf(indicator));
        }
    }
}


//OLD SCRIPTS


/*
    private void HideText(Text text)
    {
        Vector2 oldPos = text.rectTransform.anchoredPosition;

        text.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(oldPos.x, -130, 1), oldPos.y);
    }
    */

/*
        //text.transform.position = new Vector3(Mathf.Lerp(textPositionsOnStart[text].x, -130, 1), textPositionsOnStart[text].y, textPositionsOnStart[text].z);
        //text.transform.position = new Vector3(Mathf.Lerp(text.transform.position.x, -130, 1), text.transform.position.y, text.transform.position.z);
        int index = indicators.IndexOf(indicators.Find(i => i.indicatorType == indicatorType));

        Text text = indicators[index].text;
        indicators.RemoveAt(index);

        Vector2 oldPos = text.rectTransform.anchoredPosition;

        text.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(oldPos.x, -130, 1), oldPos.y);


        /*
        if (timers.ContainsKey(text))
        {
            Debug.Log("CollectablesManagerGUI | Resetting timer to 3000");
            timers.Remove(text);
            --indexCounter;
            UpdateList();
        }
        */

/*
        //text.transform.position = new Vector3(Mathf.Lerp(-130, textPositionsOnStart[text].x, 1), indicators.Find(i=>i.GetText()==text).GetText().transform.position.y, textPositionsOnStart[text].z);
        //text.transform.position = new Vector3(Mathf.Lerp(-130, textPositionsOnStart[text].x, 1), text.transform.position.y, textPositionsOnStart[text].z);
        indicators.Add(new Indicator(indicatorType));

        index = indicators.IndexOf(indicators.Find(i => i.indicatorType == indicatorType));
        Debug.Log("CollectablesManagerGUI | Found index: "+index);

        //indicators[index].UpdatePosition(index);

        //StartTimer(text);
        UpdateList();
        */

//items[IndicatorType.CC].text = "CC: " + value;
/*
if (!indicators.Contains(x => x.indicatorType == IndicatorType.CC))
{
    indicators.Enqueue(new Indicator(IndicatorType.CC));
    ShowText(CCText);
}
else
{
    indicators.Contains(x => if (x.indicatorType == IndicatorType.CC) { x.ResetTimer() })
}
*/

//items[IndicatorType.SL].text = "SL: " + value;
/*
if (!indicators.Contains(x => x.indicatorType == IndicatorType.SL))
{
    indicators.Enqueue(new Indicator(IndicatorType.SL));
    ShowText(SLText);
}
else
{
    indicators.Contains(x => if (x.indicatorType == IndicatorType.SL) { x.ResetTimer() })
}
*/