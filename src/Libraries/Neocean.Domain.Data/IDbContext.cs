﻿using System;
using System.Collections.Generic;

using System.Data.Entity;

namespace Neocean.Domain.Data
{
    /// <summary>
    /// DataBase Content interface 
    /// </summary>
    public interface IDbContext
    {
        IDbSet<TAggregateRoot> Set<TAggregateRoot>() where TAggregateRoot : AggregateRoot;

        int SaveChanges();

        IList<TAggregateRoot> ExecuteStoredProcedureList<TAggregateRoot>(string commandText, params object[] parameters)
            where TAggregateRoot : AggregateRoot, new();

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters);
    }
}
