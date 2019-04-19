using System.Threading.Tasks;

namespace VehicleDealer.Core
{
    public interface IUnitOfWork
    {
        Task SaveAsync();        
    }
}