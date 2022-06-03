using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject seciliObje;
    GameObject seciliStand;
    Cember _Cember;

    public bool hareketVar;
    public int hedefStandSayisi;
    int tamamlananStandSayisi;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (seciliObje != null && seciliStand != hit.collider.gameObject)
                    { // bir halka secilmis ise ve seciliStand ilk secilen halkanin kendisi degil baska bir halka ise halkayi g�nder

                        Stand stand = hit.collider.GetComponent<Stand>();

                        seciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(seciliObje);

                        _Cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, stand.MusaitSoketiVer(), stand.hareketPozisyonu);

                        stand.bosOlanSoket++;
                        stand._Cemberler.Add(seciliObje);

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

                        if (_Cember.hareketEdebilirMi)
                        {
                            _Cember.HareketEt("Secim", null, null, _Cember._aitOlduguStand.GetComponent<Stand>().hareketPozisyonu);
                            seciliStand = _Cember._aitOlduguStand;
                        }
                    }
                }
            }
        }
    }
}
