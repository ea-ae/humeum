using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Web.Test.Common;

[CollectionDefinition(COLLECTION_NAME)]
public class WebApplicationFactoryCollection : ICollectionFixture<WebApplicationFactory<Program>> {
    public const string COLLECTION_NAME = "WebApp";

    private readonly WebApplicationFactory<Program> _fixture;

    public WebApplicationFactoryCollection(WebApplicationFactory<Program> fixture) {
        _fixture = fixture;
    }
}
