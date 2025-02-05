using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenScaleUpVfx : MonoBehaviour
{
    public Vector3 finalScale;
    public float scaleTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(finalScale, scaleTime).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

  
}
