namespace Rabbit_SecondBus.Consumers;

public class SomeConsumer(ISecondBus secondBus) : IConsumer<ClassForConsume>
{
    private readonly ISecondBus _secondBus = secondBus;

    public async Task Consume(ConsumeContext<ClassForConsume> context)
    {
        try
        {
            //do something
            //var id = context.Message.Id;

            await _secondBus.Publish("some object");
        }
        catch (Exception e)
        {
            throw;
        }
    }
}