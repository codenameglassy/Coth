using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using DG.Tweening;

public class ControableEntityManager : MonoBehaviour
{

    public static ControableEntityManager instance;

    [Header("Animators")]
    public Animator tigerAnimator;
    public Animator playerAnimator;

    [Header("Parent Game-Object")]
    public GameObject tigerGO;
    public GameObject playerGO;

    [Header("Controllers")]
    public ControableEntity tigerController;
    public PlayerEntity playerEntity;

    [Header("Camera-Roots")]
    public Transform tigerCameraRoot;
    public Transform playerCameraRoot;


    [Header("Components")]
    public CinemachineVirtualCamera virtualCamera;
    public float changeTime;
    public GameObject changeVfx;
    public Quaternion vfxRotation;

    [Header("Dev")]
    public GameObject tigerStatue;
    public GameObject trailVfx;

    private void Awake()
    {
        instance = this;
    }

    public void ControlTiger()
    {
        StartCoroutine(Enum_ControlTiger());
    }

    IEnumerator Enum_ControlTiger()
    {
        tigerStatue.gameObject.SetActive(false);
        tigerGO.transform.position = tigerStatue.transform.position;
        tigerGO.transform.rotation = tigerStatue.transform.rotation;
        playerEntity.GetComponent<PlayerInput>().enabled = false;
        //tigerGO.transform.position = playerGO.transform.position;
        //tigerGO.transform.rotation = playerGO.transform.rotation;

        yield return null;

        //playerGO.SetActive(false);
       
        tigerGO.SetActive(true);
     

        virtualCamera.Follow = tigerCameraRoot;
        Instantiate(changeVfx, tigerGO.transform.position, vfxRotation);
        //GameObject trailVfxGO = Instantiate(trailVfx, playerGO.transform.position, vfxRotation);
        //trailVfxGO.transform.DOMove(tigerStatue.transform.position, 1f);

        yield return new WaitForSeconds(changeTime);

        
        playerEntity.stateMachine.ChangeState(playerEntity.sleepState);
        
        tigerController.stateMachine.ChangeState(tigerController.idleState);
        PlayerManaManager.instance.isUsingMana = true;
    }

    public void ControlPlayer()
    {
        StartCoroutine(Enum_ControlPlayer());
    }

    IEnumerator Enum_ControlPlayer()
    {
        //playerGO.transform.position = tigerGO.transform.position;
        //playerGO.transform.rotation = tigerGO.transform.rotation;
        
        yield return null;

        tigerStatue.SetActive(true);
        tigerGO.SetActive(false);

        playerEntity.GetComponent<PlayerInput>().enabled = true;

        yield return null;
        playerGO.SetActive(true);

        virtualCamera.Follow = playerCameraRoot;
        Instantiate(changeVfx, playerGO.transform.position, vfxRotation);
       
        yield return new WaitForSeconds(changeTime);
        playerEntity.stateMachine.ChangeState(playerEntity.waitState);
        PlayerManaManager.instance.isUsingMana = false;
    }

    public void DisablePlayerInput()
    {
        playerEntity.GetComponent<ThirdPersonController>().enabled = false;
        
    }
  
}
