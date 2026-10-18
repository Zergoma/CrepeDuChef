using CrepeDuChef.Api.Contracts.CrepeParties;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Models;
using CrepeDuChef.Application.Models.Requests.Chefs;
using CrepeDuChef.Application.Models.Requests.CrepeParty;

using Microsoft.AspNetCore.Mvc;


using AppDtos = CrepeDuChef.Application.DTOs;
using AppInterface = CrepeDuChef.Application.Interfaces;
using AppModels = CrepeDuChef.Application.Models;

namespace CrepeDuChef.Api.Endpoints
{
    public static class CrepePartyEndpoints
    {
        public static IEndpointRouteBuilder MapCrepePartyEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/crepeparty")
                       .WithTags("CrepeParty")
                       .WithDescription("Endpoints liés aux sessions de crêpes")
                       .WithSummary("Gestion des sessions de crêpes");

            // Add CrepeParty
            group.MapPost("/",
                async (
                    [FromBody] AddCrepePartyApiRequest req,
                    [FromServices] AppInterface.ICrepePartyDataService service
                ) =>
                {
                    await service.AddCrepePartyAsync(new AddCrepePartyClientRequest(req.Data));
                    return Results.Ok();
                })
                .WithName("AddCrepeSession")
                .WithSummary("Ajoute une nouvelle session de crêpes")
                .WithDescription("Crée une session de crêpes avec l’utilisateur sélectionné")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);


            // Get All Crepes Party
            group.MapGet("/sessions",
                async (
                    [FromServices] AppInterface.ICrepePartyDataService service
                ) =>
                {
                    IEnumerable<AppModels.CrepePartySession> sessions =
                        await service.GetSessionsAsync();
                    return Results.Ok(sessions);
                })
                .WithName("GetCrepeSessions")
                .WithSummary("Récupère toutes les sessions de crêpes")
                .WithDescription("Retourne les sessions regroupées par numéro")
                .Produces<List<CrepePartySession>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

            // Get all chefs from the current session
            //group.MapGet("/chefs",
            //    async ([FromServices] AppInterface.ICrepePartyDataService service) =>
            //    {
            //        List<AppDtos.UserDto> chefs =
            //            await service.GetAllChefsAsync();

            //        return Results.Ok(chefs);
            //    })
            //    .WithName("GetChefs")
            //    .WithSummary("Récupère tous les chefs")
            //    .WithDescription("Retourne la liste des utilisateurs pouvant être chefs")
            //    .Produces<List<UserDto>>(StatusCodes.Status200OK)
            //    .Produces(StatusCodes.Status500InternalServerError);

            return app;
        }
    }
}
