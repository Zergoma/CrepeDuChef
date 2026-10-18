using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Exceptions;
using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Domain.Services;
using CrepeDuChef.Tests.Fakes;
using CrepeDuChef.Tests.Helpers;
using FluentAssertions;

namespace CrepeDuChef.Tests.Domain.Services
{
    public class ChefRotationAlgorithmTests
    {
        [Fact]
        public void SelectNextChef_NoChef_ShouldThrowNoChefException()
        {
            // Arrange
            List<Chef> chefs = [];
            List<CrepeParty> sessionCrepes = [];
            IRandomProvider random = new FakeRandomProvider(0);

            // Act
            Action act = () =>
                ChefRotationAlgorithm.SelectNextChef(
                    chefs,
                    0,
                    sessionCrepes,
                    random);

            // Assert
            act.Should().Throw<NoChefException>();
        }

        [Fact]
        public void SelectNextChef_ChefIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            List<Chef> chefs = null!;
            List<CrepeParty> sessionCrepes = [];
            IRandomProvider random = new FakeRandomProvider(0);

            // Act
            Action act = () =>
                ChefRotationAlgorithm.SelectNextChef(
                    chefs,
                    0,
                    sessionCrepes,
                    random);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SelectNextChef_SessionChefIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            List<Chef> chefs = [];
            List<CrepeParty> sessionCrepes = null!;
            IRandomProvider random = new FakeRandomProvider(0);

            // Act
            Action act = () =>
                ChefRotationAlgorithm.SelectNextChef(
                    chefs,
                    0,
                    sessionCrepes,
                    random);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SelectNextChef_RandomIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            List<Chef> chefs = [];
            List<CrepeParty> sessionCrepes = [];
            IRandomProvider random = null!;

            // Act
            Action act = () =>
                ChefRotationAlgorithm.SelectNextChef(
                    chefs,
                    0,
                    sessionCrepes,
                    random);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

#pragma warning disable format
        [Theory]

        // 1 chef -> incr session and get this chef
        [InlineData(new int[] { 1}, new int[] { }, 0, 5,                        6, 1)]
        [InlineData(new int[] { 1}, new int[] { 1 }, 0, 5,                      6, 1)]

        // 2 chefs no session, select one of them
        [InlineData(new int[] { 1,2 }, new int[] { }, 0, 5,                     5, 1)]
        [InlineData(new int[] { 1,2 }, new int[] { }, 1, 5,                     5, 2)]
        
        
        // 2 chefs 1 session, select the remaining
        [InlineData(new int[] { 1,2 }, new int[] { 1}, 0, 5,                    5, 2)]
        [InlineData(new int[] { 1,2 }, new int[] { 2}, 0, 5,                    5, 1)]


        // 2 chefs 2 sessions, incr session don't take last session's last chef 
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 }, 0, 5,               6, 1)]
        [InlineData(new int[] { 1, 2 }, new int[] { 2, 1 }, 0, 5,               6, 2)]


        //3 chefs, 1 session, take one of the remaining chefs
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1 }, 0, 5,               5, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 2 }, 0, 5,               5, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3 }, 0, 5,               5, 1)]

        //3 chefs, 2 session, take the last remaining chef
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2 }, 0, 5,            5, 3)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 2, 3 }, 0, 5,            5, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 3 }, 0, 5,            5, 2)]

        //3 chefs, 3 session, incr session take one chef but not the last session's last chef
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, 0, 5,         6, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 2, 3, 1 }, 0, 5,         6, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3, 1, 2 }, 0, 5,         6, 1)]

        // 3 chefs, 0 session, fixrandom idx 
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, 0, 5,                 5, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, 1, 5,                 5, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, 2, 5,                 5, 3)]
#pragma warning restore format
        public void SelectNextChef_AllChefs(
            int[] allChefs,
            int[] sessionChefs,
            int fixedRandomIdx,
            int sessionId,
            int expectedSessionId,
            int expectedChefId
            )
        {
            // Arrange
            List<Chef> chefs = TestData.ChefsWithIds(allChefs);
            List<CrepeParty> session = TestData.CrepePartiesWithChefIds(sessionChefs);
            IRandomProvider random = new FakeRandomProvider(fixedRandomIdx);
            int lastSessionId = sessionId;

            // Act
            var (Chef, SessionNumber) =
                ChefRotationAlgorithm.SelectNextChef(
                    chefs,
                    lastSessionId,
                    session,
                    random);

            // Assert
            Chef.Id.Should().Be(TestData.IntToGuid(expectedChefId));
            SessionNumber.Should().Be(expectedSessionId);
        }



#pragma warning disable format
        [Theory]
        // all available - no session
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 1, 2, 3 } , 0, 5,      5, 1)]

        // all available - session no completed
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1 }, new int[] { 1, 2, 3 } , 0, 5,      5, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 2 }, new int[] { 1, 2, 3 } , 0, 5,      5, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3 }, new int[] { 1, 2, 3 } , 0, 5,      5, 1)]


        // all available - session completed
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1,2,3 }, new int[] { 1, 2, 3 }, 0, 5,    6, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 2,3,1 }, new int[] { 1, 2, 3 }, 0, 5,    6, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3,1,2 }, new int[] { 1, 2, 3 }, 0, 5,    6, 1)]


        // 1 available, no session, incr and return that chef
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 1 }, 0, 5,            6, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 2 }, 0, 5,            6, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 3 }, 0, 5,            6, 3)]

        // 1 available, 1 session with it
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1 }, new int[] { 1 }, 0, 5,          6, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 2 }, new int[] { 2 }, 0, 5,          6, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3 }, new int[] { 3 }, 0, 5,          6, 3)]

        // 1 available, 1 session with someone else
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3 }, new int[] { 1 }, 0, 5,          6, 1)]

        // 2 available, no session yet (first draw), pick one of them in the current session
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 1, 2 }, 0, 0,          0, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 1, 2 }, 1, 0,          0, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 2, 3 }, 1, 5,          5, 3)]

        // 2 available, 1 session with one of them, get the last remaining
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1 }, new int[] { 1, 2 }, 0, 5,       5, 2)]
        // 2 available, 2 session with one of them, get the last remaining
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 3 }, new int[] { 1, 2 }, 0, 5,    5, 2)]
        
        // 2 available, 2 sessions with both, incr session, get not the last
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2 }, new int[] { 1, 2 }, 0, 5,    6, 1)]

