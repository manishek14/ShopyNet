using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.ProductAgg.Services
{
    public interface ProductDomainService
    {
        bool SlugExists(string slug, Guid productId);
    }
}
