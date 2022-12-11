using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Categories
{
    public class GetCategories
    {
        public class CategoriesVm
        {
            public IList<CategoryLookupDto> Categories { get; set; }
        }

        public class CategoryLookupDto : IMapWith<Category>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Category, CategoryLookupDto>();
            }
        }

        public class GetCategorysQuery : IRequest<CategoriesVm>
        { }

        public class GetCategorysQueryHandler : IRequestHandler<GetCategorysQuery, CategoriesVm>
        {
            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetCategorysQueryHandler(IJobBoardDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoriesVm> Handle(GetCategorysQuery request, CancellationToken cancellationToken)
            {
                var entities = await _context.Categories
                    .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new CategoriesVm { Categories = entities };
            }
        }
    }
}