using TMPro;
using UnityEngine;

[FromFactory("ScoreDisplay",true)]
public class ScoreDisplay : MonoBehaviour, IInjectable<LevelDistance>
{
    public TextMeshProUGUI DistanceText;
    public TextMeshProUGUI ScoreText;

    public LevelDistance levelDistance;

    public void Inject(LevelDistance dependency)
    {
        levelDistance = dependency;
    }

    private void Update()
    {
        DistanceText.text = levelDistance.Distance.ToString(); 
    }
}
