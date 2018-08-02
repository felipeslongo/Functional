using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Felipeslongo.Functional.Monads.Tests
{
    class MaybeTests
    {
        [Fact]
        public void Bind_ExceptionToAnotherException()
        {
            var maybe = new Maybe<Exception>(new Exception())
                .Bind<ArgumentException>(e => new ArgumentException("", e));

            Assert.IsType<ArgumentException>(maybe.Value);
        }

        [Fact]
        public void Bind_NoneToOtherType_NullAndSafeNavigation()
        {
            var maybe = Maybe<Exception>.None()
                .Bind<ArgumentException>(e => new ArgumentException("", e));

            Assert.Null(maybe.Value);
        }

        [Fact]
        public void Constructor_HasValue_IsNotNull()
        {
            var maybe = new Maybe<Exception>(new Exception());

            Assert.True(maybe.HasValue);
            Assert.False(maybe.IsNull);
        }


        [Fact]
        public void None_IsNull_DoesNotHasValue()
        {
            var maybe = Maybe<Exception>.None();

            Assert.False (maybe.HasValue);
            Assert.True(maybe.IsNull);
        }

        [Fact]
        public void WhenNull_DoNullAction_DontDoValueAction()
        {
            var maybe = Maybe<Exception>.None();

            var nullActionExecuted = false;
            maybe.WhenNull(() => nullActionExecuted = true);
            Assert.True(nullActionExecuted);
            maybe.WhenHasValue(e => Assert.True(false));
        }

    }
}
