using Produtos_api.DataBase;
using Produtos_api.Domain.Products;
using Produtos_api.EndPoints.Categorias.dto;

namespace Produtos_api.Service.Category_service;

class CategoryService
{
    public CategoryService(AplicationDB_context context) => Context = context;

    private readonly AplicationDB_context Context;

    public async Task<int> Delete(int param)
    {
        Category? categoria =  await GetAsync(param);

        if (categoria is null)
            return 0;

        Context.Categories.Remove(categoria);
        await Context.SaveChangesAsync();
        return 1;
    }

    public async Task<Category?> GetAsync(int param)
    {
        return await Context.Categories.FindAsync(param);
    }

    public IQueryable<CategoryDto> GetAll()
    {

        return  Context.Categories.Where(c => c.disabled == false)
            .Select(c => new CategoryDto(c.Id,c.name,c.IsValid));

    }

    public async Task<bool> SaveAsync(List<Category> param)
    {
        try{
            await Context.Categories.AddRangeAsync(param);
            await Context.SaveChangesAsync();
        }catch(Exception e){

            throw new Exception("on method SaveAsync", e);
           
        }
        return true;
    }

    public async Task<bool> UpdateAsync(Category param)
    {
        try
        {
            Context.Categories.Update(param);
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            
            throw new Exception("on method UpdateAsync", e);
        }
       
        return true;
    }

    // public async Task<bool> Safe_delete(Category param)
    // {
    //     if (param.disabled)
    //     {
    //         Context.Categories.Update(param);
    //         await Context.SaveChangesAsync();
    //         return true;
    //     }

    //     return false;

    // }
}
