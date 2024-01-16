using UnityEngine.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Gate Charger / Point Data", fileName = "Point Data")]
public class PointSettings : ScriptableObject
{
    [SerializeField, FormerlySerializedAs("Ball point for bronze ball")] int bronzeBallPoint = 10;
    [SerializeField, FormerlySerializedAs("Ball point for silver ball")] int silverBallPoint = 20;
    [SerializeField, FormerlySerializedAs("Ball point for gold ball")] int goldBallPoint = 30;

    public int BronzeBallPoint { get => bronzeBallPoint; set => bronzeBallPoint = value; }
    public int SilverBallPoint { get => silverBallPoint; set => silverBallPoint = value; }
    public int GoldBallPoint { get => goldBallPoint; set => goldBallPoint = value; }
}
