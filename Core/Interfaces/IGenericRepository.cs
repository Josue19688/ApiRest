using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /*
     *Creamos la interfaz generica para evitar estar creando una clase y un interfaz para cada dato
     *que podamos necesitar
     *
     */
    public interface IGenericRepository<T> where T:ClaseBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdWithSpec(ISpecifications<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecifications<T> spec);

        //agregar
        Task<int> Add(T entity);
        Task<int> Update(T entity);
    }
}
