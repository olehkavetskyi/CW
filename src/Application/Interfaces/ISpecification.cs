using System.Linq.Expressions;

namespace Application.Interfaces;

public interface ISpecification<T>
{
    string FindElement { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}