#pragma warning restore format

        public void SelectNextChef_WithAvailableChefs(
            int[] allChefs,
            int[] sessionChefs,
            int[] availableChefs,
            int fixedRandomIdx,
            int sessionId,
            int expectedSessionId,
            int expectedChefId
            )
        {
            // Arrange
            List<Chef> chefs = TestData.ChefsWithIds(allChefs);
            List<CrepeParty> session = TestData.CrepePartiesWithChefIds(sessionChefs);
            List<Chef> available = TestData.ChefsWithIds(availableChefs);
            IRandomProvider random = new FakeRandomProvider(fixedRandomIdx);
            int lastSessionId = sessionId;

            // Act
            var (Chef, SessionNumber) =
                ChefRotationAlgorithm.SelectNextChef(
                    chefs,
                    lastSessionId,
                    session,
                    random,
                    available);

            // Assert
            Chef.Id.Should().Be(TestData.IntToGuid(expectedChefId));
            SessionNumber.Should().Be(expectedSessionId);
        }

        [Fact]
        public void SelectNextChef_AvailableChefsNull_ShouldFallbackToNormalSelection()
        {
            // Arrange
            var chefs = TestData.ChefsWithIds([1, 2, 3]);
            var session = TestData.CrepePartiesWithChefIds([]);
            IRandomProvider random = new FakeRandomProvider(0);

            // Act
            var (chef, sessionId) = ChefRotationAlgorithm.SelectNextChef(
                chefs, 5, session, random, null);

            // Assert
            chef.Id.Should().Be(TestData.IntToGuid(1));
            sessionId.Should().Be(5);
        }

        [Fact]
        public void SelectNextChef_AvailableChefsEmpty_ShouldFallbackToNormalSelection()
        {
            // Arrange
            var chefs = TestData.ChefsWithIds([1, 2, 3]);
            var session = TestData.CrepePartiesWithChefIds([]);
            IRandomProvider random = new FakeRandomProvider(1);

            // Act
            var (chef, sessionId) = ChefRotationAlgorithm.SelectNextChef(
                chefs, 5, session, random, []);

            // Assert
            chef.Id.Should().Be(TestData.IntToGuid(2));
            sessionId.Should().Be(5);
        }

        [Fact]
        public void SelectNextChef_SessionContainsUnknownId_ShouldIgnoreIt()
        {
            // Arrange
            var chefs = TestData.ChefsWithIds([1, 2, 3]);
            var session = TestData.CrepePartiesWithChefIds([99]);
            IRandomProvider random = new FakeRandomProvider(2);

            // Act
            var (chef, sessionId) = ChefRotationAlgorithm.SelectNextChef(
                chefs, 5, session, random);

            // Assert
            chef.Id.Should().Be(TestData.IntToGuid(3));
            sessionId.Should().Be(5);
        }

        [Fact]
        public void SelectNextChef_RandomIndexTooLarge_ShouldThrow()
        {
            // Arrange
            var chefs = TestData.ChefsWithIds([1, 2, 3]);
            var session = TestData.CrepePartiesWithChefIds([]);
            IRandomProvider random = new FakeRandomProvider(10);

            // Act
            Action act = () => ChefRotationAlgorithm.SelectNextChef(
                chefs, 5, session, random);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void SelectNextChef_UnsortedChefs_ShouldRespectInputOrder()
        {
            // Arrange
            var chefs = TestData.ChefsWithIds([3, 1, 2]);
            var session = TestData.CrepePartiesWithChefIds([]);
            IRandomProvider random = new FakeRandomProvider(0);

            // Act
            var (chef, _) = ChefRotationAlgorithm.SelectNextChef(
                chefs, 5, session, random);

            // Assert
            chef.Id.Should().Be(TestData.IntToGuid(3));
        }

        [Fact]
        public void SelectNextChef_AvailableChefsWithDuplicates_ShouldWorkNormally()
        {
            // Arrange
            var chefs = TestData.ChefsWithIds([1, 2, 3]);
            var available = TestData.ChefsWithIds([1, 1, 2]);
            var session = TestData.CrepePartiesWithChefIds([]);
            IRandomProvider random = new FakeRandomProvider(1);

            // Act
            var (chef, _) = ChefRotationAlgorithm.SelectNextChef(
                chefs, 5, session, random, available);

            // Assert
            chef.Id.Should().Be(TestData.IntToGuid(2));
        }

        [Fact]
        public void SelectNextChef_EmptySessionButNonZeroSessionId_ShouldKeepSessionId()
        {
            // Arrange
            var chefs = TestData.ChefsWithIds([1, 2, 3]);
            var session = TestData.CrepePartiesWithChefIds([]);
            IRandomProvider random = new FakeRandomProvider(0);

            // Act
            var (_, sessionId) = ChefRotationAlgorithm.SelectNextChef(
                chefs, 10, session, random);

            // Assert
            sessionId.Should().Be(10);
        }

    }
}
