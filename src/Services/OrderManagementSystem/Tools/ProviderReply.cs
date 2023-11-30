using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Grpc.ProviderService;
public partial class ProviderReply
{

    public ProviderReply(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public static implicit operator Provider(ProviderReply providerReply)
    {
        return new Provider
        {
            Id = providerReply.Id,
            Name = providerReply.Name,
        };
    }

    public static implicit operator ProviderReply(Provider provider)
    {
        return new ProviderReply
        {
            Id = provider.Id,
            Name = provider.Name,
        };
    }

   
}

