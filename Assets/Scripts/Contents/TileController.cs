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

    public UnityEvent<int, int> flipEvent;

    public void Initialize(int index, int currentOwner)
    {
        this.index = index;
        this.currentOwner = currentOwner;
        tileObject.transform.localRotation = Quaternion.Euler(0, 0, currentOwner * 180);
    }

    public void Flip()
    {
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
        var currentRotation = ((currentOwner + 1) % 2) * 180f;
        var targetRotation = currentOwner * 180f;

        var rotation = Quaternion.Euler(0, 0, 0);

        while (animationTime >= 0)
        {
            rotation.z = Mathf.Lerp(currentRotation, targetRotation, 1f - (flipTime / animationTime));
            tileObject.transform.localRotation = rotation;
            animationTime -= Time.deltaTime;
            yield return null;
        }

    }

}
