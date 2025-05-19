using Marten;

namespace backend.Infrastructure.Context;

public class MartenContext(IDocumentStore documentStore)
{
    public IDocumentSession GetLightweightSession()
    {
        return documentStore.LightweightSession();
    }

    public IQuerySession GetQuerySession()
    {
        return documentStore.QuerySession();
    }

}
