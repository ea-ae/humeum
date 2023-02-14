using Xunit;

namespace Web.Test.Common;

[CollectionDefinition(COLLECTION_NAME)]
public class WebAppFactoryCollection : ICollectionFixture<CustomWebAppFactory> {
    public const string COLLECTION_NAME = "WebApp";

    private readonly CustomWebAppFactory _fixture;

    public WebAppFactoryCollection(CustomWebAppFactory fixture) {
        _fixture = fixture;
    }
}
