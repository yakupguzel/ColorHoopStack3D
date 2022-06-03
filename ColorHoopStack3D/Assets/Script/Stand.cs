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
}
