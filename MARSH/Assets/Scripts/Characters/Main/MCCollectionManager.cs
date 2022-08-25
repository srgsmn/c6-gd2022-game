/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 * 
 *  TODO:
 *      -
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class MCCollectionManager : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [SerializeField]
    [ReadOnlyInspector] private int _sugarLumps;
    [SerializeField]
    [ReadOnlyInspector] private int _chocoChips;

    public int sugarLumps
    {
        private set
        {
            _sugarLumps = value;

            if (_sugarLumps < 0) _sugarLumps = 0;
        }
        get { return _sugarLumps; }
    }

    public int chocoChips
    {
        private set
        {
            _chocoChips = value;

            if (_chocoChips < 0) _chocoChips = 0;
        }
        get { return _chocoChips; }
    }

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void Start()
    {
        sugarLumps = 0;
        chocoChips = 0;

        OnValueChanged?.Invoke(ChParam.SL, sugarLumps);
        OnValueChanged?.Invoke(ChParam.CC, chocoChips);
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS
    public virtual void MaxValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps = Consts.COLLECTABLE_MAX;
                OnValueChanged?.Invoke(parameter, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips = Consts.COLLECTABLE_MAX;
                OnValueChanged?.Invoke(parameter, chocoChips);

                break;
        }
    }

    public virtual void ResetValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps = 0;
                OnValueChanged?.Invoke(parameter, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips = 0;
                OnValueChanged?.Invoke(parameter, chocoChips);

                break;
        }
    }

    public virtual void AddValue(ChParam parameter, int value)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps += value;
                OnValueChanged?.Invoke(parameter, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips += value;
                OnValueChanged?.Invoke(parameter, chocoChips);

                break;
        }
    }

    public virtual void SubValue(ChParam parameter, int value)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps -= value;
                OnValueChanged?.Invoke(parameter, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips -= value;
                OnValueChanged?.Invoke(parameter, chocoChips);

                break;
        }
    }

    public virtual void IncValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps++;
                OnValueChanged?.Invoke(parameter, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips++;
                OnValueChanged?.Invoke(parameter, chocoChips);

                break;
        }
    }

    public virtual void DecValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps--;
                OnValueChanged?.Invoke(parameter, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips--;
                OnValueChanged?.Invoke(parameter, chocoChips);

                break;
        }
    }

    public virtual void SetValue(ChParam parameter, int value)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps = value;
                OnValueChanged?.Invoke(parameter, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips = value;
                OnValueChanged?.Invoke(parameter, chocoChips);

                break;
        }
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void CollectionChangedEvent(ChParam parameter, object value);
    public static CollectionChangedEvent OnValueChanged;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            // Debug inputs
            InputManager.OnDebugValueUpdate += OnDebugValueUpdate;

            Collectable.OnCollection += OnCollection;

            StoreItem.OnPurchase += Purchase;

            DataManager.OnGameLoading += LoadData;

            CollectablesGUIManager.OnGUIStartup += ReplyWithData;
        }
        else
        {
            // Debug inputs
            InputManager.OnDebugValueUpdate -= OnDebugValueUpdate;

            Collectable.OnCollection -= OnCollection;

            StoreItem.OnPurchase -= Purchase;

            DataManager.OnGameLoading -= LoadData;

            CollectablesGUIManager.OnGUIStartup -= ReplyWithData;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void ReplyWithData()
    {
        OnValueChanged?.Invoke(ChParam.SL, sugarLumps);
        OnValueChanged?.Invoke(ChParam.CC, chocoChips);
    }

    private void LoadData(PlayerData data)
    {
        Deb("LoadData(): Loading collection data \n{ SL: " + data.sl + ", CC: " + data.cc + "}");

        SetValue(ChParam.SL, data.sl);
        SetValue(ChParam.CC, data.cc);

        Deb("LoadData(): Loaded data is { SL: " + sugarLumps + ", CC: " + chocoChips + " }");

    }

    private void OnCollection(CollectableType type, string id)
    {
        ChParam? param = null;

        switch (type)
        {
            case CollectableType.SL:
                param = ChParam.SL;

                break;

            case CollectableType.CC:
                param = ChParam.CC;

                break;
        }

        if(param != null)   IncValue((ChParam)param);
    }

    private void OnDebugValueUpdate(DebValue value, DebAction action)
    {
        ChParam? parameter = null;

        Deb("OnDebugValueUpdate(): elaborating debug input...");

        switch (value)
        {
            case DebValue.SL:
                Deb("OnDebugValueUpdate(): target value is SL...");

                parameter = ChParam.SL;

                break;

            case DebValue.CC:
                Deb("OnDebugValueUpdate(): target value is CC...");

                parameter = ChParam.CC;

                break;

            default:
                parameter = null;

                break;
        }

        if (parameter != null)
        {
            switch (action)
            {
                case DebAction.Inc:
                    Deb("OnDebugValueUpdate(): incrementing target value...");

                    IncValue((ChParam)parameter);

                    break;

                case DebAction.Dec:
                    Deb("OnDebugValueUpdate(): decrementing target value...");

                    DecValue((ChParam)parameter);

                    break;

                case DebAction.Max:
                    Deb("OnDebugValueUpdate(): maxing target value...");

                    MaxValue((ChParam)parameter);

                    break;

                case DebAction.Rst:
                    Deb("OnDebugValueUpdate(): resetting target value...");

                    ResetValue((ChParam)parameter);

                    break;
            }
        }
    }

    private void Purchase(StoreTransaction transaction)
    {
        SubValue(ChParam.SL, transaction.SL);
        SubValue(ChParam.CC, transaction.CC);

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
