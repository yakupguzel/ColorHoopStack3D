using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource backtoSocket;
    [SerializeField] AudioSource gotoNext;
    [SerializeField] AudioSource standComplate;
    [SerializeField] AudioSource levelEnd;


    public void BackToSocket()
    {
        backtoSocket.Stop();
        backtoSocket.Play();
    }

    public void GoToNext()
    {
        gotoNext.Stop();
        gotoNext.Play();
    }

    public void StandComplate()
    {
        standComplate.Stop();
        standComplate.Play();
    }

    public void LevelEnd()
    {
        gameMusic.Stop();
        levelEnd.Stop();
        levelEnd.Play();
    }
}
