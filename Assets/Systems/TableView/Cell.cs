using MPUIKIT;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MPImage))]
public abstract class Cell<T> : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cellValue;

    [SerializeField]
    private MPImage image;

    [Header("Tween")]
    [SerializeField] bool useTween = true;
    [SerializeField] float colorTweenTime = .1f;
    [SerializeField] float textTweenTime = 2f;

    private void Reset()
    {
        image = GetComponent<MPImage>();
        cellValue = GetComponentInChildren<TMP_Text>();
    }

    public T Value
    {
        get
        {
            if (string.IsNullOrEmpty(cellValue.text))
                return default;

            return cellValue.text.Convert<T>();
        }

        set
        {
            if (value == null)
                cellValue.text = string.Empty;
            else
            {

                if (useTween)
                {
                    LeanTween.scale(cellValue.gameObject, Vector3.one, textTweenTime)
                        .setFrom(Vector3.one * .5f)
                        .setEaseOutBounce();
                }

                cellValue.text = value.ToString();
            }
        }
    }

    public void SetBackgroundColor(Color color)
    {
        if (useTween)
        {
            LeanTween.value(image.gameObject, (color, obj) =>
            {
                image.color = color;
            }, image.color, color, colorTweenTime)
                .setEaseInCirc();
        }
        else
        {
            image.color = color;
        }
    }
}
