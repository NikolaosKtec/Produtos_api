using Flunt.Validations;
using Produtos_api.Domain.Generic;

namespace Produtos_api.Domain.Products;

public class Category : Entity
{
    public Category(string name) {
        Validate(name);

        this.name = name;
        is_active = true;

        CreatedBy = "test";
        CreatedOn = DateTime.UtcNow;
        EditedBy = "test";
        EditedOn = DateTime.UtcNow;
    }

    public string name { get; private set;}

    public bool is_active { get; private set; }

    public bool disabled { get; private set; } = false;

    public void Set (string name, bool activity)//(string name , bool is_active)
    {
        
        this.name = name;
        this.is_active = activity;
        Validate(name);
    }


    public void set_invalidate()
    {
        disabled = !disabled;
    }

    private void Validate(string name)
    {
        //validation flunt
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name")
            .IsGreaterOrEqualsThan(name, 4, "Name");
        AddNotifications(contract);
    }
}
