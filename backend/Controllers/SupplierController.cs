using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Route("api/supplier/{supplierId}")]
public class SupplierController : ControllerBase
{
    private readonly KafkaConsumerService _consumerService;

    public SupplierController(KafkaConsumerService consumerService)
    {
        _consumerService = consumerService;
    }

    [HttpGet("messages")]
    public async Task<IEnumerable<string>> GetTransactionHistoryForSupplier(int supplierId)
    {
        return await _consumerService.ConsumeMessagesAsync(GetTopicName(supplierId));
    }

    public string GetTopicName(int supplierId)
    {
        return $"transactions_{supplierId}";
    }
}