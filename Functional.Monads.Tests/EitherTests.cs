using System;
using Xunit;

namespace Felipeslongo.Functional.Monads.Tests
{
    public class EitherTests
    {
        private enum EitherParam
        {
            Left,
            Right
        }

        [Fact]
        public void Match_WhenLeft_ThenExecutesLeftFunc()
        {
            var either = new Either<Exception, bool>(new Exception());
            var actual = either.Match<object>(l => EitherParam.Left, r => EitherParam.Right);
            Assert.Equal(EitherParam.Left, actual);
        }

        [Fact]
        public void Match_WhenRight_ThenExecutesRightFunc()
        {
            var either = new Either<Exception, bool>(true);
            var actual = either.Match<object>(l => EitherParam.Left, r => EitherParam.Right);
            Assert.Equal(EitherParam.Right, actual);
        }

        [Fact]
        public void WhenRight_DoRight_DontDoLeft()
        {
            var either = new Either<Exception, bool>(true);
            either.DoRight(r => Assert.True(r));
            either.DoLeft(l => Assert.True(false, "Left was executed."));
        }

        [Fact]
        public void WhenLeft_DoLeft_DontDoRight()
        {
            var either = new Either<Exception, bool>(new Exception());
            either.DoLeft(r => Assert.IsType<Exception>(r));
            either.DoRight (l => Assert.True(false, "Right was executed."));
        }


    }
}
