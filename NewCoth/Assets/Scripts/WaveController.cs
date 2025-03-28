using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    [Header("Movment")]
    public float moveSpeed = 5f;
   
 
    [Header("Check Surroundings")]
    public LayerMask whatIsControableEntity;
    public float checkRadius;
    

    void Start()
    {
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        CheckControableEntity();
    }

    void CheckControableEntity()
    {
        bool isControableEntity = Physics.CheckSphere(transform.position, checkRadius, whatIsControableEntity);
        if (isControableEntity)
        {
            StartCoroutine(Enum_TryControlTiger());
        }
    }
    bool controllingEntity = false;
    IEnumerator Enum_TryControlTiger()
    {
        yield return null;

        if (controllingEntity)
        {
            yield break;
        }
        controllingEntity = true;

        ControableEntityManager.instance.ControlTiger();
    }

    public void ResetWave()
    {
        controllingEntity = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

}