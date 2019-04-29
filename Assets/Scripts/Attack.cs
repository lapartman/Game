using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float timeBetweenAttacks;
    public Transform slashPosition;

    protected Animator animator;
    protected PlayerMovement player;
    protected Health health;
    protected AudioSource audioSource;
    protected float attackTimer;

    /// <summary>
    /// Támadást adja meg.
    /// </summary>
    protected abstract void Slash();

    /// <summary>
    /// Megadja a támadás feltételeit.
    /// </summary>
    /// <returns>Ha igazzal tér vissza, akkor lehet támadásba lendülni.</returns>
    protected abstract bool SlashCondition();

    /// <summary>
    /// Beállítja a támadás pozícióját a karaktertől relatívan, és a helyes irányba, attól függően, melyik irányba néz a karakter.
    /// </summary>
    /// <param name="facingRight">A karakter éppen jobbra néz-e.</param>
    public abstract void SetSlashPosition(bool facingRight);

    /// <summary>
    /// Lejátszik egy AudioClip tömbből egy véletlenszerű hangot, paraméterben átadva.
    /// </summary>
    /// <param name="audioClips">AudioClip tömb</param>
    /// <returns>Visszatér az AudioClip tömbben található véletlen elemmel</returns>
    protected AudioClip PlayRandomSound(AudioClip[] audioClips)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        return audioClips[randomIndex];
    }
}