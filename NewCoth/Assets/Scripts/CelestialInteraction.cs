using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CelestialInteraction : MonoBehaviour, IInteractable
{
    public CinemachineVirtualCamera myVcam;
    private Camera mainCam;

    public List<GameObject> normalScene = new List<GameObject>();
    public List<GameObject> darkScene = new List<GameObject>();

    public float interactionTime;

    [Header("Vfx")]
    public Transform prayVfxPos;
    public GameObject prayVfxPrefab;
    public Quaternion prayvfxRotaion;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void Interact(GameObject interacter)
    {

        StartCoroutine(Enum_SwitchToDark());
    }

  

    IEnumerator Enum_SwitchToDark()
    {
        yield return null;

        //darkscene
        myVcam.Priority = 11;
        AudioManagerCS.instance.Play("zoomOut");
        for (int i = 0; i < normalScene.Count; i++)
        {
            normalScene[i].SetActive(false);
        }
        for (int i = 0; i < darkScene.Count; i++)
        {
            darkScene[i].SetActive(true);
        }
        mainCam.clearFlags = CameraClearFlags.SolidColor;
        mainCam.backgroundColor = Color.black;

        SpawnPrayVfxRoutine();
        yield return new WaitForSeconds(interactionTime);

        //normalscene
        myVcam.Priority = 0;
        AudioManagerCS.instance.Play("zoomOut");
        for (int i = 0; i < normalScene.Count; i++)
        {
            normalScene[i].SetActive(true);
        }
        for (int i = 0; i < darkScene.Count; i++)
        {
            darkScene[i].SetActive(false);
        }
        mainCam.clearFlags = CameraClearFlags.Skybox;
       
    }

    public void SpawnPrayVfxRoutine()
    {
        StartCoroutine(Enum_SpawnPrayVfx());
    }

    IEnumerator Enum_SpawnPrayVfx()
    {
        yield return new WaitForSeconds(1f);
        if (prayVfxPrefab != null)
            Instantiate(prayVfxPrefab, prayVfxPos.position, prayvfxRotaion);

        yield return new WaitForSeconds(1f);
        if (prayVfxPrefab != null)
            Instantiate(prayVfxPrefab, prayVfxPos.position, prayvfxRotaion);

        yield return new WaitForSeconds(1f);
        if (prayVfxPrefab != null)
            Instantiate(prayVfxPrefab, prayVfxPos.position, prayvfxRotaion);

        yield return new WaitForSeconds(1f);
        if (prayVfxPrefab != null)
            Instantiate(prayVfxPrefab, prayVfxPos.position, prayvfxRotaion);

    }
}
