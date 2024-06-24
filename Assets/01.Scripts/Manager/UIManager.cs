using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System;

public class UIManager : MonoBehaviour
{
    public Health health;
    public GameMananegr Gamemanager;
    public GameObject InfoPanel;
    public GameObject victory;

    private void OnEnable()
    {
        if (health != null)
        {
            health.OnDie += Info;
        }
        if(Gamemanager !=null)
        {
            Gamemanager.action += Victory;
        }
    }

    private void Victory()
    {
        victory.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Info()
    {
        if (health== null)
        {
            InfoPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            StartCoroutine(Delay());
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Close()
    {
        InfoPanel.SetActive(false);
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnDie -= Info;
        }
        if (Gamemanager != null)
        {
            Gamemanager.action -= Victory;
        }

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        InfoPanel.SetActive(true);

    }

}
