using Domain.Entities;
using Domain.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IShips
    {
        public Task<PagedResult<Ship>?> GetAllShips(PaginationParams paginationParams, string? name);
        public Task<Ship?> GetShipById(int id);
        public Task<Ship> CreateShip(Ship ship);
        public Task<Ship?> UpdateShip(int id, string Name, string IMO);
        public Task<Ship?> DeleteShip(int id);
    }
}
