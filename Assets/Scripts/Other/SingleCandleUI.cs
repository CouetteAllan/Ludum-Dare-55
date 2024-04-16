using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleCandleUI : MonoBehaviour
{
    [SerializeField] private LayoutElement _layout;
    [SerializeField] private Image _candleImage;

    public void Bounce()
    {
        //_layout.ignoreLayout = true;
        LeanTween.moveY(this.gameObject, 200.0f, 1.0f).setEasePunch().setOnComplete(() => _layout.ignoreLayout = false);
    }

    public void ChangeSprite(Sprite sprite)
    {
        _candleImage.sprite = sprite;
    }
}
