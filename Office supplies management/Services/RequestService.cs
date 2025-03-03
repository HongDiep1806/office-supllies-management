using AutoMapper;
using Azure.Core;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IProduct_RequestService _productRequestService;
        private readonly IMapper _mapper;
        public RequestService(IRequestRepository requestRepository, IMapper mapper, IProduct_RequestService productRequestService)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            _productRequestService = productRequestService;
        }

        public async Task<RequestDto> Create(CreateRequestDto createRequest)
        {
            var newRequest = new Models.Request
            {
                UserID = createRequest.UserID,
                RequestCode = createRequest.RequestCode,
                TotalPrice = createRequest.TotalPrice,

            };
            await _requestRepository.CreateAsync(newRequest);
            var productRequests = createRequest.Products
                                               .Select(p => new Product_Request
                                               {
                                                   RequestID = newRequest.RequestID,
                                                   ProductID = p.ProductID,
                                                   Quantity = p.Quantity
                                               }).ToList();
            await _productRequestService.AddRanges(productRequests);
            newRequest.Product_Requests = productRequests;
            return _mapper.Map<RequestDto>(newRequest);
        }


        public async Task<List<RequestDto>> GetAll()
        {
            var requests = await _requestRepository.GetAllAsync();
            return _mapper.Map<List<RequestDto>>(requests);
        }

        public async Task<RequestDto> GetByID(int id)
        {
           var request = await _requestRepository.GetByIdAsync(id);
            return _mapper.Map<RequestDto>(request);
        }

        public async Task<List<RequestDto>> GetByUserID(int userId)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsOfUser = requests.Where(r => r.UserID == userId).ToList();
            return _mapper.Map<List<RequestDto>>(requestsOfUser);
        }

        public Task<RequestDto> Update(RequestDto updateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
