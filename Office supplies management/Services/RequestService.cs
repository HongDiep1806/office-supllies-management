using AutoMapper;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            var newRequest = new RequestDto
            {
                UserID = createRequest.UserID,
                RequestCode = createRequest.RequestCode,
                TotalPrice = createRequest.TotalPrice,
                Product_Requests = createRequest.Products
                                                .Select(p => new ProductRequestDto
                                                {
                                                    ProductID = p.ProductID,
                                                    Quantity = p.Quantity
                                                }).ToList()
            };
            await _requestRepository.CreateAsync(_mapper.Map<Models.Request>(newRequest));
            return newRequest;
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
                                               .Select(p => new ProductRequestDto
                                               {
                                                   ProductID = p.ProductID,
                                                   Quantity = p.Quantity,
                                               }).ToList();
                await _productRequestService.AddRanges(_mapper.Map<List<Models.Product_Request>>(productRequests));
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

        public async Task<bool> ApproveRequestDepLeader(int requestId, int userId)
        {
            var requestEntity = await _requestRepository.GetByIdAsync(requestId);
            if (requestEntity == null || requestEntity.UserID != userId)
            {
                return false;
            }

            requestEntity.IsApprovedByDepLead = true;
            return await _requestRepository.UpdateAsync(requestId, requestEntity);
        }

        public async Task<List<RequestDto>> GetApprovedRequestsByDepLeader()
        {
            var requests = await _requestRepository.GetAllAsync();
            var approvedRequests = requests.Where(r => r.IsApprovedByDepLead).ToList();
            return _mapper.Map<List<RequestDto>>(approvedRequests);
        }

        public async Task<bool> ApproveRequestSupLead(int requestId, int userId)
        {
            var requestEntity = await _requestRepository.GetByIdAsync(requestId);
            if (requestEntity == null || requestEntity.UserID != userId)
            {
                return false;
            }

            requestEntity.IsApprovedBySupLead = true;
            return await _requestRepository.UpdateAsync(requestId, requestEntity);
        }

        public async Task<bool> ApproveRequestSupLead(ApproveRequestSupLeadCommand command)
        {
            var requestEntity = await _requestRepository.GetByIdAsync(command.RequestId);
            if (requestEntity == null)
            {
                return false;
            }

            // Check if the request is already approved by the department leader
            if (!requestEntity.IsApprovedByDepLead)
            {
                return false;
            }

            // Allow Finance Management Employee to approve the request
            requestEntity.IsApprovedBySupLead = true;
            return await _requestRepository.UpdateAsync(command.RequestId, requestEntity);
        }
        public async Task<List<RequestDto>> GetAllRequestsForSupLeader()
        {
            var requests = await _requestRepository.GetAllAsync();
            return _mapper.Map<List<RequestDto>>(requests);
        }


    }
}
