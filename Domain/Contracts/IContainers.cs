using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Helpers.Pagination;
using Domain.Requests;

namespace Domain.Contracts
{
    public interface IContainers
    {
        public Task<PagedResult<Container>?> GetAll(
            PaginationParams paginationParams,
            string? ContainerNumber,
            string? Bill_Of_Lading,
            string? ClientName
        );
        public Task<List<Container>> GetAllByTallySession(int TallySheetSessionId);
        public Task<Container?> GetAsync(string ContainerNumber);
        public Task<Container> CreateAsync(Container container);
        public Task<bool?> UpdateAsync(string ContainerNumber, UpdateContainerRequest request);
        public Task<bool?> DeleteAsync(string ContainerNumber);
    }
}
