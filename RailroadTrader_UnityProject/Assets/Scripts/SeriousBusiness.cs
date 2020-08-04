using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SeriousBusiness : MonoBehaviour
{
    public VideoPlayer player;
    public GameObject UICanvas;


    public KeyCode[] combo;
    [SerializeField]
    private int currentIndex = 0;
    private bool gifPlaying = false;
    [SerializeField]
    private float passedTime = 0.0f;
    private float timeTilReset;
    private bool comboInput { get { return currentIndex > 0 ? true : false; } }

    private void Awake()
    {
        timeTilReset = 3.0f;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    StartCoroutine(PlayGif());
        //}

        if (comboInput)
            Timer();

        if (currentIndex < combo.Length && !gifPlaying)
        {            
            if (Input.GetKeyDown(combo[currentIndex]))
            {
                print("pressed " + combo[currentIndex].ToString());
                currentIndex++;
                passedTime = 0.0f;
                if (currentIndex == combo.Length)
                {
                    Debug.Log("C-c-c-combo!");
                    ResetCombo();
                    gifPlaying = true;
                    StartCoroutine(PlayGif());
                }
            }       
            return;
        }
    }

    private void ResetCombo()
    {
        currentIndex = 0;
        passedTime = 0.0f;
    }

    private void Timer()
    {
        passedTime += Time.deltaTime;
        if (passedTime >= timeTilReset)
        {
            ResetCombo();
        }
    }

    private IEnumerator PlayGif()
    {
        UICanvas.SetActive(false);
        print("do combo thing");
        player.Play();
        while (player.isPlaying)
            yield return null;

        UICanvas.SetActive(true);
        player.Stop();
        gifPlaying = false;
        yield return null;
    }
}
