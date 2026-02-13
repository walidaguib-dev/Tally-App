using Application.Dtos.Merchandises;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class MerchandiseMapper
    {
        public static MerchandiseDto MapToJson(this Merchandise model) {
            return new MerchandiseDto
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type
            };
        }

        public static Merchandise MapToModel(this CreateMerchandiseDto dto) {
            return new Merchandise
            {
                Name = dto.Name,
                Type = dto.Type
            };
        }
    }
}
