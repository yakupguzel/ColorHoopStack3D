using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cember : MonoBehaviour
{
    public GameObject _aitOlduguStand;
    public GameObject _aitOlduguCemberSoketi;
    public bool hareketEdebilirMi;
    public Colors renk;
    public GameManager gameManager;

    GameObject hareketPozisyonu;
    GameObject gidecegiStand;

    bool secildi, pozisyonDegistir, soketeOtur, soketeGeriGit;


    public void HareketEt(ActionState state, GameObject stand = null, GameObject soket = null, GameObject gidilecekObje = null)
    {
        switch (state)
        {
            case ActionState.Choosen:
                hareketPozisyonu = gidilecekObje;
                secildi = true;
                break;
            case ActionState.ShiftPosition:
                gidecegiStand = stand;
                _aitOlduguCemberSoketi = soket;
                hareketPozisyonu = gidilecekObje;
                pozisyonDegistir = true;
                break;
            case ActionState.BackToLastSocket:
                soketeGeriGit = true;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (secildi)
        {
            transform.position = Vector3.Lerp(transform.position, hareketPozisyonu.transform.position, 10f * Time.deltaTime);

            if (Vector3.Distance(transform.position, hareketPozisyonu.transform.position) < .05f)
            {
                secildi = false;
            }
        }

        if (pozisyonDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, hareketPozisyonu.transform.position, 10f * Time.deltaTime);

            if (Vector3.Distance(transform.position, hareketPozisyonu.transform.position) < .05f)
            {
                pozisyonDegistir = false;
                soketeOtur = true;
            }
        }

        if (soketeOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _aitOlduguCemberSoketi.transform.position, 10f * Time.deltaTime);

            if (Vector3.Distance(transform.position, _aitOlduguCemberSoketi.transform.position) < .05f)
            {
                transform.position = _aitOlduguCemberSoketi.transform.position;

                soketeOtur = false;

                _aitOlduguStand = gidecegiStand;

                if (_aitOlduguStand.GetComponent<Stand>()._Cemberler.Count > 1)
                {
                    Stand _stand = _aitOlduguStand.GetComponent<Stand>();

                    _stand._Cemberler[_stand._Cemberler.Count - 2].GetComponent<Cember>().hareketEdebilirMi = false;


                }
                gameManager.hareketVar = false;
            }
        }

        if (soketeGeriGit)
        {
            transform.position = Vector3.Lerp(transform.position, _aitOlduguCemberSoketi.transform.position, 10f * Time.deltaTime);

            if (Vector3.Distance(transform.position, _aitOlduguCemberSoketi.transform.position) < .05f)
            {
                transform.position = _aitOlduguCemberSoketi.transform.position;

                soketeGeriGit = false;
                gameManager.hareketVar = false;
            }
        }
    }
}
