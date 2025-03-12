using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform coinTextContainer;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private Ease animationCurve;

    private float containerInitPosition;
    private float moveAmount;

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        toUpdate.SetText("0");

        containerInitPosition = coinTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }

    public void UpdateScore(int score)
    {
        toUpdate.SetText($"{score}");
        coinTextContainer.DOLocalMoveY(containerInitPosition + moveAmount,duration).SetEase(animationCurve);
        coinTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration)
            .OnComplete(() => StartCoroutine(ResetCoinContainer(score)));
    }

    private System.Collections.IEnumerator ResetCoinContainer(int score)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}");
        coinTextContainer.localPosition = new Vector3(coinTextContainer.localPosition.x, containerInitPosition, coinTextContainer.localPosition.z);
    }
}
