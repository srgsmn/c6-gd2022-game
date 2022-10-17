/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 * 
 *  TODO:
 *      - Save ID of Key and Wheel and keep state from destination object somehow
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class MCCollectionManager : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public CollectableType type;
        public object value;
        public string id;

        public Item(CollectableType type, object value, string id)
        {
            this.type = type;
            this.value = value;
            this.id = id;
        }
    }

    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [SerializeField]
    [ReadOnlyInspector] private int _sugarLumps;
    [SerializeField]
    [ReadOnlyInspector] private int _chocoChips;
    [SerializeField]
    [ReadOnlyInspector] private bool _gateKey;
    [SerializeField]
    [ReadOnlyInspector] private bool _storeWheel;

    public List<Item> otherItems;

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

    public bool gateKey
    {
        private set
        {
            _gateKey = value;
        }
        get { return _gateKey; }
    }

    public bool storeWheel
    {
        private set
        {
            _storeWheel = value;
        }
        get { return _storeWheel; }
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
        storeWheel = false;
        gateKey = false;

        OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);
        OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

        otherItems = new List<Item>();
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
                OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips = Consts.COLLECTABLE_MAX;
                OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

                break;
        }
    }

    public virtual void ResetValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps = 0;
                OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips = 0;
                OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

                break;
        }
    }

    public virtual void AddValue(ChParam parameter, int value)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps += value;
                OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips += value;
                OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

                break;
        }
    }

    public virtual void SubValue(ChParam parameter, int value)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps -= value;
                OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips -= value;
                OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

                break;
        }
    }

    public virtual void IncValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps++;
                OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips++;
                OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

                break;
        }
    }

    public virtual void DecValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps--;
                OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips--;
                OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

                break;
        }
    }

    public virtual void SetValue(ChParam parameter, int value)
    {
        switch (parameter)
        {
            case ChParam.SL:
                sugarLumps = value;
                OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);

                break;

            case ChParam.CC:
                chocoChips = value;
                OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);

                break;
        }
    }


    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void InventoryChangedEvent(CollectableType parameter, object value);
    public static InventoryChangedEvent OnInventoryChanged;

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

            MCCollisionManager.OnOpenGate += OnOpen;
        }
        else
        {
            // Debug inputs
            InputManager.OnDebugValueUpdate -= OnDebugValueUpdate;

            Collectable.OnCollection -= OnCollection;

            StoreItem.OnPurchase -= Purchase;

            DataManager.OnGameLoading -= LoadData;

            CollectablesGUIManager.OnGUIStartup -= ReplyWithData;

            MCCollisionManager.OnOpenGate -= OnOpen;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void ReplyWithData()
    {
        OnInventoryChanged?.Invoke(CollectableType.SL, sugarLumps);
        OnInventoryChanged?.Invoke(CollectableType.CC, chocoChips);
        OnInventoryChanged?.Invoke(CollectableType.Key, gateKey);
        OnInventoryChanged?.Invoke(CollectableType.Wheel, storeWheel);
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
        switch (type)
        {
            case CollectableType.SL:
                IncValue(ChParam.SL);

                break;

            case CollectableType.CC:
                IncValue(ChParam.CC);

                break;

            case CollectableType.Key:
                gateKey = true;
                OnInventoryChanged?.Invoke(CollectableType.Key, gateKey);

                otherItems.Add(new Item(type, (int)1, id));

                break;

            case CollectableType.Wheel:
                storeWheel = true;
                OnInventoryChanged?.Invoke(CollectableType.Wheel, storeWheel);

                otherItems.Add(new Item(type, (int)1, id));

                break;
        }
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

    private void OnOpen()
    {
        gateKey = false;
    }

    private void Purchase(StoreTransaction transaction)
    {
        SubValue(ChParam.SL, transaction.SL);
        SubValue(ChParam.CC, transaction.CC);

    }

    public void UseItem(CollectableType type)
    {
        switch (type)
        {
            case CollectableType.Key:
                gateKey = false;
                OnInventoryChanged?.Invoke(CollectableType.Key, gateKey);

                break;

            case CollectableType.Wheel:
                storeWheel = false;
                OnInventoryChanged?.Invoke(CollectableType.Wheel, storeWheel);

                break;
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
