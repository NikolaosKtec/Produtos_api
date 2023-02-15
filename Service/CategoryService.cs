using Produtos_api.DataBase;
using Produtos_api.Domain.Dto;
using Produtos_api.Domain.Products;
//  partial interface IContext_service
// {
//     public abstract void Save(Categoria param);
//     public abstract Categoria Get(int param);
//     public abstract void Update(Categoria param);
//     public abstract int Delete(int param);
// }

namespace Produtos_api.Service;



class CategoryService //: IContext_service
{
    public CategoryService(DB_context context) => Context = context;

    private readonly DB_context Context;

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
    public int Delete(int param)//todo verificar metodo
    {
        Category categoria = Get(param);

        if (categoria is null)
            return 0;

        Context.Categories.Remove(categoria);
        Context.SaveChanges();
        return 1;
    }

    public Category Get(int param)
    {
        return Context.Categories.Find(param);
    }

    public IQueryable<CategoryDto> GetAll()
    {

        return Context.Categories.Select(c => new CategoryDto
        {
            set_activity = c.is_active,
            Id = c.Id,
            Name = c.name,
        });
    }

    public void Save(Category param)
    {
        Context.Categories.Add(param);
        Context.SaveChanges();
    }

    public void Update(Category param)
    {
        Context.Categories.Update(param);
        Context.SaveChanges();
    }
}
