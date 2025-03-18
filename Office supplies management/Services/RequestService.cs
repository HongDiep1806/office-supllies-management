using AutoMapper;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RequestService(IUserRepository userRepository,IRequestRepository requestRepository, IMapper mapper, IProduct_RequestService productRequestService)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            _productRequestService = productRequestService;
            _userRepository = userRepository;
        }

        public async Task<RequestDto> Create(CreateRequestDto createRequest)
        {
            var newRequest = new Models.Request
            {
                UserID = createRequest.UserID,
                RequestCode = createRequest.RequestCode,
                TotalPrice = createRequest.TotalPrice,
                //IsProcessedByDepLead = true
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
            //if (productsInRequest.Count > 0)
            //{
            //    Console.WriteLine("co san pham ne");
            //}
            //else
            //{
            //    Console.WriteLine("khong co san pham");
            //}
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
            var requestOfUsersInDepartment = new List<Request>();
            foreach (var request in requests)
            {
                var user = await _userRepository.GetByIdAsync(request.UserID);
                if(user.Department.ToLower().Equals(department.ToLower()))
                {
                    requestOfUsersInDepartment.Add(request);
                }
            }
            return _mapper.Map<List<RequestDto>>(requestOfUsersInDepartment);
        }

        public async Task<bool> ApproveByDepLeader(int requestID)
        {
            var request = await _requestRepository.GetByIdAsync(requestID);
            if (request != null)
            {
                request.IsProcessedByDepLead = true;
                request.IsApprovedByDepLead = true;
                await _requestRepository.UpdateAsync(requestID, request);
                return true;
            }
            return false;
        }

        public async Task<List<RequestDto>> GetApprovedRequestsByDepLeader()
        {
            var requests = await _requestRepository.GetAllAsync();
            var approvedRequests = requests.Where(r => r.IsApprovedByDepLead == true).ToList();
            return _mapper.Map<List<RequestDto>>(approvedRequests);
        }

        public async Task<bool> ApproveByFinEmployee(int requestId)
        {
            var requestEntity = await _requestRepository.GetByIdAsync(requestId);
            if (requestEntity == null)
            {
                return false;
            }

            requestEntity.IsApprovedBySupLead = true;
            await _requestRepository.UpdateAsync(requestId, requestEntity);
            return true;
        }

        public async Task<List<RequestDto>> GetAllRequestsForFinEmployee()
        {
            var requests = await _requestRepository.GetAllAsync();
            return _mapper.Map<List<RequestDto>>(requests);
        }
        public async Task<bool> NotApproveRequestByDepLeader(int requestID)
        {
            var request = await _requestRepository.GetByIdAsync(requestID);
            if (request != null)
            {
                request.IsProcessedByDepLead = true;
                request.IsApprovedByDepLead = false;
                await _requestRepository.UpdateAsync(requestID, request);
                return true;
            }
            return false;
        }
        public async Task<bool> NotApproveRequestByFinEmployee(int requestID)
        {
            var request = await _requestRepository.GetByIdAsync(requestID);
            if (request != null)
            {
                request.IsProcessedByDepLead = false;
                request.IsApprovedByDepLead = true;
                await _requestRepository.UpdateAsync(requestID, request);
                return true;
            }
            return false;
        }
        public async Task UpdateRequestStatus(int summaryID, bool isProcessedBySupLead, bool isApprovedBySupLead)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsToUpdate = requests.Where(r => r.SummaryID == summaryID).ToList();

            foreach (var request in requestsToUpdate)
            {
                request.IsSummaryBeProcessed = isProcessedBySupLead;
                request.IsSummaryBeApproved = isApprovedBySupLead;
            }

            foreach (var request in requestsToUpdate)
            {
                await _requestRepository.UpdateAsync(request.RequestID, request);
            }
        }
        public async Task<List<RequestDto>> GetCollectedRequests()
        {
            var requests = await _requestRepository.GetAllAsync();
            var collectedRequests = requests.Where(r => (r.IsCollectedInSummary && r.IsSummaryBeApproved && r.IsSummaryBeProcessed)).ToList();
            var collectedRequestDtos = new List<RequestDto>();

            // Log the request IDs that pass the filter
            //Console.WriteLine("Request IDs that pass the filter:");
            //foreach (var request in collectedRequests)
            //{
            //    Console.WriteLine(request.RequestID);
            //}

            foreach (var request in collectedRequests)
            {
                var productsInRequest = await _productRequestService.GetByRequestID(request.RequestID);
                var requestDto = _mapper.Map<RequestDto>(request);
                requestDto.Product_Requests = productsInRequest;
                collectedRequestDtos.Add(requestDto);
            }

            return collectedRequestDtos;
        }
        public async Task<List<RequestDto>> GetRequestsInApprovedSummary()
        {
            var requests = await _requestRepository.GetAllAsync();
            var approvedSummaryRequests = requests.Where(r => r.IsSummaryBeApproved).ToList();
            var approvedSummaryRequestDtos = new List<RequestDto>();

            foreach (var request in approvedSummaryRequests)
            {
                var productsInRequest = await _productRequestService.GetByRequestID(request.RequestID);
                var requestDto = _mapper.Map<RequestDto>(request);
                requestDto.Product_Requests = productsInRequest;
                approvedSummaryRequestDtos.Add(requestDto);
            }

            return approvedSummaryRequestDtos;
        }
        public async Task<List<RequestDto>> GetRequestsInDateRange(DateTime startDate, DateTime endDate)
        {
            var requests = await _requestRepository.GetAllInclude(r => r.Product_Requests);
            var filteredRequests = requests
                .Where(r => r.CreatedDate.Date >= startDate && r.CreatedDate.Date <= endDate && r.IsSummaryBeApproved)
                .ToList();

            return _mapper.Map<List<RequestDto>>(filteredRequests);
        }

        public async Task<List<RequestDto>> GetApprovedRequestsByDepartment(string department)
        {
            var requests = await _requestRepository.GetAllInclude(r => r.User);
            var approvedRequests = requests
                .Where(r => r.IsSummaryBeApproved && r.User != null && r.User.Department != null && r.User.Department.ToLower() == department.ToLower())
                .ToList();

            var approvedRequestDtos = _mapper.Map<List<RequestDto>>(approvedRequests);

            //foreach (var requestDto in approvedRequestDtos)
            //{
            //    var productsInRequest = await _productRequestService.GetByRequestID(requestDto.RequestID);
            //    requestDto.Product_Requests = productsInRequest;
            //}

            return approvedRequestDtos;
        }
        public async Task<List<RequestDto>> GetApprovedRequestsByDateRangeAndDepartment(DateTime startDate, DateTime endDate, string department)
        {
            var requests = await _requestRepository.GetAllInclude(r => r.User);
            var filteredRequests = requests
                .Where(r => r.IsSummaryBeApproved && r.CreatedDate.Date >= startDate && r.CreatedDate.Date <= endDate && r.User != null && r.User.Department != null && r.User.Department.ToLower() == department.ToLower())
                .ToList();

            var requestDtos = _mapper.Map<List<RequestDto>>(filteredRequests);

            //foreach (var requestDto in requestDtos)
            //{
            //    var productsInRequest = await _productRequestService.GetByRequestID(requestDto.RequestID);
            //    requestDto.Product_Requests = productsInRequest;
            //}

            return requestDtos;
        }









    }
}

