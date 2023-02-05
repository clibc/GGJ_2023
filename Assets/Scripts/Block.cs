using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] SpriteRenderer Renderer;
    [SerializeField] Sprite[] Sprites;
    int Health = 3;

    int SpriteIndex => Mathf.Clamp(Health-1, 0, 2);

    void Start()
    {
        Renderer.sprite = Sprites[SpriteIndex];
    }

    public void Hit()
    {
        Health -= 1;

        if(Health <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        Renderer.sprite = Sprites[SpriteIndex];
    }

    public void Highligt(bool Value)
    { 
        Renderer.color = Value ? Color.white * 0.9f : Color.white;
    }
}
