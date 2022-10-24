/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 * 
 *  TODO:
 *      -
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class MCHealthController : HealthController
{

    [Header("Text instantiation parameter:")]
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private Canvas targetCanvas;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    public override void Start()
    {
        base.Start();

        NotifyAll();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        EventNotifier(ChParam.Health);
        if (isArmored) EventNotifier(ChParam.Armor);

        if (!isAlive)
        {
            Die();
        }
        else
        {
            OnDamage?.Invoke();
        }
    }

    public override void Die()
    {
        base.Die();

        OnDeath?.Invoke();
    }

    public override void RestoreValue(ChParam parameter)
    {
        base.RestoreValue(parameter);

        EventNotifier(parameter);
    }

    public override void ResetValue(ChParam parameter)
    {
        base.ResetValue(parameter);

        if(parameter == ChParam.Health)
            Die();

        EventNotifier(parameter);
    }

    public override void AddValue(ChParam parameter, float value)
    {
        base.AddValue(parameter, value);

        EventNotifier(parameter);
    }

    public override void SubValue(ChParam parameter, float value)
    {
        base.SubValue(parameter, value);

        EventNotifier(parameter);
    }

    public override void IncValue(ChParam parameter)
    {
        base.IncValue(parameter);

        EventNotifier(parameter);
    }

    public override void DecValue(ChParam parameter)
    {
        base.DecValue(parameter);

        EventNotifier(parameter);
    }

    public override void SetValue(ChParam parameter, float value)
    {
        base.SetValue(parameter, value);

        EventNotifier(parameter);
    }

    private void EventNotifier(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.Health:
                OnValueChanged?.Invoke(parameter, health);

                break;

            case ChParam.MaxHealth:
                OnValueChanged?.Invoke(parameter, maxHealth);

                break;

            case ChParam.DefHFact:
                OnValueChanged?.Invoke(parameter, defHealthFactor);

                break;

            case ChParam.Armor:
                OnValueChanged?.Invoke(parameter, armor);

                break;

            case ChParam.MaxArmor:
                OnValueChanged?.Invoke(parameter, maxArmor);

                break;

            case ChParam.DefAFact:
                OnValueChanged?.Invoke(parameter, defArmorFactor);

                break;
        }
    }

    private void NotifyAll()
    {
        Deb("NotifyStart(): Component is active and communicating initial parameters. DataManager is supposed to update inspector data.");

        OnValueChanged?.Invoke(ChParam.MaxHealth, maxHealth);
        OnValueChanged?.Invoke(ChParam.Health, health);
        OnValueChanged?.Invoke(ChParam.MaxArmor, maxArmor);
        OnValueChanged?.Invoke(ChParam.Armor, armor);
        OnValueChanged?.Invoke(ChParam.DefHFact, defHealthFactor);
        OnValueChanged?.Invoke(ChParam.DefAFact, defArmorFactor);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void HealthChangedEvent(ChParam parameter, object value);
    public static HealthChangedEvent OnValueChanged;

    public delegate void DeathEvent();
    public static DeathEvent OnDeath;

    public delegate void DamageEvent();
    public static DamageEvent OnDamage;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            // Debug
            InputManager.OnDebugValueUpdate += OnDebugValueUpdate;

            StoreItem.OnPurchase += Purchase;

            DataManager.OnGameLoading += LoadData;

            HealthBarGUIManager.OnGUIStartup += ReplyWithData;

            OnDamage += StartAnimation;
        }
        else
        {
            // Debug
            InputManager.OnDebugValueUpdate -= OnDebugValueUpdate;

            StoreItem.OnPurchase -= Purchase;

            DataManager.OnGameLoading -= LoadData;

            HealthBarGUIManager.OnGUIStartup -= ReplyWithData;

            OnDamage -= StartAnimation;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void LoadData(PlayerData data)
    {
        Deb("LoadData(): Loading health data \n{ maxHealth: " + data.maxHealth + ", health: " + data.health + ", maxArmor: " + data.maxArmor + ", armor: " + data.armor + ", defHFactor: " + data.defHFactor + ", defAFactor: " + data.defAFactor + " }");

        SetValue(ChParam.MaxHealth, data.maxHealth);
        SetValue(ChParam.Health, data.health);
        SetValue(ChParam.MaxArmor, data.maxArmor);
        SetValue(ChParam.Armor, data.armor);
        SetValue(ChParam.DefHFact, data.defHFactor);
        SetValue(ChParam.DefAFact, data.defAFactor);

        Deb("LoadData(): Loaded data is { maxHealth: " + maxHealth + ", health: " + health + ", maxArmor: " + maxArmor + ", armor: " + armor + ", defHFactor: " + defHealthFactor + ", defAFactor: " + defArmorFactor + " }");

    }

    private void OnDebugValueUpdate(DebValue value, DebAction action)
    {
        ChParam? parameter=null;

        Deb("OnDebugValueUpdate(): elaborating debug input...");

        switch (value)
        {
            case DebValue.H:
                Deb("OnDebugValueUpdate(): target value is health...");

                parameter = ChParam.Health;

                break;

            case DebValue.A:
                Deb("OnDebugValueUpdate(): target value is armor...");

                parameter = ChParam.Armor;

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

                    RestoreValue((ChParam)parameter);

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
        if (transaction.isIncremental)
        {
            if (transaction.health != 0) AddValue(ChParam.Health, transaction.health);
            if (transaction.armor != 0) AddValue(ChParam.Armor, transaction.armor);
        }
        else
        {
            if (transaction.health != 0)
            {
                SetValue(ChParam.MaxHealth, transaction.health);
                RestoreValue(ChParam.Health);
                
            }
            if (transaction.armor != 0)
            {
                SetValue(ChParam.MaxArmor, transaction.armor);
                RestoreValue(ChParam.Armor);
            }

            if (transaction.defHFactor != 0) SetValue(ChParam.DefHFact, transaction.defHFactor);
            if (transaction.defAFactor != 0) SetValue(ChParam.DefAFact, transaction.defAFactor);
        }
    }

    private void ReplyWithData()
    {
        NotifyAll();
    }

    private void StartAnimation()
    {
        GameObject instance = Instantiate(panelPrefab, targetCanvas.transform);

        Deb("###", DebMsgType.warn);

        Destroy(instance, .5f);
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
