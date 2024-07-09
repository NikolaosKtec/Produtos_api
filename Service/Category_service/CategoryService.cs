using Produtos_api.DataBase;
using Produtos_api.Domain.Products;
using Produtos_api.EndPoints.Categorias.dto;


//  partial interface IContext_service
// {
//     public abstract void Save(Categoria param);
//     public abstract Categoria Get(int param);
//     public abstract void Update(Categoria param);
//     public abstract int Delete(int param);
// }

namespace Produtos_api.Service.Category_service;



class CategoryService //: IContext_service
{
    public CategoryService(AplicationDB_context context) => Context = context;

    private readonly AplicationDB_context Context;

    public int inactivate_childs(Category categoria)//todo verificar metodo
    {

        //
        //
        // List<Produto> produtos = Context.Produto.Where(p => p.Categoria.Id == categoria.Id).ToList();
        //
        // produtos.ForEach(p => p.set_isActive(false));
        //  
        //     
        // Context.Update(produtos);
        // Context.SaveChanges();
        return 1;
    }
    public async Task<int> Delete(int param)//todo verificar metodo
    {
        Category? categoria =  await Get(param);

        if (categoria is null)
            return 0;

        Context.Categories.Remove(categoria);
        await Context.SaveChangesAsync();
        return 1;
    }

    public async Task<Category?> Get(int param)
    {
        return await Context.Categories.FindAsync(param);
    }

    public IQueryable<CategoryDto> GetAll()
    {

        return Context.Categories.Where(c => c.disabled == false)
            .Select(c => new CategoryDto(c.name,c.IsValid)
            );

    }

    public async void Save(Category param)
    {
        await Context.Categories.AddAsync(param);
        await Context.SaveChangesAsync();
    }

    public async void Update(Category param)
    {
        Context.Categories.Update(param);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> safe_delete(Category param)
    {
        if (param.disabled)
        {
            Context.Categories.Update(param);
            await Context.SaveChangesAsync();
            return true;
        }

        return false;

    }
}
