using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCanvasController : MonoBehaviour
{
    private Camera mainCamera;
    float weaponUpgradeTextTime = 1.25f;
    Transform weaponUpgradeTextObject;
    Sequence moveTextSequence;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                            mainCamera.transform.rotation * Vector3.up);

    }

    void Start()
    {
        
    }
    public void ClearColor(TextMeshProUGUI tempText)
    {
        Color tempColor = tempText.color;
        tempColor = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
        tempText.color = tempColor;
    }

    public void ShowWeaponUpgradeText(TextMeshProUGUI text)
    {
        Transform textTransform = text.transform;

        Transform tempTextObject = Instantiate
            (textTransform,
             textTransform.position,
             textTransform.rotation,
             this.transform);
        Destroy(tempTextObject.gameObject, 1.1f * weaponUpgradeTextTime);

        
        tempTextObject.localScale = Vector3.zero;

        moveTextSequence = DOTween.Sequence().SetAutoKill().Play().OnComplete(RewindSequence);

        moveTextSequence.Append(
            tempTextObject.DOLocalMoveY(80, 0.5f * weaponUpgradeTextTime).
            SetEase(Ease.InOutSine));

        moveTextSequence.Join(
            tempTextObject.DOScale(1.5f, 0.5f * weaponUpgradeTextTime).
            SetEase(Ease.OutQuart));

        moveTextSequence.Join(
            tempTextObject.GetComponent<TextMeshProUGUI>().DOFade(1, 0.5f * weaponUpgradeTextTime).
            SetEase(Ease.InOutSine));

        moveTextSequence.Append(
             tempTextObject.DOLocalMoveY(200, 0.5f * weaponUpgradeTextTime).
            SetEase(Ease.InOutSine));

        moveTextSequence.Join(tempTextObject.GetComponent<TextMeshProUGUI>().DOFade(0, 0.5f * weaponUpgradeTextTime).
           SetEase(Ease.InExpo));
    }

    void RewindSequence()
    {
        //moveTextSequence.Rewind();
    }

    private void OnDestroy()
    {
        moveTextSequence.Kill();
    }
}
