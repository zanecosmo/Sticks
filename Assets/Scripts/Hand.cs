using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] int[] identity;

    void Update()
    {
        score.text = GameState.Players[identity[0], identity[1]].ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Has Entered");

        GameState.Hovered = identity;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision Has Exited");

        GameState.Hovered = null;
    }
}
