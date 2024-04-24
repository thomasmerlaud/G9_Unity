using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollSpeed = 1.0f; // Vitesse de défilement
    private float spriteHeight; // Hauteur du sprite de route

    private void Start()
    {
        // Récupérer la hauteur du sprite de route
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        // Déplacer le sprite de route le long de l'axe Y
        float newY = Mathf.Repeat(Time.time * scrollSpeed, spriteHeight);
        transform.position = new Vector2(transform.position.x, newY);
    }
}
