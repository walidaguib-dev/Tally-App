using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Moq;
using StackExchange.Redis;
using Xunit;
using Infrastructure.Repositories;


namespace Tests.InfrastructureTests.Repositories.Caching
{
    public class CachingRepository_RemoveByPattern_Tests
    {
        private static Mock<IConnectionMultiplexer> CreateMockConnection(out Mock<IDatabase> mockDb, out Mock<IServer> mockServer, EndPoint endpoint)
        {
            mockDb = new Mock<IDatabase>();
            mockServer = new Mock<IServer>();
            var mockConn = new Mock<IConnectionMultiplexer>();
            mockConn
                .Setup(c => c.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(mockDb.Object);
            mockConn
                .Setup(c => c.GetEndPoints())
                .Returns(new EndPoint[] { endpoint });
            mockConn
                .Setup(c => c.GetServer(It.IsAny<EndPoint>(), It.IsAny<object>()))
                .Returns(mockServer.Object);
            return mockConn;
        }

        [Theory]
        [InlineData("user:", 0)]
        [InlineData("user:", 2)]
        [InlineData("sess:", 3)]
        public async Task RemoveByPattern_DeletesAllMatchingKeys(string pattern, int expectedCount)
        {
            // Arrange
            var endpoint = new DnsEndPoint("127.0.0.1", 6379);
            var mockConn = CreateMockConnection(out var mockDb, out var mockServer, endpoint);

            // Prepare keys that server.Keys should return for the pattern call.
            var keys = Enumerable.Range(1, expectedCount).Select(i => (RedisKey)$"{pattern}{i}").ToArray();

            // Setup server.Keys to return our keys.
            // Note: Keys has several overloads across versions; this setup matches the common signature:
            mockServer
                .Setup(s => s.Keys((int)It.IsAny<RedisValue>(), It.IsAny<int>(), (int)It.IsAny<long>(), It.IsAny<int>(), (int)It.IsAny<CommandFlags>()))
                .Returns(keys.AsEnumerable());

            // Ensure db.KeyDeleteAsync returns true for deletions
            mockDb
                .Setup(d => d.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(true);

            var repo = new CachingRepository(mockConn.Object);

            // Act
            await repo.RemoveByPattern(pattern);

            // Assert - KeyDeleteAsync called expected number of times with expected keys
            mockDb.Verify(d => d.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()), Times.Exactly(expectedCount));

            // Verify each expected key was attempted to be deleted
            foreach (var k in keys)
            {
                mockDb.Verify(d => d.KeyDeleteAsync(It.Is<RedisKey>(rk => rk == k), It.IsAny<CommandFlags>()), Times.Once);
            }

            // Verify GetServer/GetEndPoints were used
            mockConn.Verify(c => c.GetEndPoints(), Times.AtLeastOnce);
            mockConn.Verify(c => c.GetServer(It.IsAny<EndPoint>(), It.IsAny<object>()), Times.AtLeastOnce);
        }
    }
}