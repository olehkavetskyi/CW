using Domain.Entities;

namespace Application.Extensions;

public static class CustomerOrderProductToShopProduct
{
    public static ShopProduct ToShopProduct(this EmployeeOrderProduct product, Guid shopId)
    {
        return new ShopProduct
        {
            ShopId = shopId,
            ProductId = product!.ProductId,
            Quantity = product.Quantity
        };
    }
}
