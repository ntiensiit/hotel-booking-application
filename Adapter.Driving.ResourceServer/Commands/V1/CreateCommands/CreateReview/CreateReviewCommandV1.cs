using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Review;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateReview;

public record CreateReviewCommandV1(ReviewCreateRequestBodyV1 RequestBody) : ICommand<object>;