using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandom : MonoBehaviour
{
    // Define four colors
    Color color1 = new Color(1, 98 / 255f, 0);  // Orange
    Color color2 = new Color(0, 1, 1);          // Cyan
    Color color3 = new Color(1, 0, 1);          // Magenta
    Color color4 = new Color(0, 1, 0);          // Green

    // SpriteRenderer components for three child objects
    SpriteRenderer sr1;
    SpriteRenderer sr2;
    SpriteRenderer sr3;

    void Start()
    {
        // Get SpriteRenderer components from child objects
        sr1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
        sr3 = transform.GetChild(2).GetComponent<SpriteRenderer>();

        // Create an array of colors
        Color[] colors = { color1, color2, color3, color4 };

        // Shuffle the color array
        Shuffle(colors);

        // Assign the first three shuffled colors to the child objects
        sr1.color = colors[0];
        sr2.color = colors[1];
        sr3.color = colors[2];
    }

    private void Update()
    {
        try
        {
            // Check if none of the child objects' colors match the player's color
            if (sr1.color != PlayerBall.Instance.GetComponent<SpriteRenderer>().color &&
                sr2.color != PlayerBall.Instance.GetComponent<SpriteRenderer>().color &&
                sr3.color != PlayerBall.Instance.GetComponent<SpriteRenderer>().color)
            {
                // If no match, set the first child's color to the player's color
                sr1.color = PlayerBall.Instance.GetComponent<SpriteRenderer>().color;
            }
        }
        catch
        {
            // Catch and ignore any exceptions (e.g., if PlayerBall.Instance is null)
        }
    }

    // Fisher-Yates shuffle algorithm to randomize the color array
    void Shuffle(Color[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            Color temp = array[i];
            array[i] = array[r];
            array[r] = temp;
        }
    }
}