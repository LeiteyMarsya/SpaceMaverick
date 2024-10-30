using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GameController))]
public class ScoreOverlayController : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI Score { get; set; }

    private GameController _gameController { get; set; }

    public void UpdateScore (int score)
    {
        Score.text = score.ToString().PadLeft(8, '0');
    }
}
