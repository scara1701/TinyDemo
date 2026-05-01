
using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.WebAPI.Endpoints
{
    public static class LottoEndpoints
    {
        public static RouteGroupBuilder MapLottoEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/lotto");

            group.MapGet("/", HandleGetLotto)
                .WithName("GetLotto")
                .Produces<Lotto>()
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            return group;
        }

        private static async Task<IResult> HandleGetLotto(ILottoService lottoService)
        {
            try
            {
                var lotto = await lottoService.GenerateLotto();
                return TypedResults.Ok(lotto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(
                    title: "Server Exception",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}