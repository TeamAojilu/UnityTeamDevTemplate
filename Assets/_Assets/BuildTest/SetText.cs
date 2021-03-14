using System;
using System.Text;
using SilCilSystem.Variables;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SetText : MonoBehaviour
{
    [SerializeField] private VariableString stringAsset;
    [SerializeField] private Text text;

    private void Start()
    {
        text.text = stringAsset.Value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stringAsset.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(Random.Range(-1000, 1000).ToString()));
            text.text = stringAsset.Value;
        }                
    }
}
