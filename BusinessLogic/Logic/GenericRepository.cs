﻿using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClaseBase
    {
        private readonly MarcketDbContext _context;
        public GenericRepository(MarcketDbContext context)
        {
            _context = context;
        }
        public async  Task<IReadOnlyList<T>> GetAllAsync()
        {
           return await  _context.Set<T>().ToListAsync();
        }

       

        public async  Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
            
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync(); 
        }

        public async Task<T> GetByIdWithSpec(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();

        }
    }


}
