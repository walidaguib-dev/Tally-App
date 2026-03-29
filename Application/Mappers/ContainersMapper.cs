using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Containers;
using Domain.Entities;
using Domain.Enums;
using Domain.Requests;

namespace Application.Mappers
{
    public static class ContainersMapper
    {
        public static ContainersDto MapToDto(this Container item)
        {
            return new ContainersDto
            {
                Bill_of_lading = item.Bill_of_lading,
                ContainerNumber = item.ContainerNumber,
                ContainerSize = item.ContainerSize.ToString(),
                ContainerStatus = item.ContainerStatus.ToString(),
                ContainerType = item.ContainerType.ToString(),
                SealNumber = item.SealNumber,
                ClientId = item.ClientId,
                ClientName = item.Client.Name,
                TallySheetId = item.TallySheetId,
                userId = item.userId,
                Username = $"Recorded by {item.user.UserName!}",
            };
        }

        public static Container MapToEntity(this CreateContainerDto dto)
        {
            var containerSize = Enum.TryParse<ContainerSize>(dto.ContainerSize, true, out var size)
                ? size
                : ContainerSize.Forty;
            var containerType = Enum.TryParse<ContainerType>(dto.ContainerType, true, out var type)
                ? type
                : ContainerType.OpenTop;
            var containerStatus = Enum.TryParse<ContainerStatus>(
                dto.ContainerStatus,
                true,
                out var status
            )
                ? status
                : ContainerStatus.Pending;
            return new Container
            {
                Bill_of_lading = dto.Bill_of_lading,
                ContainerNumber = dto.ContainerNumber,
                ContainerSize = containerSize,
                ContainerStatus = containerStatus,
                ContainerType = containerType,
                SealNumber = dto.SealNumber,
                ClientId = dto.ClientId,
                TallySheetId = dto.TallySheetId,
                userId = dto.userId,
            };
        }

        public static UpdateContainerRequest MapToRequest(this UpdateContainerDto dto)
        {
            return new UpdateContainerRequest
            {
                Bill_of_lading = dto.Bill_of_lading,
                ContainerNumber = dto.ContainerNumber,
                ContainerSize = dto.ContainerSize.ToString(),
                ContainerStatus = dto.ContainerStatus.ToString(),
                ContainerType = dto.ContainerType.ToString(),
                SealNumber = dto.SealNumber,
                ClientId = dto.ClientId,
            };
        }
    }
}
