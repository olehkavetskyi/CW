using Application.Interfaces;
using System.Linq.Expressions;

namespace Application.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public string? FindElement { get; private set; }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected void AddFindElement(string element)
    {
        FindElement = element;
    }
}
