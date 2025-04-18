﻿using AutoMapper;
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
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ISummaryRepository _summaryRepository;
        public RequestService(IUserRepository userRepository,IRequestRepository requestRepository, IMapper mapper, IProduct_RequestService productRequestService, IProductService productService, ISummaryRepository summaryRepository)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            _productRequestService = productRequestService;
            _userRepository = userRepository;
            _productService = productService;
            _summaryRepository = summaryRepository; // Initialize the field
        }

        public async Task<RequestDto> Create(CreateRequestDto createRequest)
        {
            var newRequest = new Models.Request
            {
                UserID = createRequest.UserID,
                RequestCode = createRequest.RequestCode,
                TotalPrice = createRequest.TotalPrice,
                DateDepLeadApprove = createRequest.DateDepLeadApprove,
                NoteDepLead = createRequest.NoteDepLead,
                DateSupLeadApprove = createRequest.DateSupLeadApprove,
                NoteSupLead = createRequest.NoteSupLead
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
            requestDto.IsDeleted = request.IsDeleted; // Ensure this is included
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
            var updatedRequest = _mapper.Map<Models.Request>(updateRequest);
            updatedRequest.DateDepLeadApprove = updateRequest.DateDepLeadApprove;
            updatedRequest.NoteDepLead = updateRequest.NoteDepLead;
            updatedRequest.DateSupLeadApprove = updateRequest.DateSupLeadApprove;
            updatedRequest.NoteSupLead = updateRequest.NoteSupLead;
            return await _requestRepository.UpdateAsync(updateRequest.RequestID, updatedRequest);
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

        public async Task<bool> ApproveByDepLeader(int requestID, string note)
        {
            var request = await _requestRepository.GetByIdAsync(requestID);
            if (request != null)
            {
                request.IsProcessedByDepLead = true;
                request.IsApprovedByDepLead = true;
                request.DateDepLeadApprove = DateTime.UtcNow.AddHours(7); // Update the approval date
                request.NoteDepLead = note;
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

        public async Task<bool> ApproveByFinEmployee(int requestId, string note)
        {
            var requestEntity = await _requestRepository.GetByIdAsync(requestId);
            if (requestEntity == null)
            {
                return false;
            }

            requestEntity.IsApprovedBySupLead = true;
            requestEntity.NoteSupLead = note;
            requestEntity.DateSupLeadApprove = DateTime.UtcNow.AddHours(7); // Update the
            await _requestRepository.UpdateAsync(requestId, requestEntity);
            return true;
        }

        public async Task<List<RequestDto>> GetAllRequestsForFinEmployee()
        {
            var requests = await _requestRepository.GetAllAsync();
            return _mapper.Map<List<RequestDto>>(requests);
        }
        public async Task<bool> NotApproveRequestByDepLeader(int requestID, string note)
        {
            var request = await _requestRepository.GetByIdAsync(requestID);
            if (request != null)
            {
                request.IsProcessedByDepLead = true;
                request.IsApprovedByDepLead = false;
                request.DateDepLeadApprove = DateTime.UtcNow.AddHours(7); // Update the approval d
                request.NoteDepLead = note;
                await _requestRepository.UpdateAsync(requestID, request);
                return true;
            }
            return false;
        }
        public async Task<bool> NotApproveRequestByFinEmployee(int requestID, string note)
        {
            var request = await _requestRepository.GetByIdAsync(requestID);
            if (request != null)
            {
                request.IsProcessedByDepLead = false;
                request.IsApprovedByDepLead = true;
                request.NoteSupLead = note;
                request.DateSupLeadApprove = DateTime.UtcNow.AddHours(7); // Update the approval date
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

        public async Task<List<RequestDto>> GetRequestsByProductID(int productID)
        {
            var requests = await _requestRepository.GetAllInclude(r => r.Product_Requests);
            var filteredRequests = requests
                .Where(r => r.Product_Requests != null && r.Product_Requests.Any(pr => pr.ProductID == productID))
                .ToList();

            var requestDtos = _mapper.Map<List<RequestDto>>(filteredRequests);

            foreach (var requestDto in requestDtos)
            {
                var productsInRequest = await _productRequestService.GetByRequestID(requestDto.RequestID);
                requestDto.Product_Requests = productsInRequest;
            }

            return requestDtos;
        }
        public async Task<bool> RecalculateTotalPrice(int requestID)
        {
            var request = await _requestRepository.GetByIdAsync(requestID);
            if (request == null)
            {
                return false;
            }
            if (request.SummaryID != null)
            {
                //do nothing instead of return false
                return true;
            }
            var productRequests = await _productRequestService.GetByRequestID(requestID);
            int totalPrice = 0;

            foreach (var productRequest in productRequests)
            {
                var product = await _productService.GetById(productRequest.ProductID); // Fixed method name
                if (product != null)
                {
                    totalPrice += int.Parse(product.UnitPrice) * productRequest.Quantity;
                }
            }

            request.TotalPrice = totalPrice;
            return await _requestRepository.UpdateAsync(requestID, request);
        }
        public async Task<bool> RecalculateAllRequestsTotalPrice()
        {
            var requests = await _requestRepository.GetAllAsync();
            foreach (var request in requests)
            {
                var productRequests = await _productRequestService.GetByRequestID(request.RequestID);
                int totalPrice = 0;

                foreach (var productRequest in productRequests)
                {
                    var product = await _productService.GetById(productRequest.ProductID);
                    if (product != null)
                    {
                        totalPrice += int.Parse(product.UnitPrice) * productRequest.Quantity;
                    }
                }

                request.TotalPrice = totalPrice;
                await _requestRepository.UpdateAsync(request.RequestID, request);
            }
            return true;
        }

        public async Task<bool> AdjustDatesByAdding7Hours()
        {
            // Adjust dates for requests
            var requests = await _requestRepository.GetAllAsync();
            foreach (var request in requests)
            {
                request.DateDepLeadApprove = request.DateDepLeadApprove.AddHours(7);
                request.DateSupLeadApprove = request.DateSupLeadApprove.AddHours(7);
                await _requestRepository.UpdateAsync(request.RequestID, request);
            }

            // Adjust dates for summaries
            var summaries = await _summaryRepository.GetAllAsync();
            foreach (var summary in summaries)
            {
                if (summary.UpdateDate.HasValue)
                {
                    summary.UpdateDate = summary.UpdateDate.Value.AddHours(7);
                }
                await _summaryRepository.UpdateAsync(summary.SummaryID, summary);
            }

            return true;
        }

        public async Task<bool> ResetApprovalDatesAsync()
        {
            // Fetch all requests
            var allRequests = await _requestRepository.GetAllAsync();

            // Iterate through each request and reset dates based on conditions
            foreach (var requestEntity in allRequests)
            {
                if (!requestEntity.IsApprovedByDepLead)
                {
                    requestEntity.DateDepLeadApprove = DateTime.MinValue; // Reset to blank date
                }

                if (!requestEntity.IsApprovedBySupLead)
                {
                    requestEntity.DateSupLeadApprove = DateTime.MinValue; // Reset to blank date
                }

                // Update the request in the database
                await _requestRepository.UpdateAsync(requestEntity.RequestID, requestEntity);
            }

            return true;
        }






    }
}

