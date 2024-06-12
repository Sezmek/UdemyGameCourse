using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;

    [SerializeField] Material hitMaterial;
    private Material orginalMaterial;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        orginalMaterial = spriteRenderer.material;
    }

    IEnumerator FlashFX()
    {
        spriteRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = orginalMaterial;
    }
}
