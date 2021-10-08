using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Definizione di un powerup
/// </summary>
public abstract class PowerUpDefinition
{
    /// <summary>
    /// Durata del power up
    /// </summary>
    public float Duration = 1;

    /// <summary>
    /// Coroutine interna
    /// </summary>
    public Coroutine InternalCoroutine;

    /// <summary>
    /// Evento lanciato all'attivazione
    /// </summary>
    public abstract void Activate();

    /// <summary>
    /// Evento lanciato alla deattivazione
    /// </summary>
    public abstract void Deactivate();

    /// <summary>
    /// Funzione di attesa
    /// </summary>
    /// <returns></returns>
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(Duration);
        Deactivate();
    }

}