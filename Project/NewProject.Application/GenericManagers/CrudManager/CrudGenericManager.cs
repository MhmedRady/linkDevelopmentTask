using NewProject.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NewProject.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using NewProject.Domain;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NewProject.Shared;

namespace NewProject.Application;

public class CrudGenericManager<TKey, Entity, ReadDto, WriteDto> : ICrudGenericManager<TKey, Entity, ReadDto, WriteDto> 
    where ReadDto : class 
    where WriteDto : class
    where Entity : class
{
    private readonly IGeneralRepository<Entity, TKey> _repository;
    private readonly IMapper _mapper;

    public CrudGenericManager(IGeneralRepository<Entity, TKey> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public ReadDto Add(WriteDto dto)
    {
        var entity = _mapper.Map<WriteDto, Entity>(dto);
        _repository.Add(entity);
        _repository.SaveChanges();
        return _mapper.Map<ReadDto>(entity);
    }

    public async Task<ReadDto> AddAsync(WriteDto dto)
    {
        var entity = _mapper.Map<WriteDto, Entity>(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ReadDto>(entity);
    }

    public int Count(Expression<Func<Entity, bool>> expression)
    {
        return _repository.Count(expression);
    }

    public async Task<IEnumerable<ReadDto>> GetAll(Expression<Func<Entity, bool>> expression,  int? take, int? skip,
        Expression<Func<Entity, object>> orderBy, string orderDirection = Constanties.ORDERASC, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> include = null)
    {
        var data = _repository.Get(expression: expression, take: take, skip: skip, orderby: orderBy,
            orderDirection: orderDirection, include: include);
        return _mapper.Map<IEnumerable<ReadDto>>(data);
    }

    public async Task<IEnumerable<ReadDto>> GetAll(Expression<Func<Entity, bool>> expression, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> include = null)
    {
        var data = _repository.Get(expression: expression, include: include);
        return _mapper.Map<IEnumerable<ReadDto>>(data);
    }

    public ReadDto GetBy(Expression<Func<Entity, bool>> expression)
    {
        var obj = _repository.GetBy(expression);
        return _mapper.Map<ReadDto>(obj);
    }
    
    public ReadDto GetBy(Expression<Func<Entity, bool>> expression, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> include = null)
    {
        var obj = _repository.GetBy(expression, include: include);
        return _mapper.Map<ReadDto>(obj);
    }

    public async Task<ReadDto> GetById(TKey Id)
    {
        var obj = await _repository.GetById(Id);
        return _mapper.Map<ReadDto>(obj);
    }

    public async Task<WriteDto> GetWrite(Expression<Func<Entity, bool>> expression, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> include = null)
    {
        var obj = _repository.GetBy(expression, include);
        return _mapper.Map<WriteDto>(obj);
    }

    public async Task<Entity> GetModelById(TKey Id)
    {
        return await _repository.GetById(Id);
    }

    public bool IsExisted(Expression<Func<Entity, bool>> expression)
    {
        return _repository.IsExisted(expression);
    }

    public async Task<bool> IsExistedAsync(Expression<Func<Entity, bool>> expression)
    {
        return await _repository.IsExistedAsync(expression);
    }

    public async Task<bool> Remove(TKey id)
    {
        var entity = await _repository.GetById(id);
        if (entity is null) { return false; }
        _repository.Remove(entity);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<ReadDto> Update( WriteDto dto)
    {
        var obj = _mapper.Map<Entity>(dto);
        var result = _repository.Update(obj);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ReadDto>(result.Entity);
    }

    public async Task<ReadDto> UpdateById(TKey Id, WriteDto dto)
    {
        var obj = await GetModelById(Id);
        // var obj = _mapper.Map<Department>(dto);
        var result = _repository.Update(obj);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ReadDto>(result.Entity);
    }
}
