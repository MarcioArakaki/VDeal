using System.Threading.Tasks;

namespace VehicleDealer.Persistence.DataAbstraction.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();        
    }
}