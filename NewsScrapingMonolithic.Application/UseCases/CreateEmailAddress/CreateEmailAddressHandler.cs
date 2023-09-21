using AutoMapper;
using MediatR;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed class CreateEmailAddressHandler : IRequestHandler<CreateEmailAddressRequest, CreateEmailAddressResponse>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IEmailRepository _emailRepository;
    private readonly IMapper _mapper;

    public CreateEmailAddressHandler
    (
        IUnityOfWork unityOfWork,
        IEmailRepository emailRepository,
        IMapper mapper
        )
    {
        _unityOfWork = unityOfWork;
        _emailRepository = emailRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateEmailAddressResponse> Handle(CreateEmailAddressRequest request, CancellationToken cancellationToken)
    {
        var email = _mapper.Map<Email>(request);
        _emailRepository.Create(email);
        await _unityOfWork.Save(cancellationToken);

        return _mapper.Map<CreateEmailAddressResponse>(email);
    }
}