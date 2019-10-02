using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Exceptions.BasketLogicExceptions;

namespace Ardalis.GuardClauses
{
    public static class BasketGuards
    {
        public static void NullBasket(this IGuardClause guardClause, int basketId, Basket basket)
        {
            if (basket == null)
                throw new BasketNotFoundException(basketId);
        }

        public static void TooManyOfItemInBasket(this IGuardClause guardClause, int maxNumberOfItem, int numberOfItemDesired)
        {
            if (numberOfItemDesired > maxNumberOfItem)
                throw new TooManyOfItemInBasketException(maxNumberOfItem);
        }
    }
}