using Xunit;

namespace Application.Test.Common;

[CollectionDefinition(COLLECTION_NAME)]
public class InMemoryDbContextCollection : ICollectionFixture<InMemorySqliteDbContextFixture> {
    public const string COLLECTION_NAME = "DbContext";

    readonly InMemorySqliteDbContextFixture _fixture;

    public InMemoryDbContextCollection(InMemorySqliteDbContextFixture fixture) {
        _fixture = fixture;
    }
}
