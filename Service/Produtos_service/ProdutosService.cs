using Produtos_api.DataBase;
using Produtos_api.Domain.Products;
using Produtos_api.EndPoints.Produtos;

namespace Produtos_api.Service.Produtos_service;
class ProdutosService
{
    public ProdutosService(AplicationDB_context context) => Context = context;

    private readonly AplicationDB_context Context;

     public async Task<int> DeleteAsync(int param)
    {
        Product? product =  await Get(param);

        if (product is null)
            return 0;

        Context.Products.Remove(product);
        await Context.SaveChangesAsync();
        return 1;
    }

    public async Task<Product?> Get(int param)
    {
        return await Context.Products.FindAsync(param);
    }

    public IQueryable<ProductDto> GetAll()
    {

        return Context.Products.Where(item => item.HasStock ==true)
        .Select(p => new ProductDto(p.Name,p.Id,p.Category,p.HasStock));

    }

    public async Task<bool> SaveAsync(Product param)
    {
        await Context.Products.AddAsync(param);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Product param)
    {
        Context.Products.Update(param);
        await Context.SaveChangesAsync();
        return true;
    }

}