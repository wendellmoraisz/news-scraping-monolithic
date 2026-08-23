using AutoMapper;
using MediatR;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateNewsPage;

public sealed class CreateNewsPageHandler : IRequestHandler<CreateNewsPageRequest, CreateNewsPageResponse>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly INewsPageRepository _newsPageRepository;
    private readonly IMapper _mapper;

    public CreateNewsPageHandler(
        IUnityOfWork unityOfWork,
        INewsPageRepository newsPageRepository,
        IMapper mapper)
    {
        _unityOfWork = unityOfWork;
        _newsPageRepository = newsPageRepository;
        _mapper = mapper;
    }

    public async Task<CreateNewsPageResponse> Handle(CreateNewsPageRequest request, CancellationToken cancellationToken)
    {
        var newsPage = _mapper.Map<NewsPage>(request);

        _newsPageRepository.Create(newsPage);
        await _unityOfWork.Save(cancellationToken);

        return _mapper.Map<CreateNewsPageResponse>(newsPage);
    }
}