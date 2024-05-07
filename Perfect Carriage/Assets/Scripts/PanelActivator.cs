using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelActivator : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    public MovingDirection MovingDirectionShow;

    public MovingDirection MovingDirectionClose;

    public CanvasScaler MyCanvasScaler;

    public float DurationMove;

    public RectTransform BlockScreen;

    public System.Action OnShowPanel;

    public Dictionary<MovingDirection, Vector2> movingVectors = new Dictionary<MovingDirection, Vector2>()
    {
        {MovingDirection.up, new Vector2(0,1)},
        {MovingDirection.right, new Vector2(1,0)},
        {MovingDirection.down, new Vector2(0,-1)},
        {MovingDirection.left, new Vector2(-1,0)}

    };

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        
    }

    public void Activate(bool state)
    {
        if(state)
        {
            Show(MovingDirectionShow);
        }
        else
        {
            Close(MovingDirectionClose);

        }
    }

    public void Show(MovingDirection dir)
    {
        if(_rectTransform.gameObject.activeInHierarchy)
        {
            return;
        }

        _rectTransform.anchoredPosition = MyCanvasScaler.referenceResolution * movingVectors[dir];

        _rectTransform.gameObject.SetActive(true);

        DOTween.Sequence()
            .AppendCallback(() => BlockScreen.gameObject.SetActive(true))
            .Append(_rectTransform.DOLocalMove(Vector2.zero, DurationMove))
            .AppendCallback(() => BlockScreen.gameObject.SetActive(false))
            .AppendCallback(() => OnShowPanel?.Invoke());

    }

    public void Close(MovingDirection dir)
    {
        if (!_rectTransform.gameObject.activeInHierarchy)
        {
            return;
        }

        _rectTransform.anchoredPosition = Vector2.zero * movingVectors[dir];

        DOTween.Sequence()
            .AppendCallback(() => BlockScreen.gameObject.SetActive(true))
            .Append(_rectTransform.DOLocalMove(MyCanvasScaler.referenceResolution * movingVectors[dir], DurationMove))
            .AppendCallback(() => BlockScreen.gameObject.SetActive(false))
            .AppendCallback(() => _rectTransform.gameObject.SetActive(false));

        _rectTransform.anchoredPosition = Vector2.zero * movingVectors[dir];


    }
}

public enum MovingDirection
{
    up,
    right,
    down,
    left
}
