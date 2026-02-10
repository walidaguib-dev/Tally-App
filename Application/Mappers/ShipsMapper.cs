using Application.Dtos.Ships;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class ShipsMapper
    {
        public static Ship MapToModel(this Dtos.Ships.CreateShipDto dto)
        {
            return new Ship
            {
                Name = dto.Name,
                ImoNumber = dto.ImoNumber
            };
        }

        public static ShipDto MapToJson(this Ship ship) {
            return new ShipDto
            {
                Id = ship.Id,
                Name = ship.Name,
                ImoNumber = $"IMO{ship.ImoNumber}"
            };
                }
    }
}
