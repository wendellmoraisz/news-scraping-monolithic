using AutoMapper;
using MediatR;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed class CreateEmailAddressHandler : IRequestHandler<CreateEmailAddressRequest, CreateEmailAddressResponse>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IEmailRepository _emailRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IMapper _mapper;

    public CreateEmailAddressHandler
    (
        IUnityOfWork unityOfWork,
        IEmailRepository emailRepository,
        IMapper mapper,
        IHostRepository hostRepository
        )
    {
        _unityOfWork = unityOfWork;
        _emailRepository = emailRepository;
        _mapper = mapper;
        _hostRepository = hostRepository;
    }

    public async Task<CreateEmailAddressResponse> Handle(CreateEmailAddressRequest request, CancellationToken cancellationToken)
    {
        var email = _mapper.Map<Email>(request);

        var hostAdresses = request.Hosts
        .Distinct()
        .Select(hostAddress => hostAddress.Trim().ToLowerInvariant())
        .ToArray();

        var existingHosts = await _hostRepository.GetByAddresses(hostAdresses, cancellationToken);
        var hostsByAddress = existingHosts.ToDictionary(host => host.Address);

        foreach (var hostAddress in request.Hosts)
        {
            var host = hostsByAddress.GetValueOrDefault(hostAddress)
                ?? new Host { Address = hostAddress };

            email.Hosts.Add(host);
        }

        _emailRepository.Create(email);
        await _unityOfWork.Save(cancellationToken);

        return _mapper.Map<CreateEmailAddressResponse>(email);
    }
}
