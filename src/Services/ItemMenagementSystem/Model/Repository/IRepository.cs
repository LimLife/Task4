using ItemManagementSystem.Model.Repository.RepositoryOrderItem;
using ItemManagementSystem.Model.Repository.RepositoryOrder;

namespace ItemManagementSystem.Model.Repository
{
    public interface IRepository : IRepositoryOrderItem, IRepositoryOrder { }  
}
