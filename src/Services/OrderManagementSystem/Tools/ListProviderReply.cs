using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Grpc.ProviderService;
public partial class ListProviderReply
{
    public static implicit operator List<Provider>(ListProviderReply reply)
    {
        return reply.Provider.Select(item => new Provider { Id = item.Id, Name = item.Name }).ToList();
    }
    public static implicit operator ListProviderReply(List<Provider> reply)
    {
        var replyPrvoidersList = new ListProviderReply();
        var replyList = reply.Select(item => new ProviderReply { Id = item.Id, Name = item.Name }).ToList();
        replyPrvoidersList.Provider.AddRange(replyList);
        return replyPrvoidersList;
    }
}

