using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Collectable : IEquatable<Collectable>
{
    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// Color value.
    /// </summary>
    public string color;

    /// <summary>
    /// Point value.
    /// </summary>
    public int point;

    #endregion


    // ======================== Functional ====================== //

    #region Functional 

    /// <summary>
    /// Custom object comparing.
    /// </summary>
    public bool Equals(Collectable other)
    {
        if (other == null) return false;
        return StringComparer.Ordinal.Equals(this.color, other.color);
    }

    /// <summary>
    /// Custom object comparing.
    /// </summary>
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Collectable employee = (Collectable)obj;
        return StringComparer.Ordinal.Equals(this.color, employee.color);
    }

    /// <summary>
    /// Custom object comparing.
    /// </summary>
    public override int GetHashCode()
    {
        return this.color.GetHashCode() ^ this.point.GetHashCode();
    }

    /// <summary>
    /// Return this object.
    /// </summary>
    public Collectable GetCollectable()
    {
        return this;
    }

    #endregion
}
