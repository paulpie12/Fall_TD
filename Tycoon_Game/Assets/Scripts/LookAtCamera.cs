using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        if(Camera.main != null)
        {
            transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));
        }
    }
}
