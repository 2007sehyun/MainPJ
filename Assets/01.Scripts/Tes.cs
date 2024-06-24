using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tes : MonoBehaviour
{
    private void Start()
    {
        transform.DOJump(transform.position+new Vector3(4,7),10,1,4);

    }
}
