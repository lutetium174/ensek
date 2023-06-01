using Domain.MeterReadings;
using Domain.MeterReadings.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Domain.Web;

public record UploadFileResponse(
    List<MeterReading> Successful, 
    List<MeterReading> Failed);

public record UploadFileCommand(IFormFile File) : IRequest<UploadFileResponse>;

public class FileInputHandler
    : IRequestHandler<UploadFileCommand, UploadFileResponse>
{
    private readonly IMediator _mediator;
    private readonly IMeterReadingsInput _readingsInput;
    public FileInputHandler(
        IMediator mediator,
        IMeterReadingsInput readingsInput)
    {
        _mediator = mediator;
        _readingsInput = readingsInput;
    }

    public async Task<UploadFileResponse> Handle(
        UploadFileCommand request,
        CancellationToken cancellation)
    {
        using var stream = request.File.OpenReadStream();
        var readings = await _readingsInput.Load(stream);

        var (successful, failed) = await _mediator.Send(new ImportMeterReadingsCommand(readings), cancellation);
        return new UploadFileResponse(successful.ToList(), failed.ToList());
    }
}