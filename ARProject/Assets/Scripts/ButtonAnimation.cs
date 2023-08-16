using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOScale(Vector3.zero, .3f).SetEase(Ease.OutBack).From();
    }
}
