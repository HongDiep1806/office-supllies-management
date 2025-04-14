
using AutoMapper;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Add logging
using ClosedXML.Excel;
using System.Globalization;
using System.Globalization;
namespace Office_supplies_management.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SummaryService> _logger;
        private readonly IProductService _productService; // Add this field

        public SummaryService(
            IUserRepository userRepository,
            ISummaryRepository summaryRepository,
            IRequestRepository requestRepository,
            IMapper mapper,
            ILogger<SummaryService> logger,
            IProductService productService) // Add this parameter
        {
            _summaryRepository = summaryRepository;
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _productService = productService; // Initialize the field
        }
        public async Task<SummaryDto> CreateSummary(CreateSummaryDto createSummaryDto)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsOfSummary = requests.Where(r => createSummaryDto.RequestIDs.Contains(r.RequestID)).ToList();
            var newSummary = new Summary
            {
                SummaryCode = createSummaryDto.SummaryCode,
                UserID = createSummaryDto.UserID,
                Requests = requestsOfSummary,
                TotalPrice = requestsOfSummary.Sum(r => r.TotalPrice),
                //IsProcessedBySupLead = true,
            };

           await _summaryRepository.CreateAsync(newSummary);
            foreach (var request in requestsOfSummary)
            {
                request.SummaryID = newSummary.SummaryID;
                request.IsCollectedInSummary = true;
                await _requestRepository.UpdateAsync(request.RequestID, request);
            }
            return _mapper.Map<SummaryDto>(newSummary);
        }

        public async Task<bool> UpdateSummary(UpdateSummaryDto updateSummaryDto)
        {
            var summary = await _summaryRepository.GetByIdAsync(updateSummaryDto.SummaryID);
            if (summary == null)
            {
                return false;
            }

            summary.IsProcessedBySupLead = updateSummaryDto.IsProcessedBySupLead;
            summary.IsApprovedBySupLead = updateSummaryDto.IsApprovedBySupLead;
            await _summaryRepository.UpdateAsync(summary.SummaryID, summary);
            return true;
        }

        public async Task<List<SummaryDto>> GetAllSummaries()
        {
            var summaries = await _summaryRepository.GetAllAsync();
            return _mapper.Map<List<SummaryDto>>(summaries);
        }

        public async Task<List<SummaryDto>> GetSummariesByUserId(int userId)
        {
            var summaries = await _summaryRepository.GetAllAsync();
            var userSummaries = summaries.Where(s => s.UserID == userId).ToList();
            return _mapper.Map<List<SummaryDto>>(userSummaries);
        }

        public async Task<SummaryDto> GetSummaryById(int summaryId)
        {
            var summaries = await _summaryRepository.GetAllInclude( s => s.Requests);
            var currentSummary = summaries.FirstOrDefault(s => s.SummaryID == summaryId);
            return _mapper.Map<SummaryDto>(currentSummary);
        }

        public async Task<List<DepartmentUsageReportDto>> GetDepartmentUsageReport(string department, DateTime startDate, DateTime endDate)
        {
            //_logger.LogInformation("Fetching summaries between {StartDate} and {EndDate}", startDate, endDate);
            var summaries = await _summaryRepository.GetAllAsync();
            var filteredSummaries = summaries
                .Where(s => s.CreatedDate.Date >= startDate.Date && s.CreatedDate.Date <= endDate.Date && s.IsApprovedBySupLead)
                .ToList();

            //_logger.LogInformation("Found {Count} summaries in the date range", filteredSummaries.Count);
            //foreach (var summary in filteredSummaries)
            //{
            //    _logger.LogInformation("SummaryID: {SummaryID}, CreatedDate: {CreatedDate}, IsApprovedBySupLead: {IsApprovedBySupLead}", summary.SummaryID, summary.CreatedDate, summary.IsApprovedBySupLead);
            //}

            var summaryIds = filteredSummaries.Select(s => s.SummaryID).ToList();

            var users = await _userRepository.GetAllAsync();
            var userIds = users
                .Where(u => u.Department == department)
                .Select(u => u.UserID)
                .ToList();

            //_logger.LogInformation("Found {Count} users in the department {Department}", userIds.Count, department);

            var requests = await _requestRepository.GetAllAsync();
            var filteredRequests = requests
                .Where(r => userIds.Contains(r.UserID) && summaryIds.Contains(r.SummaryID ?? 0))
                .ToList();

            //_logger.LogInformation("Found {Count} requests matching the criteria", filteredRequests.Count);
            //foreach (var request in filteredRequests)
            //{
            //    _logger.LogInformation("RequestID: {RequestID}, UserID: {UserID}, SummaryID: {SummaryID}, TotalPrice: {TotalPrice}", request.RequestID, request.UserID, request.SummaryID, request.TotalPrice);
            //}

            var report = filteredRequests
                .GroupBy(r => r.User.Department)
                .Select(g => new DepartmentUsageReportDto
                {
                    Department = g.Key,
                    TotalAmount = g.Sum(r => r.TotalPrice)
                })
                .ToList();

            return report;
        }

        public async Task<List<SummaryDto>> GetSummariesByDateRange(DateTime startDate, DateTime endDate)
        {
            var summaries = await _summaryRepository.GetAllAsync();
            var filteredSummaries = summaries
                .Where(s => s.CreatedDate.Date >= startDate && s.CreatedDate.Date <= endDate)
                .ToList();

            return _mapper.Map<List<SummaryDto>>(filteredSummaries);
        }
        public async Task<List<RequestDto>> GetRequestsBySummaryId(int summaryId)
        {
            var requests = await _requestRepository.GetAllAsync();
            var filteredRequests = requests.Where(r => r.SummaryID == summaryId).ToList();
            return _mapper.Map<List<RequestDto>>(filteredRequests);
        }
        public async Task<List<DepartmentCostDto>> GetDepartmentCosts(DateTime startDate, DateTime endDate)
        {
            // Get all unique departments
            var users = await _userRepository.GetAllAsync();
            var departments = users.Select(u => u.Department).Distinct().ToList();

            // Get all requests where the summary is approved and within the date range
            var requests = await _requestRepository.GetAllAsync();
            var filteredRequests = requests
                .Where(r => r.SummaryID.HasValue && r.IsSummaryBeApproved && r.CreatedDate.Date >= startDate && r.CreatedDate.Date <= endDate)
                .ToList();

            // Log the filtered requests
            //foreach (var request in filteredRequests)
            //{
            //    _logger.LogInformation("RequestID: {RequestID}, UserID: {UserID}, Department: {Department}, CreatedDate: {CreatedDate}, TotalPrice: {TotalPrice}",
            //        request.RequestID, request.UserID, request.User?.Department, request.CreatedDate, request.TotalPrice);
            //}

            // Pair each request to the department of the user who made the request and sum the cost
            var departmentCosts = filteredRequests
                .GroupBy(r => r.User?.Department ?? "Unknown")
                .Select(g => new DepartmentCostDto
                {
                    Department = g.Key,
                    Cost = g.Sum(r => r.TotalPrice)
                })
                .ToList();

            // Ensure all departments are included in the result, even if they have no requests
            foreach (var department in departments)
            {
                if (!departmentCosts.Any(dc => dc.Department == department))
                {
                    departmentCosts.Add(new DepartmentCostDto
                    {
                        Department = department,
                        Cost = 0
                    });
                }
            }

            return departmentCosts;
        }

        public async Task<List<SummaryDto>> GetAll()
        {
            var summaries = await _summaryRepository.GetAllInclude(s => s.Requests);
            return _mapper.Map < List<SummaryDto>>(summaries);

        }
        public async Task<Dictionary<int, List<RequestDto>>> GetApprovedSummariesWithRequests()
        {
            var approvedSummaries = await GetApprovedSummariesAsync();
            var result = new Dictionary<int, List<RequestDto>>();

            foreach (var summary in approvedSummaries)
            {
                var requests = await GetRequestsBySummaryId(summary.SummaryID);
                result.Add(summary.SummaryID, requests);
            }

            return result;
        }

        private async Task<List<Summary>> GetApprovedSummariesAsync()
        {
            var summaries = await _summaryRepository.GetAllAsync();
            return summaries.Where(s => s.IsApprovedBySupLead).ToList();
        }

        public async Task<List<RequestDto>> GetSummariesWithRequestsByDateRange(DateTime startDate, DateTime endDate)
        {
            var summaries = await _summaryRepository.GetAllAsync();
            var filteredSummaries = summaries.Where(s => s.CreatedDate.Date >= startDate && s.CreatedDate.Date <= endDate && s.IsApprovedBySupLead).ToList();
            var result = new Dictionary<int, List<RequestDto>>();
            var results = new List<RequestDto>();   
            foreach (var summary in filteredSummaries)
            {
                var requests = await GetRequestsBySummaryIdWithProductDetails(summary.SummaryID);
                results.AddRange(requests);
            }

            return results;
        }

        private async Task<List<RequestDto>> GetRequestsBySummaryIdWithProductDetails(int summaryId)
        {
            var requests = await _requestRepository.GetAllInclude(r => r.Product_Requests);
            var filteredRequests = requests.Where(r => r.SummaryID == summaryId).ToList();
            return _mapper.Map<List<RequestDto>>(filteredRequests);
        }

        public async Task<int> CountSummaries()
        {
            return await _summaryRepository.Count();
        }
        public async Task<bool> UpdateSummaryApprovalAsync(int summaryId, bool isApproved)
        {
            var summary = await _summaryRepository.GetByIdAsync(summaryId);
            if (summary == null)
            {
                return false;
            }

            summary.IsApprovedBySupLead = isApproved;
            summary.UpdateDate = DateTime.UtcNow.AddHours(7); // Adjust to GMT+7
            await _summaryRepository.UpdateAsync(summary.SummaryID, summary);

            if (!isApproved)
            {
                var requests = await _requestRepository.GetAllAsync();
                var requestsInSummary = requests.Where(r => r.SummaryID == summaryId).ToList();

                foreach (var request in requestsInSummary)
                {
                    request.IsProcessedByDepLead = false;
                    request.IsApprovedByDepLead = true;
                    request.IsApprovedBySupLead = false;
                    await _requestRepository.UpdateAsync(request.RequestID, request);
                }
            }

            return true;
        }
        public async Task<SummaryDto> GetSummaryByCodeAsync(string summaryCode)
        {
            var summaries = await _summaryRepository.GetAllAsync();
            var summary = summaries.FirstOrDefault(s => s.SummaryCode == summaryCode);
            return _mapper.Map<SummaryDto>(summary);
        }
        public async Task<bool> RecalculateAllSummariesTotalPrice()
        {
            var summaries = await _summaryRepository.GetAllAsync();
            foreach (var summary in summaries)
            {
                var requests = await _requestRepository.GetAllAsync();
                var requestsOfSummary = requests.Where(r => r.SummaryID == summary.SummaryID).ToList();
                summary.TotalPrice = requestsOfSummary.Sum(r => r.TotalPrice);
                await _summaryRepository.UpdateAsync(summary.SummaryID, summary);
            }
            return true;
        }
        public async Task<bool> SetUpdateDateToCreatedDate()
        {
            var summaries = await _summaryRepository.GetAllAsync();
            foreach (var summary in summaries)
            {
                summary.UpdateDate = summary.CreatedDate; // Set UpdateDate to CreatedDate
                await _summaryRepository.UpdateAsync(summary.SummaryID, summary);
            }
            return true;
        }
        public async Task<Dictionary<string, int>> GetProductCountForApprovedSummariesInDateRange(DateTime startDate, DateTime endDate)
        {
            // Step 1: Get summaries that are approved and within the date range
            var summaries = await _summaryRepository.GetAllAsync();
            var filteredSummaryIds = summaries
                .Where(s => s.CreatedDate.Date >= startDate && s.CreatedDate.Date <= endDate && s.IsApprovedBySupLead)
                .Select(s => s.SummaryID)
                .ToList();

            if (!filteredSummaryIds.Any())
            {
                return new Dictionary<string, int>();
            }

            // Step 2: Get requests associated with the filtered summaries
            var requests = await _requestRepository.GetAllAsync();
            var filteredRequestIds = requests
                .Where(r => filteredSummaryIds.Contains(r.SummaryID ?? 0))
                .Select(r => r.RequestID)
                .ToList();

            if (!filteredRequestIds.Any())
            {
                return new Dictionary<string, int>();
            }

            // Step 3: Get product counts from Product_Requests
            var productRequests = await _requestRepository.GetAllInclude(r => r.Product_Requests);
            var filteredProductRequests = productRequests
                .Where(r => filteredRequestIds.Contains(r.RequestID))
                .SelectMany(r => r.Product_Requests)
                .GroupBy(pr => pr.ProductID)
                .ToDictionary(
                    g => g.Key, // ProductID
                    g => g.Sum(pr => pr.Quantity) // Total Quantity
                );

            if (!filteredProductRequests.Any())
            {
                return new Dictionary<string, int>();
            }

            // Step 4: Map ProductID to ProductName and include the count
            var productCounts = new Dictionary<string, int>();
            foreach (var productRequest in filteredProductRequests)
            {
                var product = await _productService.GetById(productRequest.Key);
                if (product != null)
                {
                    productCounts[product.Name] = productRequest.Value;
                }
            }

            return productCounts;
        }
        public async Task<byte[]> GenerateProductReportExcel(DateTime startDate, DateTime endDate)
        {
            // Step 1: Get product counts
            var productCounts = await GetProductCountForApprovedSummariesInDateRange(startDate, endDate);
            _logger.LogInformation("Product counts: {@ProductCounts}", productCounts);

            using (var workbook = new XLWorkbook())
            {
                // Set culture to use comma as thousands separator

                var worksheet = workbook.Worksheets.Add("Product Report");
                CultureInfo cultureInfo = new CultureInfo("en-US");
                // Step 2: Add headers
                var titleCell = worksheet.Cell(1, 1);
                titleCell.Value = "Báo cáo chi tiết sản phẩm trong phiếu yêu cầu đã được tổng hợp";
                titleCell.Style.Font.Bold = true;
                worksheet.Range(1, 1, 1, 5).Merge();
                worksheet.Row(1).Height = 20;

                worksheet.Cell(2, 1).Value = "Từ:";
                worksheet.Cell(2, 2).Value = startDate.ToString("dd/MM/yyyy");
                worksheet.Cell(2, 3).Value = "Đến:";
                worksheet.Cell(2, 4).Value = endDate.ToString("dd/MM/yyyy");

                // Step 3: Add table headers
                worksheet.Cell(4, 1).Value = "STT";
                worksheet.Cell(4, 2).Value = "Tên sản phẩm";
                worksheet.Cell(4, 3).Value = "Đơn Giá Hiện Hành";
                worksheet.Cell(4, 4).Value = "Số lượng";
                worksheet.Cell(4, 5).Value = "Tổng";

                // Make table headers bold
                worksheet.Range(4, 1, 4, 5).Style.Font.Bold = true;

                int row = 5;
                int index = 1;
                decimal totalSum = 0;

                // Step 4: Populate data
                foreach (var product in productCounts)
                {
                    var productName = product.Key;
                    var products = await _productService.SearchProductsAsync(productName, null, null, null);
                    var productDetails = products?.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                    if (productDetails == null)
                    {
                        _logger.LogWarning($"Product not found: {productName}");
                        continue;
                    }

                    if (!decimal.TryParse(productDetails.UnitPrice, out var unitPrice))
                    {
                        _logger.LogWarning($"Invalid unit price for product: {productName}");
                        continue;
                    }

                    decimal rowTotal = unitPrice * product.Value;
                    totalSum += rowTotal;

                    worksheet.Cell(row, 1).Value = index++;
                    worksheet.Cell(row, 2).Value = productName;

                    var unitPriceCell = worksheet.Cell(row, 3);
                    unitPriceCell.Value = unitPrice;
                    unitPriceCell.Style.NumberFormat.Format = "#,##0";

                    var quantityCell = worksheet.Cell(row, 4);
                    quantityCell.Value = product.Value;
                    quantityCell.Style.NumberFormat.Format = "#,##0";

                    var totalCell = worksheet.Cell(row, 5);
                    totalCell.Value = rowTotal;
                    totalCell.Style.NumberFormat.Format = "#,##0";

                    row++;
                }

                // Step 5: Add total row
                worksheet.Cell(row, 4).Value = "Tổng giá trị:";
                worksheet.Cell(row, 4).Style.Font.Bold = true;

                var sumCell = worksheet.Cell(row, 5);
                sumCell.Value = totalSum;
                sumCell.Style.NumberFormat.Format = "#,##0";
                sumCell.Style.Font.Bold = true;

                // Step 6: Adjust column widths
                worksheet.Columns(1, 5).AdjustToContents();

                // Step 7: Return Excel file as byte array
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
        public async Task<byte[]> GenerateSummaryDetailExcel(int summaryId)
        {
            var summary = await _summaryRepository.GetByIdIncludeAsync(summaryId, s => s.Requests);
            if (summary == null)
                throw new KeyNotFoundException($"Summary with ID {summaryId} not found.");

            using (var workbook = new XLWorkbook())
            {
                // Set culture to use comma as thousands separator
               

                var worksheet = workbook.Worksheets.Add("Summary Detail");
                CultureInfo cultureInfo = new CultureInfo("en-US");
                // Add title
                worksheet.Cell(1, 1).Value = $"Phiếu tổng hợp VPP {summary.SummaryCode}";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Range(1, 1, 1, 6).Merge();
                User user = await _userRepository.GetByIdAsync(summary.UserID);
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Add metadata
                worksheet.Cell(2, 1).Value = $"Người thực hiện: {user.FullName}";
                worksheet.Cell(2, 5).Value = $"Phòng Ban: {user.Department}";
                worksheet.Cell(3, 5).Value = $"Ngày thực hiện: {summary.CreatedDate:dd/MM/yyyy}";

                var statusCell = worksheet.Cell(4, 1);
                if (!summary.IsProcessedBySupLead && !summary.IsApprovedBySupLead)
                {
                    statusCell.Value = "Chưa duyệt";
                    statusCell.Style.Fill.BackgroundColor = XLColor.Orange;
                }
                else if (summary.IsProcessedBySupLead && summary.IsApprovedBySupLead)
                {
                    statusCell.Value = "Đã duyệt";
                    statusCell.Style.Fill.BackgroundColor = XLColor.Green;
                }
                else if (summary.IsProcessedBySupLead && !summary.IsApprovedBySupLead)
                {
                    statusCell.Value = "Không duyệt";
                    statusCell.Style.Fill.BackgroundColor = XLColor.Red;
                }
                statusCell.Style.Font.Bold = true;
                statusCell.Style.Font.FontColor = XLColor.White;

                // Add table headers
                worksheet.Cell(5, 1).Value = "STT";
                worksheet.Cell(5, 2).Value = "Tên VPP";
                worksheet.Cell(5, 3).Value = "Đơn vị tính";
                worksheet.Cell(5, 4).Value = "Số lượng";
                worksheet.Cell(5, 5).Value = "Đơn giá dự kiến";
                worksheet.Cell(5, 6).Value = "Thành tiền";

                worksheet.Range(5, 1, 5, 6).Style.Font.Bold = true;

                // Populate data
                int row = 6;
                var requests = await _requestRepository.GetAllInclude(r => r.Product_Requests);
                var filteredRequests = requests.Where(r => r.SummaryID == summaryId).ToList();
                decimal totalSum = 0;

                foreach (var request in filteredRequests)
                {
                    User requestUser = await _userRepository.GetByIdAsync(request.UserID);
                    // Add "Phiếu yêu cầu" row
                    worksheet.Cell(row, 1).Value = $"Phiếu: {request.RequestCode} - {requestUser.FullName} - {requestUser.Department} - {request.CreatedDate:dd/MM/yyyy}";
                    worksheet.Range(row, 1, row, 6).Merge();
                    worksheet.Cell(row, 6).Style.Fill.BackgroundColor = XLColor.LightGray;
                    worksheet.Row(row).Style.Font.Bold = true;
                    row++;

                    int index = 1;
                    foreach (var productRequest in request.Product_Requests)
                    {
                        var product = await _productService.GetById(productRequest.ProductID);
                        decimal unitPrice = decimal.Parse(product.UnitPrice);
                        decimal rowTotal = unitPrice * productRequest.Quantity;
                        totalSum += rowTotal;

                        worksheet.Cell(row, 1).Value = index++;
                        worksheet.Cell(row, 2).Value = product.Name;
                        worksheet.Cell(row, 3).Value = product.UnitCurrency;

                        var quantityCell = worksheet.Cell(row, 4);
                        quantityCell.Value = productRequest.Quantity;
                        quantityCell.Style.NumberFormat.Format = "#,##0";

                        var unitPriceCell = worksheet.Cell(row, 5);
                        unitPriceCell.Value = unitPrice;
                        unitPriceCell.Style.NumberFormat.Format = "#,##0";

                        var totalCell = worksheet.Cell(row, 6);
                        totalCell.Value = rowTotal;
                        totalCell.Style.NumberFormat.Format = "#,##0";

                        row++;
                    }
                }

                // Add total row
                worksheet.Cell(row, 5).Value = "Tổng cộng:";
                worksheet.Cell(row, 5).Style.Font.Bold = true;

                var sumCell = worksheet.Cell(row, 6);
                sumCell.Value = totalSum;
                sumCell.Style.NumberFormat.Format = "#,##0";
                sumCell.Style.Font.Bold = true;

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                // Return Excel file as byte array
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
        public async Task<byte[]> GenerateApprovedRequestsExcel(DateTime startDate, DateTime endDate, string? department)
        {
            var summaries = await _summaryRepository.GetAllAsync();
            var approvedSummaries = summaries
                .Where(s => s.IsApprovedBySupLead)
                .ToList();

            var summaryIds = approvedSummaries.Select(s => s.SummaryID).ToList();

            var users = await _userRepository.GetAllAsync();
            var userIds = string.IsNullOrEmpty(department)
                ? users.Select(u => u.UserID).ToList()
                : users.Where(u => u.Department == department).Select(u => u.UserID).ToList();

            var requests = await _requestRepository.GetAllInclude(r => r.Product_Requests);
            var filteredRequests = requests
                .Where(r => r.CreatedDate.Date >= startDate.Date && r.CreatedDate.Date <= endDate.Date &&
                       summaryIds.Contains(r.SummaryID ?? 0) && userIds.Contains(r.UserID))
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                // Set culture to use comma as thousands separator

                var worksheet = workbook.Worksheets.Add("Yêu cầu đã duyệt");
                CultureInfo cultureInfo = new CultureInfo("en-US");
                // Add title
                worksheet.Cell(1, 1).Value = "Báo cáo chi tiết yêu cầu đã được duyệt";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Range(1, 1, 1, 6).Merge();
                worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Add metadata
                worksheet.Cell(2, 1).Value = "Từ:";
                worksheet.Cell(2, 2).Value = startDate.ToString("dd/MM/yyyy");
                worksheet.Cell(2, 3).Value = "Đến:";
                worksheet.Cell(2, 4).Value = endDate.ToString("dd/MM/yyyy");
                if (!string.IsNullOrEmpty(department))
                {
                    worksheet.Cell(3, 1).Value = "Phòng ban:";
                    worksheet.Cell(3, 2).Value = department;
                }

                // Add table headers
                worksheet.Cell(5, 1).Value = "STT";
                worksheet.Cell(5, 2).Value = "Tên VPP";
                worksheet.Cell(5, 3).Value = "Đơn vị tính";
                worksheet.Cell(5, 4).Value = "Số lượng";
                worksheet.Cell(5, 5).Value = "Đơn giá dự kiến";
                worksheet.Cell(5, 6).Value = "Thành tiền";

                worksheet.Range(5, 1, 5, 6).Style.Font.Bold = true;

                // Populate data
                int row = 6;
                decimal totalSum = 0;

                foreach (var request in filteredRequests)
                {
                    var user = await _userRepository.GetByIdAsync(request.UserID);

                    // Add PYC row
                    worksheet.Cell(row, 1).Value = $"Phiếu: {request.RequestCode} - {user.FullName} - {user.Department} - {request.CreatedDate:dd/MM/yyyy}";
                    worksheet.Range(row, 1, row, 6).Merge();
                    worksheet.Row(row).Style.Font.Bold = true;
                    row++;

                    // Add product request details
                    int productIndex = 1;
                    foreach (var productRequest in request.Product_Requests)
                    {
                        var product = await _productService.GetById(productRequest.ProductID);
                        decimal unitPrice = decimal.Parse(product.UnitPrice);
                        decimal rowTotal = unitPrice * productRequest.Quantity;
                        totalSum += rowTotal;

                        worksheet.Cell(row, 1).Value = productIndex++;
                        worksheet.Cell(row, 2).Value = product.Name;
                        worksheet.Cell(row, 3).Value = product.UnitCurrency;

                        var quantityCell = worksheet.Cell(row, 4);
                        quantityCell.Value = productRequest.Quantity;
                        quantityCell.Style.NumberFormat.Format = "#,##0";

                        var unitPriceCell = worksheet.Cell(row, 5);
                        unitPriceCell.Value = unitPrice;
                        unitPriceCell.Style.NumberFormat.Format = "#,##0";

                        var totalCell = worksheet.Cell(row, 6);
                        totalCell.Value = rowTotal;
                        totalCell.Style.NumberFormat.Format = "#,##0";

                        row++;
                    }
                }

                // Add total row
                worksheet.Cell(row, 5).Value = "Tổng cộng:";
                worksheet.Cell(row, 5).Style.Font.Bold = true;

                var sumCell = worksheet.Cell(row, 6);
                sumCell.Value = totalSum;
                sumCell.Style.NumberFormat.Format = "#,##0";
                sumCell.Style.Font.Bold = true;

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }










    }
}
