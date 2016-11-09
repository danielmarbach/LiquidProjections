using System.Threading.Tasks;

namespace LiquidProjections
{
    public interface IStoreProjectionStatistics
    {
        Task SaveCheckpoint(string projectorId, long checkpoint);
    }
}