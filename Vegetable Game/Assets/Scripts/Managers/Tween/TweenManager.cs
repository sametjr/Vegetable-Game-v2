using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    private static Vector3 goldStartPos;
    private static Vector3 goldScaleTween;
    private static Vector3 goldRotateTween;
    private static float goldTweenDuration = 1f;
    private static float failTweenDuration = 1.2f;
    private static float successTweenDuration = 1f;
    private void Start() {
        goldStartPos = new Vector3(-2.41f, 4.64f, 0);
        goldScaleTween = new Vector3(.2f, .2f, .2f);
        goldRotateTween = new Vector3(360, 360, 360);
    }

    public static void FailTween(GameObject _obj, Vector3 _pos)
    {
        _pos.x = GameManager.Instance.GetRandomXFromPan();
        LeanTween.move(_obj, _pos, failTweenDuration).setEasePunch();
    }

    public static void SuccessTween(GameObject _obj, Vector3 _pos)
    {
        _pos.x = GameManager.Instance.GetRandomXFromPan();
        LeanTween.move(_obj, _pos, successTweenDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public static void GoldTween(GameObject _gold)
    {
        Vector3 _pos = GameManager.Instance.goldSectionPosition;
        _pos.z = -7f;

        LeanTween.scale(_gold, goldScaleTween, goldTweenDuration).setEaseInOutBack();


        LeanTween.rotate(_gold, goldRotateTween, goldTweenDuration).setEaseInOutQuad();


        LeanTween.move(_gold, _pos, goldTweenDuration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => {
            _gold.SetActive(false);
        });


    }

    public static void VegetableDestroyTween(GameObject _veg)
    {
        LeanTween.moveY(_veg, -30f, 2f).setEaseOutQuad().setOnComplete(() => Destroy(_veg));
    }

    public static void NavbarChangeAnimation(GameObject _navbar)
    {
        LeanTween.rotateAround(_navbar, Vector3.left, 360f, .6f);
    }

    
}
