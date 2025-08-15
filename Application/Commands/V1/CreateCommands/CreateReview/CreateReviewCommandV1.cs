using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Review;

namespace Application.Commands.V1.CreateCommands.CreateReview;

public record CreateReviewCommandV1(ReviewCreateRequestBodyV1 RequestBody) : ICommand<object>;