using MS2Project.Application.Services.Emails;
using MS2Project.Domain.PaymentAggregate;

namespace MS2Project.Application.Features.Payments.Commands;

public record SendEmailAfterPaymentCommand : InternalCommandBase<Unit>
{
    public PaymentId PaymentId { get; }

    [JsonConstructor]
    public SendEmailAfterPaymentCommand(Guid id, PaymentId paymentId) : base(id)
    {
        PaymentId = paymentId;
    }
}

public class SendEmailAfterPaymentCommandHandler : ICommandHandler<SendEmailAfterPaymentCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IPaymentRepository _paymentRepository;

    public SendEmailAfterPaymentCommandHandler(
        IEmailSender emailSender,
        IPaymentRepository paymentRepository)
    {
        _emailSender = emailSender.ThrowIfNull();
        _paymentRepository = paymentRepository.ThrowIfNull();
    }

    public async Task<Unit> Handle(SendEmailAfterPaymentCommand request, CancellationToken cancellationToken)
    {
        // Logic of preparing an email. This is only mock.
        var emailMessage = new EmailMessage("from@email.com", "to@email.com", "content");

        await _emailSender.SendEmailAsync(emailMessage);

        var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

        payment.MarkEmailNotificationIsSent();

        return Unit.Value;
    }
}
