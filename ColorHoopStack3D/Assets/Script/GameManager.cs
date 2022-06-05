using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager audio;
    [SerializeField] private GameObject levelEndPanel;

    public Material[] hoopColor;
    GameObject seciliObje;
    GameObject seciliStand;
    Cember _Cember;

    public bool hareketVar;
    public int hedefStandSayisi;
    int tamamlananStandSayisi;

    public List<Cember> cembers;

    public int sceneIndex;

    private void Awake()
    {
        levelEndPanel.SetActive(false);
        //RandomizeHoopColors();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (seciliObje != null && seciliStand != hit.collider.gameObject)
                    { // bir halka secilmis ise ve seciliStand ilk secilen halkanin kendisi degil baska bir halka ise halkayi gönder

                        Stand stand = hit.collider.GetComponent<Stand>();

                        if (stand._Cemberler.Count != 4 && stand._Cemberler.Count != 0)
                        {
                            if (_Cember.renk == stand._Cemberler[stand._Cemberler.Count - 1].GetComponent<Cember>().renk)
                            {
                                seciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(seciliObje);
                                audio.GoToNext();
                                _Cember.HareketEt(ActionState.ShiftPosition, hit.collider.gameObject, stand.MusaitSoketiVer(), stand.hareketPozisyonu);

                                stand.bosOlanSoket++;
                                stand._Cemberler.Add(seciliObje);
                                if (stand.CemberleriKontrolEt())
                                {
                                    stand.StandTamamlandi();
                                    audio.StandComplate();
                                }
                                seciliObje = null;
                                seciliStand = null;
                            }
                            else
                            {
                                _Cember.HareketEt(ActionState.BackToLastSocket);
                                audio.BackToSocket();
                                seciliObje = null;
                                seciliStand = null;
                            }
                        }
                        else if (stand._Cemberler.Count == 0)
                        {
                            seciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(seciliObje);

                            _Cember.HareketEt(ActionState.ShiftPosition, hit.collider.gameObject, stand.MusaitSoketiVer(), stand.hareketPozisyonu);
                            audio.GoToNext();
                            stand.bosOlanSoket++;
                            stand._Cemberler.Add(seciliObje);

                            seciliObje = null;
                            seciliStand = null;
                        }
                        else
                        {
                            _Cember.HareketEt(ActionState.BackToLastSocket);
                            seciliObje = null;
                            seciliStand = null;
                        }
                    }
                    else if (seciliStand == hit.collider.gameObject)
                    {
                        _Cember.HareketEt(ActionState.BackToLastSocket);
                        audio.BackToSocket();
                        seciliObje = null;
                        seciliStand = null;
                    }
                    else
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();
                        // seciliObje degerlerini set et
                        seciliObje = stand.EnUsttekiCemberiVer();
                        _Cember = seciliObje.GetComponent<Cember>();
                        hareketVar = true;
                        audio.BackToSocket();
                        if (_Cember.hareketEdebilirMi)
                        {
                            _Cember.HareketEt(ActionState.Choosen, null, null, _Cember._aitOlduguStand.GetComponent<Stand>().hareketPozisyonu);
                            seciliStand = _Cember._aitOlduguStand;
                        }
                        else
                        {
                            audio.BackToSocket();
                        }
                    }
                }
            }
        }
    }

    private void RandomizeHoopColors()
    {
        Cember[] tempList = FindObjectsOfType<Cember>();
        cembers = new List<Cember>();
        for (int i = 0; i < tempList.Length; i++)
        {
            cembers.Add(tempList[i]);
        }

        cembers.Shuffle();
        int colorIndex = 0;
        int colorFactor = 0;
        foreach (Cember cember in cembers)
        {
            cember.GetComponent<Renderer>().material.SetColor("_Color", hoopColor[colorIndex].color);
            cember.renk = (Colors)colorIndex;
            colorFactor++;
            if (colorFactor % 4 == 0)
            {
                colorFactor = 0;
                colorIndex++;
            }
        }
        cembers.Shuffle();

        CheckStandsState();

    }

    public void CheckStandsState()
    {
        Stand[] list = FindObjectsOfType<Stand>();

        List<Stand> checkList = new List<Stand>();

        foreach (Stand stand in list)
        {
            if (stand.bosOlanSoket == 4)
            {
                checkList.Add(stand);
            }
        }

        CheckColorCorrection(checkList);

    }

    public void CheckColorCorrection(List<Stand> _checkList)
    {
        for (int i = 0; i < _checkList.Count; i++)
        {
            Colors firstColor = _checkList[i]._Cemberler[0].GetComponent<Cember>().renk;
            int colorCount = 0;
            for (int j = 0; j < _checkList[i]._Cemberler.Count; j++)
            {
                if (firstColor == _checkList[i]._Cemberler[j].GetComponent<Cember>().renk)
                {
                    colorCount++;
                }
                if (colorCount > 3)
                {
                    cembers.Shuffle();
                    CheckColorCorrection(_checkList);
                }
            }
        }
    }

    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StandTamamlandi()
    {
        tamamlananStandSayisi++;

        if (tamamlananStandSayisi == hedefStandSayisi)
        {
            audio.LevelEnd();
            levelEndPanel.SetActive(true);

        }
    }

    public void LoadNextLevel()
    {
        sceneIndex = int.Parse(SceneManager.GetActiveScene().name);

        if (sceneIndex == 4)
        {
            LoadMainMenu();
        }
        else
        {
            sceneIndex++;
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
