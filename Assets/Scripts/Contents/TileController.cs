using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class TileController : MonoBehaviour
{
    [SerializeField]
    private TileBingoData tileBingoData;

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
    public AudioClip flipSFX;

    public GameObject selectVFX;

    public bool isBingo = false;
    public GameObject bingoVFX;
    public Transform bingoTilePoint;

    Coroutine fillAnimationCoroutine = null;
    public float fillTime;

    public void Initialize(int index, int currentOwner)
    {
        this.index = index;
        this.currentOwner = currentOwner;
        tileObject.transform.localRotation = Quaternion.Euler(0, 0, currentOwner * 180);
    }

    public void SetBingo(bool isBingo)
    {
        if (this.isBingo)
            return;

        this.isBingo = isBingo;
        CreateBingoTile();
        bingoVFX.SetActive(isBingo);
    }

    public int GetOwner()
    {
        return currentOwner;
    }

    public void Select(bool isSelect)
    {
        selectVFX.SetActive(isSelect);
    }

    public void Flip()
    {
        selectVFX.SetActive(false);

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

    public void FillScale()
    {
        if (fillAnimationCoroutine != null)
            StopCoroutine(fillAnimationCoroutine);

        flipAnimationCoroutine = StartCoroutine("CoFillAnimation");
    }

    private void CreateBingoTile()
    {
        var bingoTileGo = Instantiate(tileBingoData.GetRandomTilePrefab(currentOwner), bingoTilePoint);
        bingoTileGo.transform.localRotation = Quaternion.Euler(0, 180 * Random.Range(0, 2), 0);
    }

    IEnumerator CoFlipAnimation()
    {
        var animationTime = flipTime;
        var currentRotation = tileObject.transform.localRotation;
        var targetRotation = Quaternion.Euler(Vector3.forward * 180f * currentOwner);
        ObjectPoolManager.Instance.Get(flipVFX.name).transform.position = transform.position;
        SoundManager.Instance.PlaySFX(flipSFX);
        while (animationTime > 0)
        {
            tileObject.transform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, 1f - (animationTime / flipTime));
            animationTime -= Time.deltaTime;
            yield return null;
        }
        tileObject.transform.localRotation = targetRotation;
    }

    IEnumerator CoFillAnimation()
    {
        var animationTime = fillTime;
        var currentScale = tileObject.transform.localScale;
        var targetScale = Vector3.one;
        while (animationTime > 0)
        {
            tileObject.transform.localScale = Vector3.Lerp(currentScale, targetScale, 1f - (animationTime / flipTime));
            animationTime -= Time.deltaTime;
            yield return null;
        }
        tileObject.transform.localScale = targetScale;
    }

}
