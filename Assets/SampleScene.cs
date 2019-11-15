using UnityEngine;
using UnityEngine.UI;

public class SampleScene : MonoBehaviour
{
    [SerializeField] private Text _text_ = null;

    private void Start()
    {
        _text_.text = "자동 빌드4";
    }
}
