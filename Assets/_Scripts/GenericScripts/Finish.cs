using UnityEngine;

public class Finish : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.LevelComplete();
        }
    }
}
