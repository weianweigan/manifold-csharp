namespace ManifoldNET;

public partial class Manifold
{
    /// <summary>
    /// Overloaded + operator to perform a boolean addition operation on two <see cref="Manifold"/> objects.
    /// </summary>
    /// <param name="left">The left operand, a Manifold object.</param>
    /// <param name="right">The right operand, a Manifold object.</param>
    /// <returns>Returns a new Manifold object that is the result of the boolean addition operation.</returns>
    public static Manifold operator +(Manifold left, Manifold right)
    {
        return BooleanOperation(left, right, BoolOperationType.Add);
    }

    /// <summary>
    /// Overloaded - operator to perform a boolean subtraction operation on two <see cref="Manifold"/> objects.
    /// </summary>
    /// <param name="left">The left operand, a Manifold object.</param>
    /// <param name="right">The right operand, a Manifold object.</param>
    /// <returns>Returns a new Manifold object that is the result of the boolean subtraction operation.</returns>
    public static Manifold operator -(Manifold left, Manifold right)
    {
        return BooleanOperation(left, right, BoolOperationType.Subtract);
    }

    /// <summary>
    /// Overloaded &amp; operator to perform a boolean intersection operation on two Manifold objects.
    /// </summary>
    /// <param name="left">The left operand, a Manifold object.</param>
    /// <param name="right">The right operand, a Manifold object.</param>
    /// <returns>Returns a new Manifold object that is the result of the boolean intersection operation.</returns>
    public static Manifold operator &(Manifold left, Manifold right)
    {
        return BooleanOperation(left, right, BoolOperationType.Intersect);
    }

    /// <summary>
    /// Overloaded ^ operator to perform a boolean intersection operation on two Manifold objects.
    /// </summary>
    /// <remarks>
    /// Same as <see cref="operator &amp;(Manifold, Manifold)"/>
    /// </remarks>
    /// <param name="left">The left operand, a Manifold object.</param>
    /// <param name="right">The right operand, a Manifold object.</param>
    /// <returns>Returns a new Manifold object that is the result of the boolean intersection operation.</returns>
    public static Manifold operator ^(Manifold left, Manifold right)
    {
        return BooleanOperation(left, right, BoolOperationType.Intersect);
    }
}
