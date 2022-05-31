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
        return _Cemberler[_Cemberler.Count-1];
    }
}
