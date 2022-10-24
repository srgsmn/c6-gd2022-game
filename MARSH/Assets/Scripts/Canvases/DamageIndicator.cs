using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{

    private Animation anim;

    private void Awake()
    {
        EventSubscriber();

        anim = gameObject.GetComponent<Animation>();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCHealthController.OnDamage += PlayAnimation;
        }
        else
        {
            MCHealthController.OnDamage -= PlayAnimation;
        }
    }

    private void PlayAnimation()
    {
        anim.Play();
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
