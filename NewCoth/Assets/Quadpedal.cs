using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadpedal : MonoBehaviour
{
    public float speed;

   

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

  


}
