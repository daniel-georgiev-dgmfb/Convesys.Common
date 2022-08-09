using System.Threading.Tasks;

namespace Convesys.Common.CQRS.Events
{
    public interface INotifier
    {
        Task Notify();
        NotifierModel NotifierModel { get; set; }
    }
}