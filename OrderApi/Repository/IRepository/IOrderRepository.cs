﻿using OrderApi.Models;
using OrderApi.Models.Extensions;

public interface IOrderRepository
{
    Task<PaginatedResult<Order>> GetAllPaginatedAsync(int pageNumber, int pageSize, string searchTerm, Filter? filter);
    Task<Order?> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
    Task DeleteAsync(Order order);
    Task UpdateAsync(Order order);
}