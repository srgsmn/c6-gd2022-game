/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class PlayerAudio : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTE
    
    [SerializeField] private List<AudioEffect> tracks;


    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS


    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCMovementController.OnJump += OnJump;
            MCMovementController.OnLanding += OnLanding;

            MCHealthController.OnDeath += OnGameOver;
            MCHealthController.OnDamage += OnDamage;

            Collectable.OnCollection += OnCollection;
        }
        else
        {
            MCMovementController.OnJump -= OnJump;
            MCMovementController.OnLanding -= OnLanding;

            MCHealthController.OnDeath -= OnGameOver;
            MCHealthController.OnDamage -= OnDamage;

            Collectable.OnCollection -= OnCollection;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnCollection(CollectableType type, string id)
    {
        PlayerAudioType? buffType;

        switch (type)
        {
            case CollectableType.SL:
                buffType = PlayerAudioType.SL_Collection;

                break;

            case CollectableType.CC:
                buffType = PlayerAudioType.CC_Collection;

                break;

            default:

                buffType = null;

                break;
        }

        if (buffType != null)
        {
            AudioSource buffer = tracks.Find(t => t.type == buffType).source;

            if (buffer != null) buffer.Play();
        }
    }

    private void OnDamage()
    {
        AudioSource buffer = tracks.Find(t => t.type == PlayerAudioType.Damage).source;

        if (buffer != null) buffer.Play();
    }

    private void OnJump()
    {
        AudioSource buffer = tracks.Find(t => t.type == PlayerAudioType.Jump).source;

        if (buffer != null) buffer.Play();
    }

    private void OnLanding()
    {
        AudioSource buffer = tracks.Find(t => t.type == PlayerAudioType.Landing).source;

        if (buffer != null) buffer.Play();
    }

    private void OnGameOver()
    {
        AudioSource buffer = tracks.Find(t => t.type == PlayerAudioType.Death).source;

        if (buffer != null) buffer.Play();
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

