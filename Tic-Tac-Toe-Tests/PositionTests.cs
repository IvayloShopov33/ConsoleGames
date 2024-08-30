using Tic_Tac_Toe.GameBoard;

namespace Tic_Tac_Toe_Tests
{
    [TestFixture]
    public class PositionTests
    {
        private Position position;

        [SetUp]
        public void SetUp()
        {
            this.position = new Position("4, 4");
        }

        [Test]
        public void Constructor_ShouldWorkProperly()
        {
            Assert.AreEqual(4, this.position.Row);
            Assert.AreEqual(4, this.position.Column);
        }

        [Test]
        public void EqualsMethod_ShouldReturnTrue()
        {
            Assert.True(this.position.Equals(new Position(4, 4)));
        }

        [TestCase("1,  4")]
        [TestCase("3,3")]
        [TestCase("2,5")]
        public void EqualsMethod_ShouldReturnFalse(string details)
        {
            Assert.False(this.position.Equals(new Position(details)));
        }

        [Test]
        public void ToStringMethod_ShouldWorkCorrectly()
        {
            string expectedResult = "Row: 4, Column: 4";
            string actualResult = this.position.ToString();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
