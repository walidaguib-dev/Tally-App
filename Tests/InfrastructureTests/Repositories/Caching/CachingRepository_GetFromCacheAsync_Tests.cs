using System.Threading.Tasks;
using Moq;
using StackExchange.Redis;
using Xunit;
using Infrastructure.Repositories;
using Newtonsoft.Json;

namespace Tests.InfrastructureTests.Repositories.Caching
{
    public class CachingRepository_GetFromCacheAsync_Tests
    {
        private static Mock<IConnectionMultiplexer> CreateMockConnection(out Mock<IDatabase> mockDb)
        {
            mockDb = new Mock<IDatabase>();
            var mockConnection = new Mock<IConnectionMultiplexer>();
            mockConnection
                .Setup(c => c.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(mockDb.Object);
            return mockConnection;
        }

        [Theory]
        [InlineData("missing-key", null)]
        [InlineData("greeting", "hello world")]
        [InlineData("empty", "")]
        public async Task GetFromCacheAsync_String_Theory_ReturnsExpectedValueOrNull(string key, string? cachedValue)
        {
            // Arrange
            var mockConn = CreateMockConnection(out var mockDb);
            RedisValue ret = cachedValue is null ? RedisValue.Null : new RedisValue(cachedValue);
            mockDb.Setup(d => d.StringGetAsync(It.Is<RedisKey>(k => k == key), It.IsAny<CommandFlags>()))
                  .ReturnsAsync(ret);

            var repo = new CachingRepository(mockConn.Object);

            // Act
            var result = await repo.GetFromCacheAsync<string>(key);

            // Assert
            if (cachedValue is null)
            {
                Assert.Null(result);
            }
            else
            {
                Assert.Equal(cachedValue, result);
            }
        }

        [Fact]
        public async Task GetFromCacheAsync_Dto_ReturnsDeserializedObject()
        {
            // Arrange
            var key = "dto-key";
            var dto = new TestDto { Name = "Alice", Age = 30 };
            var json = JsonConvert.SerializeObject(dto);

            var mockConn = CreateMockConnection(out var mockDb);
            mockDb.Setup(d => d.StringGetAsync(It.Is<RedisKey>(k => k == key), It.IsAny<CommandFlags>()))
                  .ReturnsAsync(new RedisValue(json));

            var repo = new CachingRepository(mockConn.Object);

            // Act
            var result = await repo.GetFromCacheAsync<TestDto>(key);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result!.Name);
            Assert.Equal(dto.Age, result.Age);
        }

        private class TestDto
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
        }
    }
}