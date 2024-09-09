using IndustryConnect_Week5_WebApi.Dtos;
using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.Mappers
{
    public static class SaleMapper
    {
        public static Sale DtoToEntity(SaleDto saleDto)
        {
            var entity = new Sale
            {
                Id = saleDto.Id,
                CustomerId = saleDto.CustomerId,
                ProductId = saleDto.ProductId,
                StoreId = saleDto.StoreId,
                DateSold = saleDto.DateSold
            };

            return entity;
        }
        public static SaleDto EntityToDto(Sale entity)
        {
            var saleDto = new SaleDto
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ProductId = entity.ProductId,
                StoreId = entity.StoreId,
                DateSold = entity.DateSold,
                CustomerName = $"{entity.Customer.FirstName} {entity.Customer.LastName}",
                ProductName = entity.Product.Name,
                StoreName = entity.Store.Name
            };

            return saleDto;
        }
    }
}
