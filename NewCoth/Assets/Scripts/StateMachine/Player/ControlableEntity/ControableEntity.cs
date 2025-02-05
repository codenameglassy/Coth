using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;

public class ControableEntity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public ControableEntityFiniteStateMachine stateMachine { get; private set; }

    public ControableEntityRevertToPlayerState revertToPlayer { get; private set; }
    public ControableEntityWait waitState { get; private set; }

    public ControableEntityIdleState idleState { get; private set; }

    [Header("Components")]
    [SerializeField] private ControableEntityStateData stateData;
    [SerializeField] private ControableEntityData entityData;
    [SerializeField] private ThirdPersonController thirdPersonController;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera virtualCamera;
    public float zoomInSize;
    public float zoomOutSize;
    public float zoomTime = 2f;
    private Coroutine currentZoomCoroutine;

    [Header("Controable")]
    public GameObject tigerController;
    public GameObject playerController;
    public Transform tigerControllerCameraRoot;
    public Transform playerControllerCameraRoot;

    private void Start()
    {
        anim = GetComponent<Animator>();
        stateMachine = new ControableEntityFiniteStateMachine();

        idleState = new ControableEntityIdleState(this, stateMachine, stateData, "idle");
        revertToPlayer = new ControableEntityRevertToPlayerState(this, stateMachine, stateData, "revertToPlayer");
        waitState = new ControableEntityWait(this, stateMachine, stateData, "wait");

        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void SetMovement(bool bool_)
    {
        thirdPersonController.canMove = bool_;
    }

    #region Cinemachine
    public void SmoothZoom(float targetSize, float duration)
    {
        if (virtualCamera != null)
        {
            // Stop any ongoing zoom coroutine
            if (currentZoomCoroutine != null)
            {
                StopCoroutine(currentZoomCoroutine);
            }

            // Start a new zoom coroutine
            currentZoomCoroutine = StartCoroutine(ZoomToSize(targetSize, duration));
        }
    }


    private IEnumerator ZoomToSize(float targetSize, float duration)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Normalize elapsed time to [0, 1]
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }

        // Ensure the final size is set
        virtualCamera.m_Lens.OrthographicSize = targetSize;
    }
    public void SetNewFollowCameraRoot(Transform newCameraRoot)
    {
        virtualCamera.Follow = newCameraRoot;
    }

    #endregion

}
