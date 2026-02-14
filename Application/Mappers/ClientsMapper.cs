using Application.Dtos.Clients;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class ClientsMapper
    {
        public static ClientsDto MapToJson(this Client client) {
            return new ClientsDto { 
                Id = client.Id,
                Name = client.Name,
                ContactInfo = client.ContactInfo,
                Bill_Of_Lading = client.Bill_Of_Lading,
                MerchandiseId = client.MerchandiseId,
                Merchandise_Name = client.Merchandise.Name
            };
        }

        public static Client MapToModel(this CreateClientDto dto) {
            return new Client
            {
                Name = dto.Name,
                ContactInfo = dto.ContactInfo,
                Bill_Of_Lading = dto.Bill_Of_Lading,
                MerchandiseId = dto.MerchandiseId
            };
        }
    }
}
