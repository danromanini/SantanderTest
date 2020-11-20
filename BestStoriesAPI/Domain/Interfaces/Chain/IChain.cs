using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChain<TParam, TResult>
    {
        IChain<TParam, TResult> Next { get; set; }

        Task<TResult> Execute(TParam chainParam);

    }
}
