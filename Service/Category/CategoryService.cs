﻿using Produtos_api.DataBase;
using Produtos_api.Domain.Products;

using Produtos_api.EndPoints.Categorias;
//  partial interface IContext_service
// {
//     public abstract void Save(Categoria param);
//     public abstract Categoria Get(int param);
//     public abstract void Update(Categoria param);
//     public abstract int Delete(int param);
// }

namespace Produtos_api.Service.Category;



class CategoryService //: IContext_service
{
    public CategoryService(AplicationDB_context context) => Context = context;

    private readonly AplicationDB_context Context;

    public int inactivate_childs(CategoryDomain categoria)//todo verificar metodo
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
        CategoryDomain categoria = Get(param);

        if (categoria is null)
            return 0;

        Context.Categories.Remove(categoria);
        Context.SaveChanges();
        return 1;
    }

    public CategoryDomain? Get(int param)
    {
        return Context.Categories.Find(param);
    }

    public IQueryable<CategoryDto> GetAll()
    {

        return Context.Categories.Where(c => c.disabled == false)
            .Select(c => new CategoryDto(c.name,c.IsValid)
            );

    }

    public void Save(CategoryDomain param)
    {
        Context.Categories.Add(param);
        Context.SaveChanges();
    }

    public void Update(CategoryDomain param)
    {
        Context.Categories.Update(param);
        Context.SaveChanges();
    }

    public bool safe_delete(CategoryDomain param)
    {
        if (param.disabled)
        {
            Context.Categories.Update(param);
            Context.SaveChanges();
            return true;
        }

        return false;

    }
}
