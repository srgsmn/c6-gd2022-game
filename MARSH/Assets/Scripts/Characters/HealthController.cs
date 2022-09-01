/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 * 
 *  TODO:
 *      - Purchase event
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [SerializeField]
    [ReadOnlyInspector] private bool _isAlive, _isArmored;
    [SerializeField]
    [ReadOnlyInspector]
    private float _health, _maxHealth, _armor, _maxArmor, _defHealthFactor, _defArmorFactor;


    public bool isAlive
    {
        private set
        {
            Deb("Setting isAlive to " + value);
            _isAlive = value;
        }
        get { return _isAlive; }
    }

    public float health
    {
        private set
        {
            Deb("Setting health to " + value);

            _health = value;

            if (_health <= 0)
            {
                isAlive = false;
                _health = 0;
            }
            else if (_health > 0 && !isAlive) isAlive = true;
            if (_health > maxHealth) _health = maxHealth;
        }
        get
        {
            return _health;
        }
    }
    public float maxHealth
    {
        private set
        {
            Deb("Setting maxHealth to " + value);

            _maxHealth = value;
        }
        get
        {
            return _maxHealth;
        }
    }

    /// <summary>
    /// Over 0 and below 1 (1 = not defending)
    /// </summary>
    public float defHealthFactor
    {
        private set
        {
            if (value == 0) value = .1f;
            _defHealthFactor = value;
        }
        get { return _defHealthFactor; }
    }

    public bool isArmored
    {
        private set
        {
            _isArmored = value;
        }
        get { return _isArmored; }
    }

    public float armor
    {
        private set
        {
            _armor = value;

            if (_armor <= 0)
            {
                isArmored = false;
                _armor = 0;
                maxArmor = 0;
            }
            else if (_armor > 0 && maxArmor > 0 && !isArmored) isArmored = true;

            if (_armor > maxArmor) _armor = maxArmor;
        }
        get { return _armor; }
    }

    public float maxArmor {
        private set
        {
            _maxArmor = value;
        }
        get
        {
            return _maxArmor;
        }
    }

    /// <summary>
    /// Over 0 and below 1 (1 = not defending)
    /// </summary>
    public float defArmorFactor
    {
        private set
        {
            if (value == 0) value = 0.01f;
            _defArmorFactor = value;
        }
        get
        {
            return _defArmorFactor;
        }
    }

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        //EventSubscriber();
    }

    public virtual void Start()
    {
        AttributesReset();

    }

    private void OnDestroy()
    {
        //EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS
    private void AttributesReset()
    {
        Deb("AttributesReset(): resetting all attributes");
        maxHealth = 100;
        health = 100;
        defHealthFactor = 1;
        maxArmor = 0;
        armor = 0;
        defArmorFactor = 1;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isArmored)
        {
            if (health > armor)
            {
                health -= damage * defHealthFactor;
            }

            armor -= damage * defArmorFactor;
        }
        else
        {
            health -= damage * defHealthFactor;
        }
    }

    public virtual void Die()
    {
        if (isAlive) isAlive = false;
        if (health > 0) health = 0;
        if (armor > 0) armor = 0;

        if(gameObject.tag!="Player")
            Destroy(gameObject);
    }

    public virtual void RestoreValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.Health:
                health = maxHealth;

                break;

            case ChParam.Armor:
                armor = maxArmor;
                isArmored = true;

                break;
        }
    }

    public virtual void ResetValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.Health:
                health = 0;

                break;

            case ChParam.Armor:
                armor = 0;

                break;
        }
    }

    public virtual void AddValue(ChParam parameter, float value)
    {
        switch (parameter)
        {
            case ChParam.Health:
                health += value;

                break;

            case ChParam.Armor:
                armor += value;

                break;
        }
    }

    public virtual void SubValue(ChParam parameter, float value)
    {
        switch (parameter)
        {
            case ChParam.Health:
                health -= value;

                break;

            case ChParam.Armor:
                armor -= value;

                break;
        }
    }

    public virtual void IncValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.Health:
                health += 10f;

                break;

            case ChParam.Armor:
                armor += 10f;

                break;
        }
    }

    public virtual void DecValue(ChParam parameter)
    {
        switch (parameter)
        {
            case ChParam.Health:
                health -=10f;

                break;

            case ChParam.Armor:
                armor -= 10f;

                break;
        }
    }

    public virtual void SetValue(ChParam parameter, float value)
    {
        switch (parameter)
        {
            case ChParam.Health:
                health = value;

                break;

            case ChParam.MaxHealth:
                maxHealth = value;

                break;

            case ChParam.DefHFact:
                defHealthFactor = value;

                break;

            case ChParam.Armor:
                armor = value;

                break;

            case ChParam.MaxArmor:
                maxArmor = value;

                break;

            case ChParam.DefAFact:
                defArmorFactor = value;

                break;
        }
    }

    public virtual float GetHealth()
    {
        return _health;
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER



    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS
    

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
