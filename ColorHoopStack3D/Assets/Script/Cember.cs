using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cember : MonoBehaviour
{
    public GameObject _aitOlduguStand;
    public GameObject _aitOlduguCemberSoketi;
    public bool hakeretEdebilirMi;
    public string renk;
    public GameManager gameManager;

    GameObject hareketPozisyonu;
    GameObject aitOlduguStand;

    bool secildi, pozisyonDegistir, soketOtur, soketeGeriGit;

    public void HareketEt(string islem, GameObject stand = null, GameObject soket = null, GameObject gidilecekObje = null)
    {
        switch (islem)
        {
            case "Secim":
                hareketPozisyonu = gidilecekObje;
                secildi = true;
                break;
            case "PozisyonDegistir":
                break;
            case "SoketeOtur":
                break;
            case "SoketeGeriGit":
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (secildi)
        {
            transform.position = Vector3.Lerp(transform.position, hareketPozisyonu.transform.position, .2f);

            if (Vector3.Distance(transform.position,hareketPozisyonu.transform.position)<.10f)
            {
                secildi = false;
            }
        }
    }
}
