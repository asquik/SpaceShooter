using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class DescriptionView : MonoBehaviour
{
    private LevelInfo levelInfo;
    public Text _state;
    public Text _spawnRate;
    public Text _speedDeduction;
    public Text _fireRate;
    // Start is called before the first frame update

    private void OnEnable()
    {
        levelInfo = transform.parent.GetComponent<LevelInfo>();

        if (levelInfo.isPassed)
        {
            _state.text = "Passed";
        }
        else if (levelInfo.isCreated)
        {
            _state.text = "Open";
        }
        else
        {
            _state.text = "Closed";
            _spawnRate.gameObject.SetActive(false);
            _speedDeduction.gameObject.SetActive(false);
            _fireRate.gameObject.SetActive(false);
            return;
        }

        _spawnRate.gameObject.SetActive(true);
        _speedDeduction.gameObject.SetActive(true);
        _fireRate.gameObject.SetActive(true);
        _spawnRate.text = " Spawn rate (ms): " + levelInfo.spawnRate.ToString();
        _speedDeduction.text = "Speed deduction: " + Format(levelInfo.speedDeduction) + "%";
        _fireRate.text = " Fire rate (ms): " + levelInfo.fireRate.ToString();
    }

    private string Format(float number)
    {
        
        return string.Format("{0:0}", number);
    }

}
