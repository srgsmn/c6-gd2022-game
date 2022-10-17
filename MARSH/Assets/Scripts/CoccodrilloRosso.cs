using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoccodrilloRosso : MonoBehaviour
{

    private Animator animator;
    private float time = 0.0f;
    private float intervallo = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= intervallo)
        {
            animator.SetTrigger("movespuntoni");
            time = 0.0f;
        }
    }
}
