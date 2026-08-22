using AutoMapper;
using MediatR;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed class CreateEmailAddressHandler : IRequestHandler<CreateEmailAddressRequest, CreateEmailAddressResponse>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IEmailRepository _emailRepository;
    private readonly INewsPageRepository _newsPageRepository;
    private readonly IMapper _mapper;

    public CreateEmailAddressHandler
    (
        IUnityOfWork unityOfWork,
        IEmailRepository emailRepository,
        IMapper mapper,
        INewsPageRepository hostRepository
        )
    {
        _unityOfWork = unityOfWork;
        _emailRepository = emailRepository;
        _mapper = mapper;
        _newsPageRepository = hostRepository;
    }

    public async Task<CreateEmailAddressResponse> Handle(CreateEmailAddressRequest request, CancellationToken cancellationToken)
    {
        var email = _mapper.Map<Email>(request);

        var newsPagesByUrl = request.NewsPages
            .GroupBy(newsPage => NormalizeUrl(newsPage.Url))
            .ToDictionary(group => group.Key, group => group.First());

        var existingNewsPages = await _newsPageRepository.GetByUrls(newsPagesByUrl.Keys.ToArray(), cancellationToken);
        var existingNewsPagesByUrl = existingNewsPages.ToDictionary(newsPage => NormalizeUrl(newsPage.Url));

        foreach (var newsPage in newsPagesByUrl.Values)
        {
            var normalizedUrl = NormalizeUrl(newsPage.Url);
            var page = existingNewsPagesByUrl.GetValueOrDefault(normalizedUrl)
                ?? new NewsPage
                {
                    Url = normalizedUrl,
                    HeaderHost = newsPage.HeaderHost.Trim()
                };

            email.Hosts.Add(page);
        }

        _emailRepository.Create(email);
        await _unityOfWork.Save(cancellationToken);

        return _mapper.Map<CreateEmailAddressResponse>(email);
    }

    private static string NormalizeUrl(string url) => url.Trim().ToLowerInvariant();
}
