using MediatR;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Commands
{
    public class SetUpdateDateToCreatedDateHandler : IRequestHandler<SetUpdateDateToCreatedDateCommand, bool>
    {
        private readonly ISummaryService _summaryService;

        public SetUpdateDateToCreatedDateHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<bool> Handle(SetUpdateDateToCreatedDateCommand request, CancellationToken cancellationToken)
        {
            return await _summaryService.SetUpdateDateToCreatedDate();
        }
    }
}
