using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Animator>().speed = 1.0f;
        GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
