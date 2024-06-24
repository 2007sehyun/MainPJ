using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananegr : MonoBehaviour
{
    private static GameMananegr instance = null;

    public List<Target> target;
    Target[] targets;
    public Action action;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameMananegr Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
  
    private void Start()
    {
        targets = FindObjectsOfType<Target>();
        foreach (Target target in targets)
        {
            this.target.Add(target);
        }
    }

    private void Update()
    {
        if (target.Count !=0) return;

        action?.Invoke();
    }
}
