using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using Xunit;

namespace SWAG.Test.Controllers
{
    public class OperationsControllerTest
        : ControllerBaseTest
    {
        public OperationsControllerTest()
            : base()
        { }

        [Theory(DisplayName = nameof(Operations))]
        [Trait("Category", "Controller")]
        [InlineData(OperationType.Addition, new Double[] { 1D, 2D, 3D, 4D }, 10D)]
        [InlineData(OperationType.Subtraction, new Double[] { 10D, 20D, 3D, 4D }, -17D)]
        [InlineData(OperationType.Multiplication, new Double[] { 5D, 5D, 10D }, 250D)]
        [InlineData(OperationType.Division, new Double[] { 51D, 5.1D, 10D }, 1D)]
        [InlineData(OperationType.Exponentiation, new Double[] { 2D, 170D, 5D }, 7.5075168288047002299711576955093e+255D)]
        public async void Operations(OperationType type, Double[] values, Double result)
        {
            OperationModel data = new OperationModel
            {
                Type = type,
                Value = values,
            };

            using (HttpClient client = Client)
            {
                using (HttpResponseMessage response = await client.PostAsync($"{Version}/operations",
                    data.ToStringContent()))
                {
                    response.EnsureSuccessStatusCode();

                    String content = await response.Content.ReadAsStringAsync();
                    Assert.False(String.IsNullOrEmpty(content));

                    OperationModel operation = JsonConvert.DeserializeObject<OperationModel>(content);

                    Assert.NotNull(operation);

                    Assert.NotNull(Context.Operations);
                    Assert.NotEmpty(Context.Operations);
                    Assert.Single(Context.Operations, (e) => { return e.Id == operation.Id; });

                    Assert.Equal(data.Type, operation.Type);
                    Assert.Equal(data.Value, operation.Value);

                    Assert.Equal(result, operation.Result);
                }
            }
        }

        [Theory(DisplayName = nameof(Operations_Fails))]
        [Trait("Category", "Controller")]
        [InlineData(OperationType.None, new Double[] { 1D, 2D, 3D, 4D })]
        [InlineData(OperationType.Division, new Double[] { 51D, 0D, 10D })]
        public async void Operations_Fails(OperationType type, Double[] values)
        {
            OperationModel data = new OperationModel
            {
                Type = type,
                Value = values,
            };

            using (HttpClient client = Client)
            {
                using (HttpResponseMessage response = await client.PostAsync($"{Version}/operations",
                    data.ToStringContent()))
                {
                    Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());

                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                }
            }
        }

        [Theory]
        [Trait("Category", "Performance")]
        [InlineData(30, 1500, 10)]
        public void Events_Generate_Stress(Int16 maxBatchSize, Int64 maxEntries, Int32 maxThreads)
        {
            SpawnAndWait(() =>
            {
                for (Int32 i = 0; i < maxEntries; i++)
                {
                    using (HttpClient client = Client)
                    {
                        List<Data.EventEntity> dataEvents = new List<Data.EventEntity>();

                        for (Int32 s = 0; s < maxBatchSize; s++)
                        {
                            dataEvents.Add(new Data.EventEntity
                            {
                                //Application = Constant.Event.XUnit,
                                Code = 0x1,
                                //Activity = Guid.NewGuid(),
                                Level = EventLevel.Debug,
                                Message = $"Stress test entry {new Random().Next()}",
                            });
                        }

                        using (HttpResponseMessage response = client.PostAsync($"{Version}/events",
                            null).Result)//Mapper.Map<IEnumerable<EventModel>>(dataEvents).ToStringContent()
                        {
                            response.EnsureSuccessStatusCode();

                            Console.WriteLine($"Stress test entry {i} in {Thread.CurrentThread.ManagedThreadId}");
                        }
                    }
                }
            }, maxThreads);
        }
    }
}
