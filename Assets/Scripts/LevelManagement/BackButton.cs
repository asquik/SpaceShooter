using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                transform.parent.gameObject.SetActive(false);
            }).AddTo(this);
    }

}
