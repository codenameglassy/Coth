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

    [Header("Game Objects")]
    public GameObject playerGo;
    public GameObject tigerGo;
    public GameObject tigerStatue;
    public GameObject waveGo;

    [Header("Entity")]
    public PlayerEntity playerEntity;
    public ControableEntity tigerEntity;
    public WaveController waveEntity;

    public Vector3 wavePosOffset;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        waveGo.SetActive(false);
    }
    public void SpawnWave()
    {
        StartCoroutine(Enum_SpawnWave());
    }
    
    IEnumerator Enum_SpawnWave()
    {

        // Offset the spawn position
        Vector3 waveFinalPos = playerEntity.transform.position + wavePosOffset;
        waveEntity.transform.position = waveFinalPos;
        yield return null;
        waveGo.SetActive(true);
      
    }

    public void ControlTiger()
    {
        StartCoroutine(Enum_ControlTiger());
    }
 
    IEnumerator Enum_ControlTiger()
    {
        yield return null;
        tigerStatue.SetActive(false);
        playerGo.SetActive(false);
        waveGo.SetActive(false);
        AudioManagerCS.instance.Play("tigerRoar");
        tigerGo.SetActive(true);


      /*  waveGo.SetActive(false);
        tigerStatue.SetActive(false);
        playerGo.SetActive(false);
      
       
        tigerGo.SetActive(true);
        yield return new WaitForSeconds(.5f);
        waveVcam.Priority = 0;
        tigerVcam.Priority = 10;
        AudioManagerCS.instance.Play("tigerRoar");
        yield return new WaitForSeconds(.5f);*/
     
       
    }
    public void ControlPlayer()
    {
        StartCoroutine(Enum_ControlPlayer());
    }

    IEnumerator Enum_ControlPlayer()
    {
        yield return null;
       
        tigerStatue.transform.position = tigerEntity.transform.position;
        tigerGo.SetActive(false);
        tigerStatue.SetActive(true);
        playerGo.SetActive(true);

        //reset
        playerEntity.InitializeIdle();
        waveEntity.ResetWave();

    }
}
