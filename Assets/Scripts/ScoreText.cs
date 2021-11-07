using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text youWonText;

    private int _totalTeleportPads;
    private int _usedTeleportPads;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        _totalTeleportPads = Helpers.GetTeleportPads().Length - 1;
        UpdateUiText();
    }

    public void ArrivedAtTeleportPad()
    {
        _usedTeleportPads++;
        UpdateUiText();
    }

    private void UpdateUiText()
    {
        _text.text = $"Score: {_usedTeleportPads:D2}/{_totalTeleportPads:D2}";

        if (_usedTeleportPads == _totalTeleportPads)
            youWonText.enabled = true;
    }
}