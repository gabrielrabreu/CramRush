using Cramming.API.Endpoints.Extensions;
using Cramming.Application.Commands;
using Cramming.Application.Common.Interfaces;
using Cramming.Application.Queries;
using MediatR;

namespace Cramming.API.Endpoints
{
    public class Topics : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);
            
            group.MapGet(GetTopics);

            group.MapGet(GetTopicById, "{topicId}");

            group.MapPost(CreateTopic);

            group.MapPost(AssociateTag, "{topicId}/tags");

            group.MapDelete(DisassociateTag, "{topicId}/tags/{tagId}");

            group.MapPost(OverrideQuestions, "{topicId}/questions:override");
        }

        public async Task<IPaginatedList<TopicBriefDto>> GetTopics(ISender sender, [AsParameters] GetTopicsQuery query)
        {
            return await sender.Send(query);
        }

        public async Task<TopicDetailDto?> GetTopicById(ISender sender, Guid topicId)
        {
            return await sender.Send(new GetTopicByIdQuery(topicId));
        }

        public async Task<IResult> CreateTopic(ISender sender, CreateTopicCommand command)
        {
            var created = await sender.Send(command);
            return Results.CreatedAtRoute(nameof(GetTopicById), new { topicId = created.Id }, created);
        }

        public async Task<IResult> AssociateTag(ISender sender, Guid topicId, AssociateTagCommand command)
        {
            if (topicId != command.TopicId) return Results.BadRequest();
            var created = await sender.Send(command);
            return Results.Created(string.Empty, created);
        }

        public async Task<IResult> DisassociateTag(ISender sender, Guid topicId, Guid tagId)
        {
            await sender.Send(new DisassociateTagCommand(topicId, tagId));
            return Results.NoContent();
        }

        public async Task<IResult> OverrideQuestions(ISender sender, Guid topicId, OverrideQuestionsCommand command)
        {
            if (topicId != command.TopicId) return Results.BadRequest();
            await sender.Send(command);
            return Results.Created();
        }
    }
}
