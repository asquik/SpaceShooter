using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class UIController : MonoBehaviour
{
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var _button in buttons)
        {
            _button.transform.GetChild(1).gameObject.SetActive(false);

            _button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    TurnOffDescriptionExcept(_button);

                    //Toggle description
                    GameObject __description = _button.transform.GetChild(1).gameObject;
                    __description.SetActive(!__description.activeSelf);

                }).AddTo(this);
        }
    }

    private void TurnOffDescriptionExcept(Button activeButton)
    {
        foreach (var _button in buttons)
        {
            if (_button !=activeButton)
            _button.transform.GetChild(1).gameObject.SetActive(false);
        }
    }


}
