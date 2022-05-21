using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class TileController : MonoBehaviour
{
    [ShowInInspector]
    [ReadOnly]
    protected int index;

    [SerializeField]
    protected int currentOwner;

    public GameObject tileObject;

    Coroutine flipAnimationCoroutine = null;
    public float flipTime;
    public GameObject flipVFX;
    public UnityEvent<int, int> flipEvent;

    public bool isBingo = false;

    public void Initialize(int index, int currentOwner)
    {
        this.index = index;
        this.currentOwner = currentOwner;
        tileObject.transform.localRotation = Quaternion.Euler(0, 0, currentOwner * 180);
    }

    public void SetBingo(bool isBingo)
    {
        this.isBingo = isBingo;
    }

    public int GetOwner()
    {
        return currentOwner;
    }

    public void Flip()
    {
        if (isBingo)
            return;

        Debug.Log($"Flip : {index}");

        ++currentOwner;
        currentOwner = currentOwner % 2;

        if (flipAnimationCoroutine != null)
            StopCoroutine(flipAnimationCoroutine);

        flipAnimationCoroutine = StartCoroutine("CoFlipAnimation");
        flipEvent?.Invoke(index, currentOwner);
    }

    IEnumerator CoFlipAnimation()
    {
        var animationTime = flipTime;
        var currentRotation = tileObject.transform.localRotation;
        var targetRotation = Quaternion.Euler(Vector3.forward * 180f * currentOwner);
        flipVFX.SetActive(true);
        while (animationTime > 0)
        {
            tileObject.transform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, 1f - (animationTime / flipTime));
            animationTime -= Time.deltaTime;
            yield return null;
        }
        tileObject.transform.localRotation = targetRotation;
        flipVFX.SetActive(false);

    }

}
