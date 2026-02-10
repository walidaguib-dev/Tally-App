using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IShips
    {
        public Task<List<Ship>> GetAllShips();
         public Task<Ship?> GetShipById(int id);
         public Task<Ship> CreateShip(Ship ship);
         public Task<Ship?> UpdateShip(int id , string Name , string IMO);
         public Task<Ship?> DeleteShip(int id);
    }
}
