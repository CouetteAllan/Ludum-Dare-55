using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class RadialBar : MonoBehaviour
{
    #region Show In Inspector
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _amount = 0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _thickness = 0f;

    [Header("Circles")]

    [SerializeField]
    private Image _radialFill;

    [SerializeField]
    private Color _radialColor;

    [SerializeField]
    private Image _centerBackground;

    [SerializeField]
    private Color _centerColor;
    #endregion


    #region Public Properties
    public float Amount
    {
        get => _amount;
        set => _amount = Mathf.Clamp(value, 0, 1f);
    }
    #endregion


    private void OnEnable()
    {
        _fillTransform = _radialFill.GetComponent<RectTransform>();
        _centerTransform = _centerBackground.GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdateColors();
        UpdateAmount();
        UpdateThickness();
    }

    #region Update Style
    private void UpdateAmount()
    {
        _radialFill.fillAmount = _amount;
    }

    private void UpdateColors()
    {
        _radialFill.color = _radialColor;
        _centerBackground.color = _centerColor;
    }

    private void UpdateThickness()
    {
        float ratio = 1f - _thickness;

        _centerTransform.sizeDelta = new Vector2(
            _fillTransform.rect.width * ratio,
            _fillTransform.rect.height * ratio
        );
    }
    #endregion


    #region Private Members
    private RectTransform _fillTransform;
    private RectTransform _centerTransform;
    #endregion
}
