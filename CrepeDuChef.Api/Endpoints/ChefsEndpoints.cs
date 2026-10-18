using CrepeDuChef.Api.Contracts.Chefs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Models.Requests.Chefs;

using Microsoft.AspNetCore.Mvc;

using AppDtos = CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Api.Endpoints
{
    public static class ChefsEndpoints
    {
        public static IEndpointRouteBuilder MapChefsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/chefs")
                           .WithTags("Chefs");

            // ➕ Add Chef
            group.MapPost("/",
                async (
                    [FromBody] AddChefApiRequest req,
                    [FromServices] IChefDataService service
                    ) =>
                    {
                        AppDtos.UserDto result =
                            await service.AddChefAsync(
                                new AddChefClientRequest(
                                    req.FirstName,
                                    req.LastName,
                                    req.OriginDeviceId)
                            );

                        return Results.Created($"/chefs/{result.Id}", result);
                    })
                    .WithName("AddChef")
                    .WithSummary("Ajoute un chef");

            // Get all chef
            group.MapGet("/",
                async (
                    [FromServices] IChefDataService service
                ) =>
                {
                    var chefs = await service.GetAllChefsAsync();
                    return Results.Ok(chefs);
                }
            )
            .WithName("GetAllChefs")
            .WithSummary("Récupère la liste des chefs")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);


            // ✏️ Update Chef
            group.MapPut("/{id:guid}",
                async (
                    [FromRoute] Guid id,
                    [FromBody] UpdateChefApiRequest req,
                    [FromServices] IChefDataService service
                    ) =>
                    {
                        AppDtos.UserDto? result =
                            await service.UpdateChefAsync(
                                id,
                                new UpdateChefClientRequest(
                                    req.FirstName,
                                    req.LastName)
                            );

                        return result is null
                            ? Results.NotFound()
                            : Results.Ok(result);
                    })
                    .WithName("UpdateChef")
                    .WithSummary("Met à jour un chef")
                    .Produces(StatusCodes.Status200OK)
                    .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
