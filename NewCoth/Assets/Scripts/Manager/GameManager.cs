using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Level")]
    public string musicThemeName;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManagerCS.instance.Play(musicThemeName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
