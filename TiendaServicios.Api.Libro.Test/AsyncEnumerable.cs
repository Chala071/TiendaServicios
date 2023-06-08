using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Test
{
    //Para que se utiliza la clase AsyncEnumerable:
    //Para poder emular el comportamiento de un objeto de tipo IAsyncEnumerable
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T> 
    {
        public AsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
        public AsyncEnumerable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
    }
}
