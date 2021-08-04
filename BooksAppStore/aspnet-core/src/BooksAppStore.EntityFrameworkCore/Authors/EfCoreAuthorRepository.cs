﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using BooksAppStore.DomainAuthors;
using BooksAppStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BooksAppStore.Authors
{
    public class EfCoreAuthorRepository
        : EfCoreRepository<BooksAppStoreDbContext, Author, Guid>,
            IAuthorRepository
    {
        public EfCoreAuthorRepository(
            IDbContextProvider<BooksAppStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            
        }
        
        public async Task<Author> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(author => author.Name == name);
        }
        
        public async Task<List<Author>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    author => author.Name.Contains(filter)
                )
                .OrderBy(ParsingConfig.Default,sorting)
                // .OrderBy(a=>a.Name)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}