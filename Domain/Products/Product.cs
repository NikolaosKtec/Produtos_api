using Produtos_api.Domain.Generic;

namespace Produtos_api.Domain.Products;

public class Product : Entity
{
   
    public string Name { get; private set; }

    public int CategoryId { get; private set; }

    public Category Category { get; private set; }

    public string Description { get; private set; }

    public bool HasStock { get; private set; }

    
}
