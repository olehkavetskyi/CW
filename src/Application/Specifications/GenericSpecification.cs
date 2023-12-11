namespace Application.Specifications;

public class GenericSpecification<T> : BaseSpecification<T>
{
    public GenericSpecification(SpecParams specParams)
    {
        if (specParams.PageSize != null)
        {
            ApplyPaging((int)(specParams.PageSize * (specParams.PageNumber - 1)), (int)specParams.PageSize);
        }
        if (!(string.IsNullOrEmpty(specParams.FindElement)))
        {
            AddFindElement(specParams.FindElement);
        }
    }
}
