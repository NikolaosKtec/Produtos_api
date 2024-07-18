using Flunt.Validations;
using Produtos_api.Domain.Generic;

namespace Produtos_api.Domain.Products;

public class Product : Entity
{
   public Product(string name, string description, int CategoryId, bool HasStock){
    
    Validate(name, CategoryId);
    this.Name = name;
    // this.Id = id;
    this.Description = description;
    this.CategoryId = CategoryId;
    this.HasStock = HasStock;
        CreatedBy = "test";
        CreatedOn = DateTime.UtcNow;
        EditedBy = "test";
        EditedOn = DateTime.UtcNow;

   }
    public string? Name { get; private set; }

    public int CategoryId { get; private set; }

    public Category? Category { get; private set; }

    public string? Description { get; private set; }

    public bool HasStock { get; private set; }

    private void Validate(string name,int categoryId){
        var contract =  new Contract<Product>()
        .IsNotNullOrEmpty(name, "Name")
        .IsNotNull(categoryId, "CategoryId")
        .IsGreaterOrEqualsThan(categoryId,0,"CategoryId");

        AddNotifications(contract);
    }

    
}
