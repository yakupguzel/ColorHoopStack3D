using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioManager audio;
    public Material[] hoopColor;
    GameObject seciliObje;
    GameObject seciliStand;
    Cember _Cember;

    public bool hareketVar;
    public int hedefStandSayisi;
    int tamamlananStandSayisi;



    private void Awake()
    {
        RandomizeHoopColors();
    }

    private void RandomizeHoopColors()
    {
        Cember[] tempList = FindObjectsOfType<Cember>();
        List<Cember> cembers = new List<Cember>();
        for (int i = 0; i < tempList.Length; i++)
        {
            cembers.Add(tempList[i]);
        }

        cembers.Shuffle();
        int colorIndex = 0;
        int colorFactor = 1;
        foreach (Cember cember in cembers)
        {
            cember.GetComponent<Renderer>().material.SetColor("_Color", hoopColor[colorIndex].color);
            cember.renk = (Colors)colorIndex;
            colorFactor++;
            if (colorFactor > 4)
            {
                colorFactor = 1;
                colorIndex++;
            }
        }
        cembers.Shuffle();
    }

    private void Update()
    {
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
                    }
                }
            }
        }
    }
}
