using AutoMapper;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var productsInRequest = await _productRequestService.GetByRequestID(request.RequestID);
            var requestDto = _mapper.Map<RequestDto>(request);
            requestDto.Product_Requests = productsInRequest;
            if (productsInRequest.Count > 0)
            {
                Console.WriteLine("co san pham ne");
            }
            else
            {
                Console.WriteLine("khong co san pham");
            }
            return requestDto;
        }

        public async Task<List<RequestDto>> GetByUserID(int userId)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsOfUser = requests.Where(r => r.UserID == userId).ToList();
            return _mapper.Map<List<RequestDto>>(requestsOfUser);
        }

        public async Task<bool> Update(UpdateRequestDto updateRequest)
        {
            var currentRequest = await GetByID(updateRequest.RequestID);
            var prs = await _productRequestService.GetByRequestID(updateRequest.RequestID);
            if (prs != null)
            {
                foreach (var pr in prs)
                {
                    await _productRequestService.DeleteForever(pr.Product_RequestID);
                }
                var productRequests = updateRequest.Products
                                               .Select(p => new Product_Request
                                               {
                                                   RequestID = updateRequest.RequestID,
                                                   ProductID = p.ProductID,
                                                   Quantity = p.Quantity,
                                               }).ToList();
                await _productRequestService.AddRanges(productRequests);
            }
            else
            {
                return false;
            }
            return await _requestRepository.UpdateAsync(updateRequest.RequestID, _mapper.Map<Models.Request>(updateRequest));
        }

        public async Task<bool> DeleteByID(int id)
        {
            return await _requestRepository.DeleteAsync(id);
        }

        public Task<int> Count()
        {
            return _requestRepository.Count();
        }

        public async Task<List<RequestDto>> GetByDepartment(string department)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsByDepartment = requests.Where(r => r.User.Department == department).ToList();
            return _mapper.Map<List<RequestDto>>(requestsByDepartment);
        }
    }
}

