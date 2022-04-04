using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecifications<T>
    {
        //representa la condicion logica que se  le aplica a una entidad
        Expression<Func<T, bool>> Criteria { get; }


        //relaciones que se puedan inplementar a la entidad
        List<Expression<Func<T,object>>> Includes { get; }

        Expression<Func<T,object>>OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }
    }
}
