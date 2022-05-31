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
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (seciliObje != null && seciliStand != hit.collider.gameObject)
                    {
                        // cemberi diger standa gonder
                    }
                    else
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>();
                        seciliObje = _Stand.EnUsttekiCemberiVer();
                        _Cember = seciliObje.GetComponent<Cember>();
                        hareketVar = true;

                        if (_Cember.hakeretEdebilirMi)
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
