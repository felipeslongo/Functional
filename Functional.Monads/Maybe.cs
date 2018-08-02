using System;
using System.Collections.Generic;
using System.Text;

namespace Felipeslongo.Functional.Monads
{
    /// <summary>
    /// Functional-first language F# typically doesn't allow null for its types. 
    /// Instead, F# has a maybe implementation built into the language: it's called option type.
    /// </summary>
    /// <typeparam name="T">Wrapped type</typeparam>
    /// <seealso cref="https://mikhail.io/2018/07/monads-explained-in-csharp-again/">Credits to Mikhail Shilkov,</seealso>
    public class Maybe<T> where T : class
    {
        public T Value { get; private set; }
        public bool HasValue => !IsNull;
        public bool IsNull => Value == null;

        public Maybe(T someValue)
        {
            this.Value = someValue ?? throw new ArgumentNullException(nameof(someValue));
        }

        private Maybe()
        {
        }

        public Maybe<U> Bind<U>(Func<T, Maybe<U>> func) where U : class
        {
            return Value != null ? func(Value) : Maybe<U>.None();
        }

        public static Maybe<T> None() => new Maybe<T>();

        public static implicit operator Maybe<T>(T arg) => new Maybe<T>(arg);

        public void WhenNull(Action action)
        {
            if(action == null)
                throw new ArgumentNullException(nameof(action));

            if (IsNull)
                action();
        }

        public void WhenHasValue(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (HasValue)
                action(Value);
        }

    }
}
