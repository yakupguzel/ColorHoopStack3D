using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject hareketPozisyonu;
    public GameObject[] soketler; // Stand üzerindeki halkalarin tam pozisyonlari
    public int bosOlanSoket; // uzerinde halka olmayan soket sirasi
    public List<GameObject> _Cemberler = new List<GameObject>();

    [SerializeField] private GameManager _GameManager;

    private int CemberTamamlamaSayisi;

    public GameObject EnUsttekiCemberiVer()
    {
        // en ustteki cemberi ver
        return _Cemberler[_Cemberler.Count - 1];
    }

    public GameObject MusaitSoketiVer()
    {
        return soketler[bosOlanSoket];
    }

    public void SoketDegistirmeIslemleri(GameObject silinecekObje)
    {

        _Cemberler.Remove(silinecekObje); // Listeden Kaldir
        if (_Cemberler.Count != 0)
        {
            bosOlanSoket--;
            _Cemberler[_Cemberler.Count - 1].GetComponent<Cember>().hareketEdebilirMi = true;
        }
        else
        {
            bosOlanSoket = 0;
        }
    }

    public bool CemberleriKontrolEt()
    {
        Colors currentColor = _Cemberler[0].GetComponent<Cember>().renk;

        if (_Cemberler.Count == 4)
        {
            foreach (var cember in _Cemberler)
            {
                if (currentColor == cember.GetComponent<Cember>().renk)
                {
                    CemberTamamlamaSayisi++;
                }
            }

            if (CemberTamamlamaSayisi == 4)
            {
                _GameManager.StandTamamlandi();
                return true;
            }
            else
            {
                CemberTamamlamaSayisi = 0;

            }
        }
        return false;
    }

    public void StandTamamlandi()
    {
        foreach (var cember in _Cemberler)
        {
            cember.GetComponent<Cember>().hareketEdebilirMi = false;

            Color32 color = cember.GetComponent<MeshRenderer>().material.GetColor("_Color");
            color.a = 150;
            cember.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            gameObject.tag = "TamamlanmisStand";
        }
        
    }
}
