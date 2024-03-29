﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace TiendaServicios.Api.Libro.Test
{
    //Para que se utiliza la clase AsyncQueryProvider:
    //Para poder emular el comportamiento de un objeto de tipo IAsyncQueryProvider
    public class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;
        public AsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerable<TElement>(expression);
        }

        public object? Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var resultado = typeof(TResult).GetGenericArguments()[0];
            var ejecucionResultado = typeof(IQueryProvider)
                .GetMethod(
                                   name: nameof(IQueryProvider.Execute),
                                                      genericParameterCount: 1,
                                                                         types: new[] { typeof(Expression) }
                                                                                        )?.MakeGenericMethod(resultado)
                .Invoke(this, new[] { expression });
            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))?.MakeGenericMethod(resultado)
                .Invoke(null, new[] { ejecucionResultado });
        }
    }
}
