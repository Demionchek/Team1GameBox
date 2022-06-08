using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollsActivator : MonoBehaviour
{
    [SerializeField] private Saver _saver;
    [SerializeField] private GameObject scroll1;
    [SerializeField] private GameObject scroll2;
    [SerializeField] private GameObject scroll3;
    [SerializeField] private GameObject scroll4;

    void Start()
    {
        _saver.LoadScrollsNums();
        if (_saver.FirstScroll == 1)
            scroll1.SetActive(false);
        if (_saver.SecondScroll == 1)
            scroll2.SetActive(false);
        if (_saver.ThirdScroll == 1)
            scroll3.SetActive(false);
        if(_saver.FourthScroll == 1)
            scroll4.SetActive(false);
    }
}
