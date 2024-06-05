using Application.Streaming.Dto;

namespace Application.Streaming.Interfaces;
public interface ICustomerService
{
    CustomerDto Create(CustomerDto obj);
    List<CustomerDto> FindAll(Guid userId);
    CustomerDto FindById(Guid id);
    CustomerDto Update(CustomerDto obj);
    bool Delete(CustomerDto obj);
}
