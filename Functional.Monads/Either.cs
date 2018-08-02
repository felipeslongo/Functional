using System;

namespace Felipeslongo.Functional.Monads
{
    using System;

    /// <summary>
    /// Functional data data to represent a discriminated
    /// union of two possible types.
    /// </summary>
    /// <typeparam name="TL">Type of "Left" item.</typeparam>
    /// <typeparam name="TR">Type of "Right" item.</typeparam>
    /// <seealso cref="https://github.com/mikhailshilkov/mikhailio-samples/blob/master/Either%7BTL%2CTR%7D.cs">Original developer, credits to him!!</seealso>
    /// <seealso cref="https://mikhail.io/2016/01/validation-with-either-data-type-in-csharp/"/>
    public class Either<TL, TR>
    {
        private readonly bool isLeft;
        private readonly TL left;
        private readonly TR right;

        public Either(TL left)
        {
            this.left = left;
            this.isLeft = true;
        }

        public Either(TR right)
        {
            this.right = right;
            this.isLeft = false;
        }

        /// <summary>
        /// Wraps a left instance
        /// </summary>
        /// <param name="left"></param>
        public static implicit operator Either<TL, TR>(TL left) => new Either<TL, TR>(left);

        /// <summary>
        /// Wraps a right instance
        /// </summary>
        /// <param name="right"></param>
        public static implicit operator Either<TL, TR>(TR right) => new Either<TL, TR>(right);

        /// <summary>
        /// If right value is assigned, execute an action on it.
        /// </summary>
        /// <param name="rightAction">Action to execute.</param>
        public void DoRight(Action<TR> rightAction)
        {
            if (rightAction == null)
            {
                throw new ArgumentNullException(nameof(rightAction));
            }

            if (!this.isLeft)
            {
                rightAction(this.right);
            }
        }
        /// <summary>
        /// If left value is assigned, execute an action on it.
        /// </summary>
        /// <param name="leftAction">Action to execute.</param>
        public void DoLeft(Action<TL> leftAction)
        {
            if (leftAction == null)
            {
                throw new ArgumentNullException(nameof(leftAction));
            }

            if (this.isLeft)
            {
                leftAction(this.left);
            }
        }


        /// <summary>
        /// Gets the Left instance or a default value is not set
        /// </summary>
        /// <returns><see cref="TL"/> instance</returns>
        public TL LeftOrDefault() => this.Match(l => l, r => default(TL));

        /// <summary>
        /// Concept of pattern matching implemented in C# world.
        /// If a left value is specified, Match will return the result of the left function, otherwise the result of the right function.
        /// </summary>
        /// <typeparam name="T">Right and Left functions mutual result.</typeparam>
        /// <param name="leftFunc">Function that return a Left instance</param>
        /// <param name="rightFunc">Function that return a Right instance</param>
        /// <returns></returns>
        public T Match<T>(Func<TL, T> leftFunc, Func<TR, T> rightFunc)
        {
            if (leftFunc == null)
            {
                throw new ArgumentNullException(nameof(leftFunc));
            }

            if (rightFunc == null)
            {
                throw new ArgumentNullException(nameof(rightFunc));
            }

            return this.isLeft ? leftFunc(this.left) : rightFunc(this.right);
        }

        /// <summary>
        /// Gets the Right instance or a default value is not set
        /// </summary>
        /// <returns><see cref="TR"/> instance</returns>
        public TR RightOrDefault() => this.Match(l => default(TR), r => r);
    }
}