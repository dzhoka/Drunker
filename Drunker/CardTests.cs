using System;
using NUnit.Framework;
namespace Drunker
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void MatchTest()
        {
            Card t3 = new Card("T", 3);
            Card h2 = new Card("H", 3);
            Card t7 = new Card("T", 7);

            Assert.True(t3.Match(h2));
            Assert.True(t3.Match(t7));
            Assert.False(h2.Match(t7));
        }
    }
}
